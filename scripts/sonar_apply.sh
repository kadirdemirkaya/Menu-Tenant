#!/bin/bash

# SonarScanner başlangıç komutunu çalıştır
dotnet sonarscanner begin /k:"Local-Microservice-Dockerize-Demo" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_918506e30e26ef0d3adf127678a600d74e10bc14"
# Projeyi build et
dotnet build ../Microservice-Dockerize-Demo.sln

# SonarScanner bitiş komutunu çalıştır
# dotnet sonarscanner end /d:sonar.login="sqp_918506e30e26ef0d3adf127678a600d74e10bc14"
dotnet sonarscanner end /d:sonar.token="sqp_918506e30e26ef0d3adf127678a600d74e10bc14"