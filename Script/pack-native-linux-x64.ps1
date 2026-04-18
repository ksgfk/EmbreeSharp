param($EmbreeVersion, $OutputDir, $NugetExe)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

. (Join-Path $PSScriptRoot "pack-native-common.ps1")

$TFM = "linux-x64"
$ExtractDir = Join-Path $OutputDir $TFM
$StageDir = Join-Path $OutputDir "$TFM-lib"

Write-Host "download embree $TFM v$EmbreeVersion"
Invoke-WebRequest -Uri "https://github.com/embree/embree/releases/download/v$EmbreeVersion/embree-$EmbreeVersion.x86_64.linux.tar.gz" -OutFile "$OutputDir/$TFM.tar.gz"

Write-Host "unzip"
Invoke-NativeCommand -FilePath "7z" -ArgumentList @("x", "$OutputDir/$TFM.tar.gz", "-o$OutputDir", "-aoa")
Invoke-NativeCommand -FilePath "7z" -ArgumentList @("x", "$OutputDir/$TFM.tar", "-o$ExtractDir", "-aoa")

"copy libs"
$PackageRoot = Get-EmbreePackageRoot -ExtractDir $ExtractDir
$LibDir = Join-Path $PackageRoot "lib64"
if (-not (Test-Path -LiteralPath $LibDir)) {
    $LibDir = Join-Path $PackageRoot "lib"
}

if (-not (Test-Path -LiteralPath $LibDir)) {
    throw "Cannot locate linux library directory under '$PackageRoot'."
}

Clear-DirectoryContents -Path $StageDir
Copy-ResolvedFile -SourcePath (Join-Path $LibDir "libembree4.so") -DestinationDirectory $StageDir
Copy-ResolvedFile -SourcePath (Join-Path $LibDir "libtbb.so") -DestinationDirectory $StageDir
Copy-ResolvedFile -SourcePath (Join-Path $LibDir "libtbbmalloc.so") -DestinationDirectory $StageDir

Write-Host "package"
@"
<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd">
    <metadata>
        <!-- Required elements-->
        <id>embree-$TFM</id>
        <version>$EmbreeVersion</version>
        <description>unofficial uploaded embree native library</description>
        <authors>ksgfk</authors>
        <tags>native,embree</tags>
        <readme>README.md</readme>

        <!-- Optional elements -->
        <projectUrl>https://github.com/ksgfk/EmbreeSharp</projectUrl>
    </metadata>
    <!-- Optional 'files' node -->
    <files>
        <file src="$TFM-lib\*.so" target="runtimes\$TFM\native" />
        <file src="..\..\README.md" target="\" />
    </files>
</package>
"@ | Out-File -FilePath "$OutputDir/$TFM.nuspec"
Invoke-NativeCommand -FilePath $NugetExe -ArgumentList @("pack", "$OutputDir/$TFM.nuspec", "-OutputDirectory", $OutputDir)
