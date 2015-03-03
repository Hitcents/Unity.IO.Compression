module BuildHelpers
#r @"packages/FAKE.3.5.4/tools/FakeLib.dll"
open Fake
open Fake.XamarinHelper
open System
open System.IO
open System.Linq

let possibleUnityPaths = [
    "/Applications/Unity/Unity.app/Contents/MacOS/Unity"
    @"C:\Program Files (x86)\Unity\Editor\Unity.exe"
    @"F:\Unity 4.6.1\Editor\Unity.exe"
]

let Exec command args =
    let result = Shell.Exec(command, args)
    if result <> 0 then failwithf "%s exited with error %d" command result

let RestorePackages solutionFile =
    Exec ".nuget/NuGet.exe" ("restore " + solutionFile)

let RunNUnitTests dllPath xmlPath =
    Exec "/Library/Frameworks/Mono.framework/Versions/Current/bin/nunit-console4" (dllPath + " -xml=" + xmlPath)
    TeamCityHelper.sendTeamCityNUnitImport xmlPath

let TestFlightUpload project apiToken teamToken distList =
    let binFolder = Path.Combine(project, "bin", "iPhone", "Ad-Hoc")
    let ipa = Path.Combine(binFolder, project + ".ipa")
    let notes = Path.Combine(project, "RELEASE-NOTES.txt")
    Exec "curl" ("https://testflightapp.com/api/builds.json -F file=@" + ipa + " -F api_token=" + apiToken + " -F team_token=" + teamToken + " -F notes=@" + notes + " -F notify=False -F distribution_lists='" + distList + "'")

let UpdatePlist version project =
    let build = environVarOrDefault "BUILD_NUMBER" ""
    if not(String.IsNullOrEmpty(build)) then do
        let info = Path.Combine(project, "Info.plist")
        let finalVersion = version + "." + build
        Exec "/usr/libexec/PlistBuddy" ("-c 'Set :CFBundleVersion " + finalVersion + "' " + info)

let UnityPath =
    (Seq.where(fun p -> File.Exists(p)) possibleUnityPaths).First()

let Unity args =
    let fullPath = Path.GetFullPath(".")
    let result = Shell.Exec(UnityPath, "-quit -batchmode -logFile -projectPath \"" + fullPath + "\" " + args)
    if result < 0 then failwithf "Unity exited with error %d" result