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
	[string]$VS_TEST_CONSOLE
)

$ErrorActionPreference 		= "Stop"

Write-Output "`nINFO: Building branch $Branch"
Write-Output "`nINFO: DEVENV_EXE:$DEVENV_EXE"
Write-Output "`nINFO: VS_TEST_CONSOLE:$VS_TEST_CONSOLE"

dir

