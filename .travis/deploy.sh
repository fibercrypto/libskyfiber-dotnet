nuget push ./lib/swig/LibskycoinNet/bin/Release/*.nupkg -Verbosity detailed -ApiKey $NUGET_API_KEY -Source $NUGET_SOURCE -SkipDuplicate

(cd ./lib/skyapi/src/Skyapi/ && nuget pack ./Skyapi.nuspec)

nuget push ./lib/skyapi/src/Skyapi/*.nupkg -Verbosity detailed -ApiKey $NUGET_API_KEY -Source $NUGET_SOURCE -SkipDuplicate
