nuget push ./lib/swig/LibskycoinNet/bin/Release/*.nupkg -Verbosity detailed -ApiKey $NUGET_API_KEY -Source $NUGET_SOURCE

(cd ./lib/skyapi/src/RestCSharp/ && nuget pack ./RestCSharp.nuspec)

nuget push ./lib/skyapi/src/RestCSharp/*.nupkg -Verbosity detailed -ApiKey $NUGET_API_KEY -Source $NUGET_SOURCE
