#!/bin/bash

if [[ $TRAVIS_OS_NAME == 'linux' ]]; then
    wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    sudo apt-get install apt-transport-https
    sudo apt-get update
    sudo apt-get install dotnet-sdk-2.1
fi

if [[ $TRAVIS_OS_NAME == 'osx' ]]; then
    curl -o dotnet-sdk-2.1.500-osx-gs-x64.pkg https://download.visualstudio.microsoft.com/download/pr/576dbb8d-03f2-4d45-857a-b226d39b3dbe/0d4fbf91aa1137352680ec98ef9edb5d/dotnet-sdk-2.1.500-osx-gs-x64.pkg
    sudo installer -pkg dotnet-sdk-2.1.500-osx-gs-x64.pkg -target /
fi