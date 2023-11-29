#!/bin/bash
#
# Seed DB test data

container_id=$(docker ps -q --filter ancestor=postgres)
docker cp seeddb.sql $container_id:/seeddb.sql
MSYS_NO_PATHCONV=1 docker exec $container_id psql -U postgres -d orgcat -f /seeddb.sql
