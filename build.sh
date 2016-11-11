#!/usr/bin/env bash

dotnet restore

pwd 
dotnet build ./Colina/src/Colina.Api/


"$PATH/dotnet.exe" build ./Colina/src/Colina.Api/project.json