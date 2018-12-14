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

In order to use Visual Studio Code on development, please read carefull 
the [documentation of oficial Skycoin Visual Studio Code dev image](https://github.com/skycoin/skycoin/tree/develop/docker/images/dev-vscode#initialize-your-development-environment)

#### Add extensions to Visual Studio Code

Like Skycoin Visual Studio Code dev image, you must pass `VS_EXTENSIONS` environment variable
to the command-line with extensions you prefer.

```sh
$ docker run --rm -it -v /tmp/.X11-unix:/tmp/.X11-unix \
        -v $PWD:/go/src/github.com/simelo/libskycoin-dotnet \
        -w $GOPATH/src/github.com/simelo/libskycoin-dotnet \
        -e DISPLAY=$DISPLAY \
        -e VS_EXTENSIONS="ms-python.python rebornix.Ruby" \
        simelotech/skycoindev-dotnet:vscode
```

### Build your own images

To be filled
