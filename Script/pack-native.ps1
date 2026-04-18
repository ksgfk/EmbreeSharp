Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

. (Join-Path $PSScriptRoot "pack-native-common.ps1")

$EmbreeVersion = "4.4.1"
$OutputDir = Join-Path $PSScriptRoot "build"
$NugetUri = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
$NugetExe = Join-Path $OutputDir "nuget.exe"

Clear-DirectoryContents -Path $OutputDir -Exclude @("nuget.exe")

if (-not (Test-Path -LiteralPath $NugetExe)) {
    Write-Host "download nuget cmdline"
    Invoke-WebRequest -Uri $NugetUri -OutFile $NugetExe
}
else {
    Write-Host "reuse nuget cmdline"
}

$scriptNames = @(
    "pack-native-win-x64.ps1",
    "pack-native-linux-x64.ps1",
    "pack-native-osx-x64.ps1",
    "pack-native-osx-arm64.ps1"
)

foreach ($scriptName in $scriptNames) {
    & (Join-Path $PSScriptRoot $scriptName) $EmbreeVersion $OutputDir $NugetExe
}
