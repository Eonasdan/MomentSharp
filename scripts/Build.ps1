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

if (Test-Path $destination\bin-Net40 -PathType Container){
    Remove-Item $destination\bin-Net40 -Recurse -Force
    Remove-Item $destination\tests-Net40 -Recurse -Force
}

New-Item $destination\bin-Net40 -ItemType Directory | Out-Null
New-Item $destination\tests-Net40 -ItemType Directory | Out-Null

if (!(Test-Path $nugetDestination -PathType Container)){
    New-Item $nugetDestination -ItemType Directory | Out-Null
}

if (Test-Path $destination\bin-Net45 -PathType Container){
    Remove-Item $destination\bin-Net45 -Recurse -Force
    Remove-Item $destination\tests-Net45 -Recurse -Force
}

New-Item $destination\bin-Net45 -ItemType Directory | Out-Null
New-Item $destination\tests-Net45 -ItemType Directory | Out-Null

$build = [Math]::Floor([DateTime]::UtcNow.Subtract([DateTime]::Parse("01/01/2000").Date).TotalDays)
$revision = [Math]::Floor([DateTime]::UtcNow.TimeOfDay.TotalSeconds / 2)

.\IncrementVersion.ps1 ../MomentSharp $build $revision
.\IncrementVersion.ps1 ../MomentSharp.Tests $build $revision

$msbuild = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
& $msbuild "$parent\MomentSharp.sln" /p:Configuration="$Configuration" /p:Platform="Any CPU" /t:Rebuild /p:VisualStudioVersion=12.0 /v:m /m

Set-Location $parent

Copy-Item MomentSharp\bin\$Configuration-Net45\MomentSharp.dll $destination\bin-Net45\
Copy-Item MomentSharp\bin\$Configuration-Net45\MomentSharp.xml $destination\bin-Net45\
Copy-Item MomentSharp\bin\$Configuration-Net45\MomentSharp.pdb $destination\bin-Net45\
Copy-Item MomentSharp\bin\$Configuration-Net40\MomentSharp.dll $destination\bin-Net40\
Copy-Item MomentSharp\bin\$Configuration-Net40\MomentSharp.xml $destination\bin-Net40\
Copy-Item MomentSharp\bin\$Configuration-Net40\MomentSharp.pdb $destination\bin-Net40\

Copy-Item MomentSharp.Tests\bin\$configuration-Net45\*.ps1 $destination\tests-Net45\
Copy-Item MomentSharp.Tests\bin\$configuration-Net45\*.dll $destination\tests-Net45\
Copy-Item MomentSharp.Tests\bin\$configuration-Net40\*.ps1 $destination\tests-Net40\
Copy-Item MomentSharp.Tests\bin\$configuration-Net40\*.dll $destination\tests-Net40\

$versionInfo = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$destination\bin-Net45\MomentSharp.dll")
$version = $versionInfo.FileVersion.ToString()

Set-Location $parent\.nuget

& ".\NuGet.exe" pack MomentSharp.nuspec -Prop Configuration="$Configuration" -Version $version
Move-Item "MomentSharp.$version.nupkg" "$destination" -force

Set-Location $scriptPath

.\ResetAssemblyInfos.ps1

Write-Host
Write-Host "MomentSharp Build: " $watch.Elapsed