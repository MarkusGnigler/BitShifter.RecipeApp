cd ../..

$source = Get-Location

$names  = @('bin')
Get-ChildItem $source -Directory -Include $names -Recurse -Force |
    Sort-Object FullName -Descending |
    Remove-Item -Recurse -Force
    
$names  = @('obj')
Get-ChildItem $source -Directory -Include $names -Recurse -Force |
    Sort-Object FullName -Descending |
    Remove-Item -Recurse -Force

cd Docs/Scripts

exit 0