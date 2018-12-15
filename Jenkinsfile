#! /usr/bin/env groovy

// Global Constants
//* [How to execute groovy code](https://www.tutorialspoint.com/execute_groovy_online.php)



pipeline {

	agent any

	options {

		buildDiscarder(logRotator(daysToKeepStr: '2'))
		durabilityHint('MAX_SURVIVABILITY')
	}

	environment {

		CHANNEL = 'Fred Channel'
		SOURCE_BRANCH = "master"
		JENKINS_WORKSPACE = "C:\\Program Files (x86)\\Jenkins\\workspace"
		DEVENV_EXE = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE\\devenv.com"
		VS_TEST_CONSOLE = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE\\CommonExtensions\\Microsoft\\TestWindow\\vstest.console.exe"
		BUILD_CONFIGURATION = "Release"
		BUILD_SCRIPT = ".\\WildCardExercice.net\\build\\build.ps1"
	}

	parameters {

		booleanParam(name: BuildFastMode,        defaultValue: false , description: 'BuildFastMode')
		booleanParam(name: UpdateJenkinsUI,      defaultValue: false , description: 'Refresh Jenkins pipeline job UI')
	}

	stages {

		stage ('Setup environment') {
			steps {
				script {
					echo "Code source branch is ${env.SOURCE_BRANCH}"
					dumpEnvironmentVariables()
					dumpParameters()
				}
			}
		}

		stage('Build .NET Code') {
			steps {
				script {
					//powershell(script: "${env.BUILD_SCRIPT} -ACTION build -SOLUTION 'WildCardExercice.net.sln' -CONFIGURATION '${env.BUILD_CONFIGURATION}' -Branch '${env.SOURCE_BRANCH}' -DEVENV_EXE:'${env.DEVENV_EXE}' -VS_TEST_CONSOLE:'${VS_TEST_CONSOLE}'")
					powershell(script: getBuildCommandLine('build', 'WildCardExercice.net.sln'))
				}
			}
		}

		stage('Run unit tests') {
			steps {
				script {
					powershell(script: "${env.BUILD_SCRIPT} -ACTION unittest -SOLUTION 'WildCardExercice.net.sln' -CONFIGURATION '${env.BUILD_CONFIGURATION}' -Branch '${env.SOURCE_BRANCH}' -DEVENV_EXE:'${env.DEVENV_EXE}' -VS_TEST_CONSOLE:'${VS_TEST_CONSOLE}'")
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
			echo "No clean up"
			// cleanWs()
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

def getBuildCommandLine(action, solution) {
	return "${env.BUILD_SCRIPT} -ACTION ${action} -SOLUTION '${solution}' -CONFIGURATION '${env.BUILD_CONFIGURATION}' -Branch '${env.SOURCE_BRANCH}' -DEVENV_EXE:'${env.DEVENV_EXE}' -VS_TEST_CONSOLE:'${VS_TEST_CONSOLE}'"
}