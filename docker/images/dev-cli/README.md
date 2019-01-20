# Supported tags and respective `Dockerfile` links

## Simple Tags

- [`develop, dind, vscode, vscode-dind` (*docker/images/dev-cli/Dockerfile*)](https://github.com/simelo/libskycoin-dotnet/blob/develop/docker/images/dev-cli/Dockerfile)

## Libskycoin .NET  CLI development image

This image has the necessary tools to build, test, edit, lint and version the Libskycoin .NET
source code.  It comes with Vim editor installed, along with some plugins
to ease go development and version control with git.

## How to use this image

### Initialize your development environment

```sh
$ mkdir src
$ docker run --rm \
    -v ${PWD}/src:/usr/local/src skycoin/skycoindev-dotnet:develop \
    git clone https://github.com/simelo/libskycoin-dotnet.git \
$ sudo chown -R `whoami` src
```

This downloads the libdotnet source to src/libdotnet and changes the owner
to your user. This is necessary, because all processes inside the container run
as root and the files created by it are therefore owned by root.

### Running commands inside the container

You can run commands by just passing the them to the image.  Everything is run
in a container and deleted when finished.

#### Running tests

```sh
$ docker run --rm \
    -v ${PWD}/src:/usr/local/src simelotech/skycoindev-dotnet:develop \
    sh -c "cd libskycoin-dotnet; make test"
```

#### Editing code

```sh
$ docker run --rm \
    -v ${PWD}/src:/usr/local/src simelotech/skycoindev-dotnet:develop \
    vim
```

### Use Visual Studio Code

In order to use Visual Studio Code on development process, please read carefull
the [documentation of oficial Skycoin Visual Studio Code dev image](https://github.com/skycoin/skycoin/tree/develop/docker/images/dev-vscode#initialize-your-development-environment)

#### Installed extensions

- [C#](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)
- [C# FixFormat](https://marketplace.visualstudio.com/items?itemName=Leopotam.csharpfixformat)
- [C# Extensions](https://marketplace.visualstudio.com/items?itemName=jchannon.csharpextensions)
- [C# XML Documentation Comments](https://marketplace.visualstudio.com/items?itemName=k--kato.docomment)
- [.NET Core Tools](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet)

#### Add extensions to Visual Studio Code

Like Skycoin Visual Studio Code dev image, you must pass `VS_EXTENSIONS` environment variable
to the command-line with extensions you prefer. **Pass it if you use a docker image with Visual Studio Code**

```sh
$ docker run --rm -it -v /tmp/.X11-unix:/tmp/.X11-unix \
        -v $PWD:/go/src/github.com/simelo/libskycoin-dotnet \
        -w $GOPATH/src/github.com/simelo/libskycoin-dotnet \
        -e DISPLAY=$DISPLAY \
        -e VS_EXTENSIONS="ms-python.python rebornix.Ruby" \
        simelotech/skycoindev-dotnet:vscode
```

### Build your own images

The following arguments influence the Docker build process.

- `IMAGE_FROM`: base image used to build libskycoin-dotnet
- `SOURCE_COMMIT`: the SHA1 hash of the commit being tested.
- `IMAGE_NAME`: the name and tag of the Docker repository being built.
- `DOCKERFILE_PATH`: the dockerfile currently being built.
- `VS_EXTENSIONS` Visual Studio Code extensions to add on docker image.

For instance, the following commands can be executed in order to build using `skycoindev-cli:develop` as base image. **Pay attention, you can't use `VS_EXTENSIONS` build arg when base image is only for CLI purpose**

```sh
$ git clone https://github.com/simelo/libskycoin-dotnet.git && cd libskycoin-dotnet
$ IMAGE_FROM="skycoin/skycoindev-cli:develop"
$ SOURCE_COMMIT=$(git rev-parse HEAD)
$ IMAGE_NAME=simelotech/libskycoin-dotnet:develop
$ DOCKERFILE_PATH=docker/images/dev-cli/Dockerfile
$ docker build --build-arg IMAGE_FROM=$IMAGE_FROM \
               --build-arg BDATE=`date -u +"%Y-%m-%dT%H:%M:%SZ"` \
               --build-arg SCOMMIT=$SOURCE_COMMIT \
               -f $DOCKERFILE_PATH \
               -t "$IMAGE_NAME" .
```

If you want use Visual Studio Code as IDE, you can change `IMAGE_FROM` to build it. **When base image use Visual Studio Code, you can use `VS_EXTENSIONS` build arg**

```sh
$ git clone https://github.com/simelo/libskycoin-dotnet.git && cd libskycoin-dotnet
$ git submodule update --init --recursive
$ # Move to vscode folder to avoid file errors with vscode docker image
$ cd gopath/src/github.com/skycoin/skycoin/docker/images/dev-vscode/
$ IMAGE_FROM="skycoin/skycoindev-vscode:develop"
$ SOURCE_COMMIT=$(git rev-parse HEAD)
$ IMAGE_NAME=simelotech/libskycoin-dotnet:vscode
$ DOCKERFILE_PATH=docker/images/dev-cli/Dockerfile
$ docker build --build-arg IMAGE_FROM="$IMAGE_FROM"
               --build-arg BDATE=`date -u +"%Y-%m-%dT%H:%M:%SZ"` \
               --build-arg SCOMMIT=$SOURCE_COMMIT \
               --build-arg VS_EXTENSIONS="ms-vscode.Go windmilleng.vscode-go-autotest" \
               -f $DOCKERFILE_PATH \
               -t "$IMAGE_NAME" .
```
