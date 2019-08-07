#!/bin/bash

if [[ "$TRAVIS_OS_NAME" == "linux" ]]; then

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
    # curl -o MonoFramework-MDK-4.8.0.524.macos10.xamarin.universal.pkg https://download.mono-project.com/archive/4.8.0/macos-10-universal/MonoFramework-MDK-4.8.0.524.macos10.xamarin.universal.pkg
    # curl -o dotnet-sdk-2.2.102-osx-gs-x64.pkg https://download.visualstudio.microsoft.com/download/pr/34608266-602a-4f3c-9608-48364b01ac9a/3b890a39949af4592189ca9a300cf651/dotnet-sdk-2.2.102-osx-x64.pkg
    # sudo installer -pkg MonoFramework-MDK-4.8.0.524.macos10.xamarin.universal.pkg -target /
    # sudo installer -pkg dotnet-sdk-2.2.102-osx-gs-x64.pkg -target /
    brew update;
    echo 'Creating swig@3.0.12 formula';
    cd "$(brew --repository)/Library/Taps/homebrew/homebrew-core";
    git show 42d31bba7772fb01f9ba442d9ee98b33a6e7a055:Formula/swig.rb | grep -v 'fails_with' > Formula/swig.rb;
    echo 'Installing swig@3.0.12 (3.0.12)';
    brew install swig || brew link --overwrite swig;
    brew install gimme
fi

if [ "$DOTNETCORE" != "" ]; then
    mkdir ./.travis/bin
    curl -o ./.travis/bin/nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
fi

