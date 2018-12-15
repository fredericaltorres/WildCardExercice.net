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

	[Parameter(Mandatory = $true)]
	[string]$ACTION, # build, unittest

	[Parameter(Mandatory = $false)]
	[string]$NUGET = "nuget.exe"
)

$ErrorActionPreference 		= "Stop"

Write-Output "`nINFO: Building branch $Branch"
Write-Output "`nINFO: DEVENV_EXE:$DEVENV_EXE"
Write-Output "`nINFO: VS_TEST_CONSOLE:$VS_TEST_CONSOLE"

function ExecCommandLine($cmd0, $message) {
	$cmd = $cmd0.Replace("~", "`"")
	Write-Output ""
	Write-Output " * * * * * * * * * * * * * * * * * * * * * * * *"
	Write-Output " * $message"
	Write-Output " * * * * * * * * * * * * * * * * * * * * * * * *"
	Write-Output "Command Line:$cmd"
	Write-Output ""
	iex "& $cmd"
	if ($LASTEXITCODE -ne 0) {
		Write-Error "ERROR: Build Error, cmd:$cmd0"
		exit 1
	}
}


if($ACTION -eq "build") {
	ExecCommandLine "~$NUGET~ restore ~$SOLUTION~ " "Restoring packages"
	ExecCommandLine "~$DEVENV_EXE~ ~$SOLUTION~ /build $CONFIGURATION " "Building solution"
}
if($ACTION -eq "unittest") {
	ExecCommandLine "~$VS_TEST_CONSOLE~ ~WildCardExercice.net\bin\$CONFIGURATION\WildCardExercice.net.dll~ " "Running unit tests"
}




