#!/bin/bash

export AWS_ACCESS_KEY_ID=test
export AWS_SECRET_ACCESS_KEY=test
export AWS_DEFAULT_REGION=eu-central-1

get_secret() {
    environment=$1
    secret_name=$2
    
    full_secret_name="${environment}_${secret_name}"
    
    secret_value=$(aws --endpoint-url=http://localhost:4566 secretsmanager get-secret-value \
        --secret-id "$full_secret_name" \
        --query SecretString --output text)
    
    if [ -n "$secret_value" ]; then
        echo "$environment:$secret_name -> $secret_value"
    else
        echo "$environment:$secret_name not found"
    fi
}

# Secret'ı alalım
get_secret "Development" "Seq"