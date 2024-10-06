#!/bin/bash

cd "../"

echo "Current working directory: $(pwd)"

dotnet sonarscanner begin /k:"Menu-Tenant" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_5da742459fd92bbd113a52e1aa5e86680fce0547"

dotnet build

dotnet sonarscanner end /d:sonar.token="sqp_5da742459fd92bbd113a52e1aa5e86680fce0547"

read -p "Press enter to exit"