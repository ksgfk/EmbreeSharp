param($EmbreeVersion, $OutputDir, $NugetExe)

$TFM = "osx-arm64"

Write-Host "download embree $TFM v$EmbreeVersion"
Invoke-WebRequest -Uri "https://github.com/embree/embree/releases/download/v$EmbreeVersion/embree-$EmbreeVersion.arm64.macosx.zip" -OutFile "$OutputDir/$TFM.zip"

Write-Host "unzip"
7z x "$OutputDir/$TFM.zip" -o"$OutputDir/$TFM" -aoa

"copy libs"
If(Test-Path "$OutputDir/$TFM-lib")
{
    Remove-Item "$OutputDir/$TFM-lib/*" -Recurse
}
Else
{
    New-Item -ItemType Directory -Force -Path "$OutputDir/$TFM-lib"
}
Copy-Item "$OutputDir/$TFM/lib/libembree4.dylib" -Destination "$OutputDir/$TFM-lib"
Copy-Item "$OutputDir/$TFM/lib/libtbb.dylib" -Destination "$OutputDir/$TFM-lib"

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
        <file src="$TFM-lib\*.dylib" target="runtimes\$TFM\native" />
        <file src="..\..\README.md" target="\" />
    </files>
</package>
"@ | Out-File -FilePath "$OutputDir/$TFM.nuspec"
Start-Process -FilePath $NugetExe -ArgumentList "pack $OutputDir/$TFM.nuspec -OutputDirectory $OutputDir" -Wait -NoNewWindow
