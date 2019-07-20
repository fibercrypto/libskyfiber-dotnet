#!/usr/bin/env bash

set -ex

# Install mono
echo "deb https://download.mono-project.com/repo/ubuntu vs-trusty main" | sudo tee /etc/apt/sources.list.d/mono-official-vs.list
sudo apt update
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
sudo apt install apt-transport-https gcc make cmake git -y
sudo apt-get install monodevelop nuget -y

