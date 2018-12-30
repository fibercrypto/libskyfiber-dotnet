ApiKey=$1
Source=$2

nuget push ./LibskycoinNet/bin/Release/*.nupkg -Verbosity detailed -ApiKey $ApiKey -Source $Source