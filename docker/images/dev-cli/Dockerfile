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

# Define some important environment variables
ENV MONO_VERSION 4.8.0.524
ENV DEBIAN_FRONTEND noninteractive

# Add mono and dotnet repos and install necessary packages
RUN apt update \
    && apt upgrade -y \
    && apt install -y curl \
           apt-transport-https \
           dirmngr \
    && curl -sSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor | apt-key add - \
    && apt-key adv --no-tty --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF \
    && echo "deb [arch=amd64] https://packages.microsoft.com/debian/9/prod stretch main" > /etc/apt/sources.list.d/microsoft-dotnet.list \
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
           dotnet-sdk-2.2 \
    && apt clean \
    && rm -rf /var/lib/apt/lists/* /tmp/*
  
WORKDIR $GOPATH/src/github.com/skycoin

VOLUME $GOPATH/src/
