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
let projectInUnity = Path.Combine("Assets", project)
//let examples = Path.Combine("Assets", "Examples")

Target "dll" (fun () ->
    let output = Path.Combine(project, "bin", "Release")
    let csproj = Path.Combine(project, project + ".csproj")
    MSBuild output "Build" [ ("Configuration", "Release") ] [ csproj ] |> ignore
)

Target "unity" (fun () ->
    CleanDir projectInUnity
    File.Copy(Path.Combine(project, "bin", "Release", project + ".dll"), Path.Combine(projectInUnity, project + ".dll"))
    //Copy projectInUnity (Directory.GetFiles(examples))
    //CleanDir examples
    let folder = Path.Combine("Assets", project)
    Unity("-exportPackage " + folder + " Unity.IO.Compression.unitypackage")
)

"dll" ==> "unity"

RunTarget()
