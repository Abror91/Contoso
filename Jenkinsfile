pipeline{
    agent any
    
    environment {
        dotnet ='C:\\Program Files (x86)\\dotnet\\'
    }
    
    triggers {
        pollSCM 'H * * * *'
    }
    
    stages{
        
        stage('Restore packages'){
            steps {
                bat "dotnet restore Contoso.sln"
            }
        }
        
        stage('Clean'){
            steps {
                bat "dotnet clean Contoso.sln"
            }
        }
        
        stage('Build'){
            steps {
                bat "dotnet build Contoso.sln --configuration Release"
            }
        }
        
        stage('Test: Unit Test'){
            steps {
                bat "dotnet test Contoso.sln"
            }
        }
        
        stage('Publish') {
            steps {
                bat "dotnet publish Contoso.API\\Contoso.API.csproj"
            }
        }
    }
    post {
        always {
            emailext body: "${currentBuild.currentResult}: Job   ${env.JOB_NAME} build ${env.BUILD_NUMBER}\n More info at: ${env.BUILD_URL}",
    recipientProviders: [[$class: 'DevelopersRecipientProvider'], [$class: 'RequesterRecipientProvider']],
    subject: "Jenkins Build ${currentBuild.currentResult}: Job ${env.JOB_NAME}"
        }
    }
}
