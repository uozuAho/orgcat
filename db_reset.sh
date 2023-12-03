#!/bin/bash
# run a fresh db locally
pushd src/orgcat.postgresdb
docker stop orgcat_pg
./rundb.sh
dotnet ef database update
./seeddb.sh
popd
