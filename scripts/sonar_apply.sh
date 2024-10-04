#!/bin/bash

cd "../"

echo "Current working directory: $(pwd)"

dotnet sonarscanner begin /k:"Menu" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="sqp_b5b681fe8b64d3a30360e45c1332ea4a59d10503"

dotnet build

dotnet sonarscanner end /d:sonar.login="sqp_b5b681fe8b64d3a30360e45c1332ea4a59d10503"

read -p "Press enter to exit"