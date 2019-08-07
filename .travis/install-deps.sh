#!/bin/bash

if [[ "$TRAVIS_OS_NAME" == "linux" ]]; then
    wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb
    sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
    echo "deb https://download.mono-project.com/repo/ubuntu stable-bionic main" | sudo tee /etc/apt/sources.list.d/mono-official-stable.list
    sudo apt-get install apt-transport-https
    sudo dpkg -i packages-microsoft-prod.deb
    sudo apt-get update
#    sudo apt-get upgrade
    sudo apt-get install -y \
                 dotnet-sdk-2.2 \
                 mono-devel \
                 mono-complete \
                 referenceassemblies-pcl \
                 ca-certificates-mono \
                 msbuild
    mkdir swig_build && \
      cd swig_build && \
      wget http://prdownloads.sourceforge.net/swig/swig-3.0.12.tar.gz && \
      tar -zxf swig-3.0.12.tar.gz && \
      cd swig-3.0.12 && \
      sudo ./configure --prefix=/usr && \
      sudo make && sudo make install && \
      cd ../../ && \
      sudo rm -rf swig_build
fi

if [[ "$TRAVIS_OS_NAME" == "osx" ]]; then
    curl -o MonoFramework-MDK-4.8.0.524.macos10.xamarin.universal.pkg https://download.mono-project.com/archive/4.8.0/macos-10-universal/MonoFramework-MDK-4.8.0.524.macos10.xamarin.universal.pkg
    curl -o dotnet-sdk-2.2.102-osx-gs-x64.pkg https://download.visualstudio.microsoft.com/download/pr/34608266-602a-4f3c-9608-48364b01ac9a/3b890a39949af4592189ca9a300cf651/dotnet-sdk-2.2.102-osx-x64.pkg
    sudo installer -pkg MonoFramework-MDK-4.8.0.524.macos10.xamarin.universal.pkg -target /
    sudo installer -pkg dotnet-sdk-2.2.102-osx-gs-x64.pkg -target /

    ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)" < /dev/null 2> /dev/null
    brew install swig
    brew install gimme
fi

if [ "$DOTNETCORE" != "" ]; then
    mkdir ./.travis/bin
    curl -o ./.travis/bin/nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
fi

