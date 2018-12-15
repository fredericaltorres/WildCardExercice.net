<#
.SYNOPSIS

#>
[CmdletBinding()]
param (

	[Parameter(Mandatory = $true)]
	[string]$Branch,

	[Parameter(Mandatory = $true)]
	[string]$DEVENV_EXE,

	[Parameter(Mandatory = $true)]
	[string]$VS_TEST_CONSOLE,

	[Parameter(Mandatory = $true)]
	[string]$SOLUTION,

	[Parameter(Mandatory = $true)]
	[string]$CONFIGURATION, # Release | Debug

	[Parameter(Mandatory = $false)]
	[string]$NUGET = "nuget.exe"
)

$ErrorActionPreference 		= "Stop"

Write-Output "`nINFO: Building branch $Branch"
Write-Output "`nINFO: DEVENV_EXE:$DEVENV_EXE"
Write-Output "`nINFO: VS_TEST_CONSOLE:$VS_TEST_CONSOLE"

cmd.exe /c dir /b/s

# c:\tools\nuget.exe restore WildCardExercice.net.sln

$cmd = " ~$NUGET~ restore ~$SOLUTION~ ".Replace("~", "`"")
Write-Output "About to execute build Command:$cmd"
iex "& $cmd"

$cmd = " ~$DEVENV_EXE~ ~$SOLUTION~ /build $CONFIGURATION ".Replace("~", "`"")
Write-Output "About to execute build Command:$cmd"
iex "& $cmd"
if ($LASTEXITCODE -ne 0) {
	Write-Error "ERROR: Build Error"
	exit 1
}

cmd.exe /c dir /b/s





