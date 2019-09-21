os=$(uname -s)
if [[ $os == "Darwin" || $os == "Mac" ]]; then 
(make -C $GOPATH/src/github.com/skycoin/skycoin run-daemon ARGS="-db-read-only=true -disable-csrf=false -disable-networking=true -enable-all-api-sets=true -download-peerlist=false -db-path=$GOPATH/src/github.com/skycoin/skycoin/src/api/integration/testdata/blockchain-180.db" &)
sleep 1m
killall -9 skycoin
else
  (make -C $GOPATH/src/github.com/skycoin/skycoin run-daemon ARGS="-db-read-only=true -disable-csrf=false -disable-networking=true -enable-all-api-sets=true -download-peerlist=false -db-path=$GOPATH/src/github.com/skycoin/skycoin/src/api/integration/testdata/blockchain-180.db" &)
   make test-libsky-$TYPE
   make test-skyapi
   killall -9 skycoin
fi 
