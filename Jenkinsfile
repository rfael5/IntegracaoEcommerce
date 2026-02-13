pipeline {
    agent any

    stages {
        
        stage('Build Docker Image') {
            steps {
                bat """
                    docker build -t integracao-tpa:latest .
                    docker save integracao-tpa:latest -o integracao-tpa.tar
                """
            }
        }
        stage('Build container') {
            steps {
                withCredentials([sshUserPrivateKey(
                    credentialsId: '63ed79e4-185a-4fff-a603-9021e554a99b',
                    keyFileVariable: 'SSH_KEY',
                    usernameVariable: 'SSH_USER'
                )]){
                bat """
                    icacls "%SSH_KEY%" /inheritance:r
                    icacls "%SSH_KEY%" /grant:r "SYSTEM:R"
                    icacls "%SSH_KEY%" /grant:r "Administrators:R"

                    scp -o StrictHostKeyChecking=no -i %SSH_KEY% integracao-tpa.tar ubuntu@192.168.1.77:/home/ubuntu/arquivos-tar/
                    
                    ssh -o StrictHostKeyChecking=no -i %SSH_KEY% ubuntu@192.168.1.77 ^
                    "sudo k3s ctr images import /home/ubuntu/arquivos-tar/integracao-tpa.tar && \
                    sudo kubectl delete deployment integracao-tpa && \
                    sudo kubectl apply -f /home/ubuntu/projects/bw-k8s-manifestos/integracao-tpa.yaml"
                """
                }
            }
        }
    }
}