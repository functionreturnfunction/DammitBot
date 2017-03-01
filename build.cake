#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#addin "Cake.Compression"

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

Task("Release")
	.IsDependentOn("Build")
	.Does(() => {

	ZipCompress("./DammitBot.Console/bin/Release/", "./release.zip");
});

Task("Default")
	.IsDependentOn("Build");

RunTarget(target);