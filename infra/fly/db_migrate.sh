#!/bin/bash
source .secrets
pushd ../../src
docker-compose build db-migrate
docker run -it src-db-migrate --connection $NEONDB_CONNSTRING
popd
