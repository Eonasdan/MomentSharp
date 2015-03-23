function Locate-BuildNumber 
{
    param ($file, $pattern)
    return (get-content $file | select-string -pattern $pattern).ToString()
}

function Set-BuildNumber
{
    param ($versionLine, $build, $revision)

    $assemblyPattern = "[0-9]+(\.([0-9]+|\*)){1,3}"
    $version = $versionLine | Select-String $assemblyPattern | % { $_.Matches.Value }
    $versionParts = $version.Split('.')

    if ($versionParts.Length -lt 3) { $versionParts += 0 }
    if ($versionParts.Length -lt 4) { $versionParts += 0 }
    if ($versionParts[2] -eq "*") { $versionParts[2] = 0 }

    $versionParts[3] = ([int]$versionParts[3]) + 1
    $newVersionNumber = "{0}.{1}.{2}.{3}" -f $versionParts[0], $versionParts[1], $build, $revision

     return ($versionLine -replace $assemblyPattern, $newVersionNumber).ToString()
}

function Set-BuildNumbers
{
    param($desiredPath, $build, $revision)

    if ($build -eq $null) {
        $build = "0";
    }

    if ($revision -eq $null) {
        $revision = "0";
    }

    $assemblyPattern = "[0-9]+(\.([0-9]+|\*)){1,3}"
    $assemblyVersionPattern = '^\[assembly: AssemblyVersion\("([0-9]+(\.([0-9]+|\*)){1,3})"\)'
    $assemblyFileVersionPattern = '^\[assembly: AssemblyFileVersion\("([0-9]+(\.([0-9]+|\*)){1,3})"\)'
    
    $foundFiles = get-childitem $desiredPath\*AssemblyInfo.cs -Recurse
    if ($foundFiles.Length -le 0)
    {
        Write-Verbose "No files found"
        return
    }

    foreach( $file in $foundFiles )
    {   
        Write-Verbose $file.FullName
        
        $oldVersionNumber = Locate-BuildNumber $file $assemblyVersionPattern
        $newVersionNumber = Set-BuildNumber $oldVersionNumber $build $revision
        $oldFileVersionNumber = Locate-BuildNumber $file $assemblyFileVersionPattern
        $newFileVersionNumber = Set-BuildNumber $oldFileVersionNumber $build $revision
        
        Write-Verbose "Old $oldVersionNumber"
        Write-Verbose "Old $oldFileVersionNumber"
        Write-Verbose "New $newVersionNumber"
        Write-Verbose "New $newFileVersionNumber"  
        
        (Get-Content $file) | % {
            if ($_.StartsWith("//")) { return $_ }
            $_.Replace($oldVersionNumber, $newVersionNumber).Replace($oldFileVersionNumber, $newFileVersionNumber) 
        } | Set-Content $file -Encoding UTF8
    }
}

$folder = $args[0];
$build = $args[1];
$revision = $args[2];

Set-BuildNumbers $folder $build $revision