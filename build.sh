#!/bin/bash
dotnet publish --configuration Release --self-contained --output "./bin/publish/win-x64" --runtime win-x64
dotnet publish --configuration Release --self-contained --output "./bin/publish/linux-arm" --runtime linux-arm
