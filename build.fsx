#r @"packages/FAKE.3.5.4/tools/FakeLib.dll"
#load "build-helpers.fsx"
open Fake
open System
open System.IO
open System.Linq
open BuildHelpers
open Fake.XamarinHelper

let version = "1.0.0"
let project = "Unity.IO.Compression"
let package = project + ".unitypackage"
let testResults = "TestResults.xml"

Target "tests" (fun () ->
    Unity("-executeMethod UnityTest.Batch.RunUnitTests -resultFilePath " + testResults)
    sendTeamCityNUnitImport testResults
)

Target "unity" (fun () ->
    let folder = Path.Combine("Assets", project)
    Unity("-exportPackage " + folder + " " + package)
    TeamCityHelper.PublishArtifact package
)

"tests" ==> "unity"

RunTarget()
