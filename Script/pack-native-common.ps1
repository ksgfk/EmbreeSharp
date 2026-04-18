Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

function Clear-DirectoryContents {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Path,
        [string[]]$Exclude = @()
    )

    if (-not (Test-Path -LiteralPath $Path)) {
        New-Item -ItemType Directory -Path $Path -Force | Out-Null
        return
    }

    foreach ($item in Get-ChildItem -LiteralPath $Path -Force) {
        if ($Exclude -contains $item.Name) {
            continue
        }

        Remove-Item -LiteralPath $item.FullName -Recurse -Force
    }
}

function Invoke-NativeCommand {
    param(
        [Parameter(Mandatory = $true)]
        [string]$FilePath,
        [string[]]$ArgumentList = @()
    )

    & $FilePath @ArgumentList
    $exitCode = $LASTEXITCODE
    if ($exitCode -ne 0) {
        $displayCommand = @($FilePath) + $ArgumentList
        throw "Command failed with exit code ${exitCode}: $($displayCommand -join ' ')"
    }
}

function Get-EmbreePackageRoot {
    param(
        [Parameter(Mandatory = $true)]
        [string]$ExtractDir
    )

    $rootCandidates = @($ExtractDir)
    $rootCandidates += Get-ChildItem -LiteralPath $ExtractDir -Directory | ForEach-Object { $_.FullName }

    foreach ($candidate in $rootCandidates) {
        if ((Test-Path -LiteralPath (Join-Path $candidate "bin")) -or
            (Test-Path -LiteralPath (Join-Path $candidate "lib")) -or
            (Test-Path -LiteralPath (Join-Path $candidate "lib64"))) {
            return $candidate
        }
    }

    throw "Cannot locate Embree package root under '$ExtractDir'."
}

function Resolve-LinkTargetPath {
    param(
        [Parameter(Mandatory = $true)]
        [System.IO.FileSystemInfo]$Item
    )

    if (-not $Item.LinkType) {
        return $Item.FullName
    }

    $target = $Item.Target
    if ($target -is [Array]) {
        if ($target.Count -ne 1) {
            throw "Link '$($Item.FullName)' resolves to multiple targets."
        }

        $target = $target[0]
    }

    if ([System.IO.Path]::IsPathRooted($target)) {
        return $target
    }

    return [System.IO.Path]::GetFullPath((Join-Path $Item.DirectoryName $target))
}

function Get-LinkStubTargetPath {
    param(
        [Parameter(Mandatory = $true)]
        [System.IO.FileSystemInfo]$Item
    )

    if ($Item.PSIsContainer) {
        return $null
    }

    $bytes = [System.IO.File]::ReadAllBytes($Item.FullName)
    if ($bytes.Length -eq 0 -or $bytes.Length -gt 260 -or ($bytes -contains 0)) {
        return $null
    }

    $target = [System.Text.Encoding]::UTF8.GetString($bytes).Trim()
    if ([string]::IsNullOrWhiteSpace($target)) {
        return $null
    }

    $candidate = if ([System.IO.Path]::IsPathRooted($target)) {
        $target
    }
    else {
        [System.IO.Path]::GetFullPath((Join-Path $Item.DirectoryName $target))
    }

    if (-not (Test-Path -LiteralPath $candidate)) {
        return $null
    }

    return $candidate
}

function Copy-ResolvedFile {
    param(
        [Parameter(Mandatory = $true)]
        [string]$SourcePath,
        [Parameter(Mandatory = $true)]
        [string]$DestinationDirectory,
        [string]$DestinationName
    )

    $currentPath = [System.IO.Path]::GetFullPath($SourcePath)
    $targetName = if ($DestinationName) { $DestinationName } else { [System.IO.Path]::GetFileName($currentPath) }
    $visitedPaths = New-Object System.Collections.Generic.HashSet[string] ([System.StringComparer]::OrdinalIgnoreCase)

    while ($true) {
        if (-not $visitedPaths.Add($currentPath)) {
            throw "Link resolution cycle detected from '$SourcePath'."
        }

        $sourceItem = Get-Item -LiteralPath $currentPath -Force
        $resolvedSourcePath = Resolve-LinkTargetPath -Item $sourceItem
        if ($resolvedSourcePath -ne $sourceItem.FullName) {
            $currentPath = $resolvedSourcePath
            continue
        }

        $linkStubTargetPath = Get-LinkStubTargetPath -Item $sourceItem
        if ($linkStubTargetPath) {
            $currentPath = $linkStubTargetPath
            continue
        }

        break
    }

    $destinationPath = Join-Path $DestinationDirectory $targetName

    Copy-Item -LiteralPath $sourceItem.FullName -Destination $destinationPath -Force
}
