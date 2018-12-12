<#
.SYNOPSIS

#>
[CmdletBinding()]
param (

	[Parameter(Mandatory = $true)]
	[string]
	$Branch
)

$ErrorActionPreference 		= "Stop"

Write-Output "`nINFO: Building branch $Branch"

