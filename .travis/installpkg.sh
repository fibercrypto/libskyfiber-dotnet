
echo "Installing $1 at version $2 in folder $3"

if [ "$DOTNETCORE" == "" ]; then
  dotnet add package $1 --version $2 --package-directory $3
else
  nuget install $1 -Version $2 -OutputDirectory $3
fi

