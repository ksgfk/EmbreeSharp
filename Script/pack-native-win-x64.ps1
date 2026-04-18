param($EmbreeVersion, $OutputDir, $NugetExe)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

. (Join-Path $PSScriptRoot "pack-native-common.ps1")

$TFM = "win-x64"
$ExtractDir = Join-Path $OutputDir $TFM
$StageDir = Join-Path $OutputDir "$TFM-lib"

Write-Host "download embree win-x64 v$EmbreeVersion"
Invoke-WebRequest -Uri "https://github.com/embree/embree/releases/download/v$EmbreeVersion/embree-$EmbreeVersion.x64.windows.zip" -OutFile "$OutputDir/$TFM.zip"

Write-Host "unzip"
Expand-Archive -Path "$OutputDir/$TFM.zip" -DestinationPath $ExtractDir -Force

$PackageRoot = Get-EmbreePackageRoot -ExtractDir $ExtractDir
$BinDir = Join-Path $PackageRoot "bin"

Clear-DirectoryContents -Path $StageDir
Copy-ResolvedFile -SourcePath (Join-Path $BinDir "embree4.dll") -DestinationDirectory $StageDir
Copy-ResolvedFile -SourcePath (Join-Path $BinDir "tbb12.dll") -DestinationDirectory $StageDir
Copy-ResolvedFile -SourcePath (Join-Path $BinDir "tbbmalloc.dll") -DestinationDirectory $StageDir

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
        <file src="$TFM-lib\*.dll" target="runtimes\$TFM\native" />
        <file src="..\..\README.md" target="\" />
    </files>
</package>
"@ | Out-File -FilePath "$OutputDir/$TFM.nuspec"
Invoke-NativeCommand -FilePath $NugetExe -ArgumentList @("pack", "$OutputDir/$TFM.nuspec", "-OutputDirectory", $OutputDir)
