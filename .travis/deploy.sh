wget -c https://dist.nuget.org/win-x86-commandline/v5.1.0/nuget.exe
mono nuget.exe push ./lib/swig/LibskyfiberNet/bin/Release/*.nupkg -Verbosity detailed -ApiKey $NUGET_API_KEY -Source $NUGET_SOURCE -SkipDuplicate
(cd ./lib/skyapi/src/Skyapi/ && nuget pack ./Skyapi.nuspec)
mono nuget.exe push ./lib/skyapi/src/Skyapi/*.nupkg -Verbosity detailed -ApiKey $NUGET_API_KEY -Source $NUGET_SOURCE -SkipDuplicate
