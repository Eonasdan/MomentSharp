param (
    [Parameter()]
    [string] $Configuration = "Release"
)

$watch = [System.Diagnostics.Stopwatch]::StartNew()
$scriptPath = Split-Path (Get-Variable MyInvocation).Value.MyCommand.Path 
$parent = Split-Path $scriptPath -Parent
Set-Location $scriptPath
$destination = "$parent\build"
$nugetDestination = "$parent\build"

if (!(Test-Path $destination -PathType Container)){
    New-Item $destination -ItemType Directory | Out-Null
}

if (Test-Path $destination\bin -PathType Container){
    Remove-Item $destination\bin -Recurse -Force
    Remove-Item $destination\tests -Recurse -Force
}

New-Item $destination\bin -ItemType Directory | Out-Null
New-Item $destination\tests -ItemType Directory | Out-Null

if (!(Test-Path $nugetDestination -PathType Container)){
    New-Item $nugetDestination -ItemType Directory | Out-Null
}

$build = [Math]::Floor([DateTime]::UtcNow.Subtract([DateTime]::Parse("01/01/2000").Date).TotalDays)
$revision = [Math]::Floor([DateTime]::UtcNow.TimeOfDay.TotalSeconds / 2)

.\IncrementVersion.ps1 ../MomentSharp $build $revision
.\IncrementVersion.ps1 ../MomentSharp.Tests $build $revision

$msbuild = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
& $msbuild "$parent\MomentSharp.sln" /p:Configuration="$Configuration" /p:Platform="Any CPU" /t:Rebuild /p:VisualStudioVersion=12.0 /v:m /m

Set-Location $parent

Copy-Item MomentSharp\bin\$Configuration\MomentSharp.dll $destination\bin\
Copy-Item MomentSharp.Tests\bin\$configuration\*.ps1 $destination\tests\
Copy-Item MomentSharp.Tests\bin\$configuration\*.dll $destination\tests\

$versionInfo = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$destination\bin\MomentSharp.dll")
$version = $versionInfo.FileVersion.ToString()

Set-Location $parent\.nuget

& ".\NuGet.exe" pack MomentSharp.nuspec -Prop Configuration="$Configuration" -Version $version
Move-Item "MomentSharp.$version.nupkg" "$destination" -force

Set-Location $scriptPath

.\ResetAssemblyInfos.ps1

Write-Host
Write-Host "MomentSharp Build: " $watch.Elapsed