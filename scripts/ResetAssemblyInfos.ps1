$currentPath = (Get-Location -PSProvider FileSystem).ProviderPath
$assemblyInfos = Get-ChildItem -Recurse -Filter "assemblyinfo.cs"

foreach ($assemblyInfo in $assemblyInfos) {
    Write-Verbose $assemblyInfo.FullName
    Invoke-Expression ("cd " + $assemblyInfo.Directory.FullName)
    Invoke-Expression ("git checkout -- " + $assemblyInfo.Name)
}

Set-Location $currentPath