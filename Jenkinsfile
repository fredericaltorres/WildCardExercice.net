#! /usr/bin/env groovy

// Global Constants
//* [How to execute groovy code](https://www.tutorialspoint.com/execute_groovy_online.php)

def SourceBranch = "master"

pipeline {

	agent any

	options {
		buildDiscarder(logRotator(daysToKeepStr: '2'))
		durabilityHint('MAX_SURVIVABILITY')
	}

	environment {

		CHANNEL = 'Fred Channel'
		JENKINS_WORKSPACE = "C:\\Program Files (x86)\\Jenkins\\workspace"
		DEVENV_EXE = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE\\devenv.com"
		VS_TEST_CONSOLE = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE\\CommonExtensions\\Microsoft\\TestWindow\\vstest.console.exe"
	}

	parameters {

		booleanParam(name: BuildFastMode,        defaultValue: false , description: 'BuildFastMode')
		booleanParam(name: UpdateJenkinsUI,      defaultValue: false , description: 'Refresh Jenkins pipeline job UI')
	}

	stages {

		stage ('Setup environment') {
			steps {
				script {
					echo "Code source branch is ${SourceBranch}"
					dumpEnvironmentVariables()
					dumpParameters()
				}
			}
		}

		stage('Build .NET Code') {
			steps {
				script {
					echo "Build .NET Code . . ."
					// echo "GetEnvironmentVariablesAsPowerShellCommandLine: ${GetEnvironmentVariablesAsPowerShellCommandLine()}"
					powershell(script: ".\\WildCardExercice.net\\build\\build.ps1 -SOLUTION 'WildCardExercice.net.sln' -CONFIGURATION 'Release' -Branch '${SourceBranch}' -DEVENV_EXE:'${env.DEVENV_EXE}' -VS_TEST_CONSOLE:'${VS_TEST_CONSOLE}'")
				}
			}
		}

		stage('Update jenkins UI') {
			when {
				expression { params.UpdateJenkinsUI == true }
			}
			steps {
				echo "Refresh Jenkins pipeline job UI"
			}
		}
	}
	post {
		always {
			script {
				currentBuild.setKeepLog(true)
			}
		}
		success {
			script {
				echo " SUCCESS "
			}
		}
		aborted {
			script {
				echo " ABORTING "
			}
		}
		failure {
			script {
				echo " FAILURE "
			}
		}
		cleanup {
			cleanWs()
		}
	}
}

// Does not work, do not see new variables
def GetEnvironmentVariablesAsPowerShellCommandLine() {
    echo "*** GetEnvironmentVariablesAsPowerShellCommandLine ***"
	def r = ""
    env.each {
		key, value -> r += "-${key}:'${value}';"
	}
	return r
}


def dumpEnvironmentVariables() {
    echo "*** Environment Variables ***"
    env.each {
		key, value -> echo "${key}: ${value}"
	}    
}

def dumpParameters() {
    echo "*** Parameters ***"
	params.each {
		key, value -> echo "${key}: ${value}"
	}
}