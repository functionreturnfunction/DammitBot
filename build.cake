#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#addin "Cake.Compression"
#addin nuget:?package=Cake.Git

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var solutionFile = "./DammitBot.sln";

// Hack: Cake.Compression is missing local copy of SharpZipLib dependencies
if (!FileExists("./tools/Addins/Cake.Compression/lib/net45/ICSharpCode.SharpZipLib.dll")) {
    CopyFile("./tools/Addins/SharpZipLib/lib/20/ICSharpCode.SharpZipLib.dll",
        "./tools/Addins/Cake.Compression/lib/net45/ICSharpCode.SharpZipLib.dll");
}

Task("Restore-NuGet-Packages")
//    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solutionFile);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() => {

    if(IsRunningOnWindows())
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
	.IsDependentOn("Build")
	.IsDependentOn("CleanRelease")
	.Does(() => {

	var lastCommit = GitLogTip(".");
	ZipCompress("./src/DammitBot.Console/bin/Release/", string.Format("./release-{0}-{1:yyyy-MM-dd-HHmmss}.zip", lastCommit.Sha.Substring(0, 8), lastCommit.Author.When, ".zip"));
});

Task("Default")
	.IsDependentOn("Build");

RunTarget(target);