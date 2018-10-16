# Supported tags and respective `Dockerfile` links

## Simple Tags

-	[`develop` (*docker/images/dev-cli/Dockerfile*)](https://github.com/simelo/libskycoin-dotnet/blob/develop/docker/images/dev-cli/Dockerfile)

# Libskycoin DotNet CLI development image

This image has the necessary tools to build, test, edit, lint and version the Libskycoin DotNet
source code.  It comes with Vim editor installed, along with some plugins
to ease go development and version control with git.

# How to use this image

## Initialize your development environment.

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

## Running commands inside the container

You can run commands by just passing the them to the image.  Everything is run
in a container and deleted when finished.

### Running tests

```sh
$ docker run --rm \
    -v ${PWD}/src:/usr/local/src skycoin/skycoindev-dotnet:develop \
    sh -c "cd libskycoin-dotnet; make test"
```

### Editing code

```sh
$ docker run --rm \
    -v ${PWD}/src:/usr/local/src skycoin/skycoindev-libdotnet:develop \
    vim
```

