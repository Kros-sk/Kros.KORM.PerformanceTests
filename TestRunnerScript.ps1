Write-Host "Kros.Korm performance tests." -ForegroundColor Green
Write-Host "Build Kros.KORM.Performance." -ForegroundColor Green

$msBuild = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe"

$buildCommand = """$msBuild"" Kros.KORM.PerformanceTests.csproj /p:Configuration=Debug"
Write-Host $buildCommand -ForegroundColor Yellow
Write-Host
Invoke-Expression "& $buildCommand"

Write-Host "Clear old tests results." -ForegroundColor Green

$reportFolder = ".\Bin\Debug\Report"
if (Test-Path $reportFolder -PathType Any) {
    Remove-Item "$reportFolder\*.*"
}
if (Test-Path ".\Bin\Debug\TempInsertUpdateTest" -PathType Any) {
    Remove-Item ".\Bin\Debug\TempInsertUpdateTest\*.*"
}

Write-Host "Runner pre Kros.KORM tests." -ForegroundColor Green

.\Bin\Debug\NBench.Runner.exe `
    .\Bin\Debug\Kros.KORM.PerformanceTests.exe `
    output-directory=$reportFolder

$pass = $true

Get-ChildItem $reportFolder -Filter *.md |
    Foreach-Object {
    $result = Get-content -tail 2 $_.FullName
    $fullTestName = Get-Content -First 1 $_.FullName
    $testName = $fullTestName.Replace("# Kros.KORM.PerformanceTests.", "")

    if ($result[0].StartsWith("* [PASS]")) {
        "{0,-70} {1}" -f $testName, "****PASS****" |  Write-Host -ForegroundColor Green
    }
    else {
        "{0,-70} {1}" -f $testName, "****FAILED****" |  Write-Warning
        $pass = $false
    }
}

if (-not $pass) {
    Write-Error "Performance tests failed!!!"
    exit 1
}
else {
    exit 0
}