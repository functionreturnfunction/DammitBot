#tool nuget:?package=NUnit.ConsoleRunner
#tool nuget:?package=xunit.runner.console
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
        Assembly = "./src/DammitBot.Plugins.Data.Migrations/bin/Release/DammitBot.Plugins.Data.Migrations.dll"
        });
    FluentMigrator(new FluentMigratorSettings {
        Connection = Argument("data_source", "Data Source=localhost;Initial Catalog=DammitBot;Integrated Security=true"),
        Provider = "sqlserver",
        Assembly = "./src/DammitBot.Plugins.Reminders.Migrations/bin/Release/DammitBot.Plugins.Reminders.Migrations.dll"
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

Task("Test")
    .IsDependentOn("BuildRelease")
    .Does(() => {
    XUnit2(GetFiles("./src/**/bin/Release/*.Tests.dll"));
});


Task("CleanRelease")
	.Does(() => {
	DeleteFiles("release*.zip");
});

Task("Release")
	.IsDependentOn("Test")
	.IsDependentOn("CleanRelease")
	.Does(() => {
	var lastCommit = GitLogTip(".");
    CopyFile("./src/DammitBot.Console/App.config.example", "./src/DammitBot.Console/bin/Release/DammitBot.exe.config");
	ZipCompress("./src/DammitBot.Console/bin/Release/", string.Format("./release-{0}-{1:yyyy-MM-dd-HHmmss}.zip", lastCommit.Sha.Substring(0, 8), lastCommit.Author.When));
});

Task("Default")
	.IsDependentOn("BuildRelease");

RunTarget(target);