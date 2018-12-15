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

function ExecCommandLine($cmd0) {
	$cmd = $cmd0.Replace("~", "`"")
	Write-Output ""
	Write-Output "About to execute build Command:$cmd"
	iex "& $cmd"
	if ($LASTEXITCODE -ne 0) {
		Write-Error "ERROR: Build Error, cmd:$cmd0"
		exit 1
	}
}

ExecCommandLine "~$NUGET~ restore ~$SOLUTION~ "
ExecCommandLine "~$DEVENV_EXE~ ~$SOLUTION~ /build $CONFIGURATION "
ExecCommandLine "~$VS_TEST_CONSOLE~ ~WildCardExercice.net\bin\$CONFIGURATION\WildCardExercice.net.dll~ "


