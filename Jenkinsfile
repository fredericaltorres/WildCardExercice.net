#! /usr/bin/env groovy

// Global Constants

def SourceBranch = "master"

pipeline {

	agent any

    options {
        buildDiscarder(logRotator(daysToKeepStr: '2'))
        durabilityHint('MAX_SURVIVABILITY')
    }	

    environment {

		CHANNEL = 'Fred Channel'
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
			when {
                expression { true }
			}
			steps {
                script {
                    echo "Build .NET Code . . ."
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


def dumpEnvironmentVariables() {
    echo '============================ Environment Variables ============================='
    env.each {
		key, value -> echo "${key}: ${value}"
	}
    echo '========================== End Environment Variables ==========================='
}

def dumpParameters() {
    echo '================================= Parameters ==================================='
	params.each {
		key, value -> echo "${key}: ${value}"
	}
    echo '=============================== End Parameters ================================='
}
