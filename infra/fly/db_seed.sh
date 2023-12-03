#!/bin/bash
source .secrets
sqlpath=$(realpath ../../src/orgcat.postgresdb)
MSYS_NO_PATHCONV=1 docker run -it -v $sqlpath:/var/lib/postgresql/data \
    postgres psql $NEONDB_PSQL_CONNSTRING -f /var/lib/postgresql/data/seeddb.sql
