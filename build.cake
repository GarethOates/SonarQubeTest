#tool "nuget:?package=xunit.runner.console"
#tool "nuget:?package=OpenCover"
#tool "nuget:?package=ReportGenerator"

var solution = "./SonarQubeTest.sln";
var project = "Calculator";
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

Task("Restore").Does(() => NuGetRestore(solution));

// Build

Task("Build")
	.IsDependentOn("Restore")
	.Does(() => MSBuild(solution, configurator =>
		configurator.SetConfiguration("Debug"))
);

// Test

Task("Test")
	.IsDependentOn("Build")
	.Does(() =>
{
	var testResults = "./TestResults";

	var coverSettings = new OpenCoverSettings()
		.WithFilter("+[Calculator.*]*")
		.WithFilter("-[Calculator.Tests.*]*");

	if (!DirectoryExists(testResults))
	{
		CreateDirectory(testResults);
	}

	FilePath codeCoverage = new FilePath(new DirectoryPath(testResults) + "/CodeCoverage.xml");

	OpenCover(
		tool =>
		{
			tool.XUnit2(
				"./Calculator.Tests/bin/Debug/CalculatorTests.dll",
				new XUnit2Settings {
					OutputDirectory = new DirectoryPath(testResults),
					XmlReport = true,
					HtmlReport = false
				}
			);
		},
		codeCoverage,
		coverSettings
	);
});

// Default target

Task("Default")
	.IsDependentOn("Build")
	.IsDependentOn("Test");

var target = Argument("Target", "Default");
RunTarget(target);
