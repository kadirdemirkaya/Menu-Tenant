#!/bin/bash

export AWS_ACCESS_KEY_ID=test
export AWS_SECRET_ACCESS_KEY=test
export AWS_DEFAULT_REGION=eu-central-1

add_secret() {
	secret_name=$1 
	secret_value=$2
	aws --endpoint-url=http://localhost:4566 secretsmanager create-secret --name $secret_name --secret-string $secret_value
	echo "Added secret $secret_name"
}

add_secret "Seq" "localhost:5341"
add_secret "SecretManagement" "my_secret_2"
add_secret "Redis" "localhost:6379,ssl=False,allowAdmin=true"
add_secret "Postgresql1" ""
add_secret "Postgresql2" ""
