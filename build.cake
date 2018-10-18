#tool "nuget:?package=xunit.runner.console"

var solution = "./UDI.UIP.PJ_1026_FamFaglaertAutomatisering.sln";
var project = "PJ_1026_FamFaglaertAutomatisering";
var projectFolder = "./" + project;

// Clean

Task("DeleteArtifacts")
	.WithCriteria(DirectoryExists("./artifacts"))
	.Does(() => DeleteDirectory("./artifacts", recursive:true));

Task("DeleteBin")
	.WithCriteria(DirectoryExists(projectFolder + "/bin"))
	.Does(() => DeleteDirectory(projectFolder + "/bin", recursive:true));

Task("DeleteObj")
	.WithCriteria(DirectoryExists(projectFolder + "/obj"))
	.Does(() => DeleteDirectory(projectFolder + "/obj", recursive:true));

Task("DeleteTestResults")
	.WithCriteria(DirectoryExists("./TestResults"))
	.Does(() => DeleteDirectory("./TestResults", recursive:true));

Task("Clean")
	.IsDependentOn("DeleteArtifacts")
	.IsDependentOn("DeleteBin")
	.IsDependentOn("DeleteObj")
	.IsDependentOn("DeleteTestResults");

// Restore

Task("Restore").Does(() => NuGetRestoreSolution(solution));

// Build

Task("Build")
	.IsDependentOn("Restore")
	.IsDependentOn("Update")
	.Does(() => MSBuildSolution(solution));

// Test

Task("Test")
	.IsDependentOn("Build")
	.Does(() => 
{
	var testResults = "./TestResults";

	if (!DirectoryExists(testResults))
	{
		CreateDirectory(testResults);
	}

	XUnit2("./UnitTests/bin/Debug/UDI.UIP." + project + ".UnitTests.dll", new XUnit2Settings() { 
		OutputDirectory = new DirectoryPath(testResults),
		XmlReport = true,
		HtmlReport = false
	});
});

// Default target

Task("Default")
	.IsDependentOn("Build")
	.IsDependentOn("Test");

var target = Argument("Target", "Default");
RunTarget(target);