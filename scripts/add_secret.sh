#!/bin/bash

export AWS_ACCESS_KEY_ID=test
export AWS_SECRET_ACCESS_KEY=test
export AWS_DEFAULT_REGION=eu-central-1

add_or_update_secret() {
    environment=$1
    secret_name=$2
    secret_value=$3
    
    full_secret_name="${environment}_${secret_name}"
    
    existing_secret=$(aws --endpoint-url=http://localhost:4566 secretsmanager describe-secret --secret-id "$full_secret_name" 2>/dev/null)
    
    if [ -z "$existing_secret" ]; then
        aws --endpoint-url=http://localhost:4566 secretsmanager create-secret \
            --name "$full_secret_name" \
            --secret-string "$secret_value" \
            --tags Key=Environment,Value="$environment"
        echo "Added secret $full_secret_name"
    else
        aws --endpoint-url=http://localhost:4566 secretsmanager update-secret \
            --secret-id "$full_secret_name" \
            --secret-string "$secret_value"
        echo "Updated secret $full_secret_name"
    fi
}

add_or_update_secret "Development" "Seq" "localhost:5341"
add_or_update_secret "Development" "SecretManagement" "localhost:4566"
add_or_update_secret "Development" "Redis" "localhost:6379,ssl=False,allowAdmin=true"
#
add_or_update_secret "Development" "PostgresAuth_Host" "localhost"
add_or_update_secret "Development" "PostgresAuth_Port" "5434"
add_or_update_secret "Development" "PostgresAuth_UserName" "admin"
add_or_update_secret "Development" "PostgresAuth_Password" "321"
add_or_update_secret "Development" "PostgresAuth_DatabaseName" "authdb"
