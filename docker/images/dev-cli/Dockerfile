ARG IMAGE_FROM=skycoin/skycoindev-cli:develop
FROM $IMAGE_FROM

ARG BDATE
ARG SCOMMIT

# Image labels (see ./hooks/build for ARGS)
LABEL "org.label-schema.name"="libskycoin-dotnet" \
      "org.label-schema.description"="Docker image with mono, go, node and dev tools for libskycoin-dotnet developers" \
      "org.label-schema.vendor"="Skycoin project" \
      "org.label-schema.url"="skycoin.net" \
      "org.label-schema.version"="0.1.0" \
      "org.label-schema.schema-version"="1.0" \
      "org.label-schema.build-date"=$BDATE \
      "org.label-schema.vcs-url"="https://github.com/skycoin/libskycoin-dotnet.git" \
      "org.label-schema.vcs-ref"=$SCOMMIT \
      "org.label-schema.usage"="https://github.com/skycoin/libskycoin-dotnet/blob/"$SCOMMIT"/docker/images/dev-cli/README.md" \
      "org.label-schema.docker.cmd"="mkdir src; docker run --rm -v ${PWD}/src:/usr/local/src skycoin/skycoindev-dotnet:develop git clone https://github.com/simelo/libskycoin-dotnet.git; sudo chown -R `whoami` src"

ENV MONO_VERSION 4.8.0.524
ENV DEBIAN_FRONTEND noninteractive

# Add mono debian repo and ins
RUN apt update \
    && apt upgrade -y \
    && apt install -y curl \
           apt-transport-https \
           dirmngr \
    && apt-key adv --no-tty --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF \
    && echo "deb https://download.mono-project.com/repo/debian wheezy/snapshots/$MONO_VERSION main" > /etc/apt/sources.list.d/mono-official-stable.list \
    && apt update \
    && apt install -y \
           binutils \
           mono-devel \
           ca-certificates-mono \
           fsharp \
           mono-vbnc \
           nuget \
           referenceassemblies-pcl \
    && apt clean \
    && rm -rf /var/lib/apt/lists/* /tmp/*
  
WORKDIR $GOPATH/src/github.com/skycoin

COPY ./lib/restsharp/src/RestCSharp/packages.config .

RUN nuget install ./packages.config -o packages && rm -rf ./packages.config

VOLUME $GOPATH/src/
