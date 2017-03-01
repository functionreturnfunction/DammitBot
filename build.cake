#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#addin "Cake.Compression"
#addin nuget:?package=Cake.Git
#addin "Cake.FluentMigrator"

var target = Argument("target", "Default");
var solutionFile = "./DammitBot.sln";

// Hack: Cake.Compression is missing local copy of SharpZipLib dependencies
if (!FileExists("./tools/Addins/Cake.Compression/lib/net45/ICSharpCode.SharpZipLib.dll")) {
    CopyFile("./tools/Addins/SharpZipLib/lib/20/ICSharpCode.SharpZipLib.dll",
        "./tools/Addins/Cake.Compression/lib/net45/ICSharpCode.SharpZipLib.dll");
}

Task("Migrate")
    .IsDependentOn("BuildRelease")
    .Does(() => {
    FluentMigrator(new FluentMigratorSettings {
        Connection = Argument("data_source", "Data Source=localhost;Initial Catalog=DammitBot;Integrated Security=true"),
        Provider = "sqlserver",
        Assembly = "./src/DammitBot.Plugins.Data/bin/Release/DammitBot.Plugins.Data.dll"
	});
});

Task("Restore-NuGet-Packages")
//    .IsDependentOn("Clean")
    .Does(() => {
    NuGetRestore(solutionFile);
});

Task("Clean")
	.Does(() => {
	CleanDirectories("./src/*/bin/Release");
});

Task("BuildRelease")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() => {
	var configuration = Argument("configuration", "Release");
	
	if (IsRunningOnWindows())
	{
		// Use MSBuild
		MSBuild(solutionFile, settings =>
		settings.SetConfiguration(configuration));
	}
	else
	{
		// Use XBuild
		XBuild(solutionFile, settings =>
		settings.SetConfiguration(configuration));
	}
});


Task("CleanRelease")
	.Does(() => {
	DeleteFiles("release*.zip");
});

Task("Release")
	.IsDependentOn("BuildRelease")
	.IsDependentOn("CleanRelease")
	.Does(() => {
	var lastCommit = GitLogTip(".");
	ZipCompress("./src/DammitBot.Console/bin/Release/", string.Format("./release-{0}-{1:yyyy-MM-dd-HHmmss}.zip", lastCommit.Sha.Substring(0, 8), lastCommit.Author.When, ".zip"));
});

Task("Default")
	.IsDependentOn("BuildRelease");

RunTarget(target);