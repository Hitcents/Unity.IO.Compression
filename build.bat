@echo off
.nuget\NuGet.exe install FAKE -Version 3.5.4
packages\FAKE.3.5.4\tools\FAKE.exe build.fsx %1
