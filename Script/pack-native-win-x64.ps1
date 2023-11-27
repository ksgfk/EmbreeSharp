param($EmbreeVersion, $OutputDir, $NugetExe)

$TFM = "win-x64"

Write-Host "download embree win-x64 v$EmbreeVersion"
Invoke-WebRequest -Uri "https://github.com/embree/embree/releases/download/v$EmbreeVersion/embree-$EmbreeVersion.x64.windows.zip" -OutFile "$OutputDir/$TFM.zip"

Write-Host "unzip"
Expand-Archive -Path "$OutputDir/$TFM.zip" -DestinationPath "$OutputDir/$TFM"

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
        <file src="$TFM\bin\*.dll" target="runtimes\$TFM\native" />
        <file src="..\..\README.md" target="\" />
    </files>
</package>
"@ | Out-File -FilePath "$OutputDir/$TFM.nuspec"
Start-Process -FilePath $NugetExe -ArgumentList "pack $OutputDir/$TFM.nuspec -OutputDirectory $OutputDir" -Wait -NoNewWindow
