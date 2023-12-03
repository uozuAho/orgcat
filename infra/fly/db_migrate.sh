#!/bin/bash

set -eu

source .secrets
pushd ../../src
docker-compose build db-migrate
docker run -it src-db-migrate --connection $NEONDB_CONNSTRING
popd
