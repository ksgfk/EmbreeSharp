$EmbreeVersion = "4.3.0"
$OutputDir = "build"
$NugetUri = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
$NugetExe = "$OutputDir/nuget.exe"

If(Test-Path $OutputDir)
{
    Remove-Item $OutputDir/* -Recurse
}
Else
{
    New-Item -ItemType Directory -Force -Path $OutputDir
}

"download nuget cmdline"
Invoke-WebRequest -Uri $NugetUri -OutFile $NugetExe

.\pack-native-win-x64.ps1 $EmbreeVersion $OutputDir $NugetExe
