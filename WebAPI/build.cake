#tool "nuget:?package=xunit.runner.console&version=2.4.1"
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
 
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
///    Build Variables
/////////////////////////////////////////////////////////////////////
var outputDir = Directory("Build") + Directory(configuration);
var projDir = "./VintageCars.Web/VintageCars.Web";
var packageDir = "./VintageCars.Web/packages";
var mainCsprojPath = projDir + "/VintageCars.Web.csproj";
var buildSettings = new DotNetCoreBuildSettings
     {
         Framework = "netcoreapp3.1",
         Configuration = "Release",
         OutputDirectory = outputDir
     };

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
Task("Clean")
    .Does(() => 
{
    CleanDirectory(packageDir);
    Information("--- Cleaned packages folder");
    CleanDirectory(outputDir);
    Information("--- Cleaned build folder");
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() => 
{
    DotNetCoreRestore(projDir);
});

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() =>
{
    DotNetCoreBuild(mainCsprojPath, buildSettings);
});

Task("Default")
    .IsDependentOn("Build");
RunTarget(target);