#!/bin/bash
# run a fresh test db locally

set -eu

# restart db
docker stop orgcat_pg_test
docker run --rm --name orgcat_pg_test -e POSTGRES_PASSWORD=footest -e \
    POSTGRES_USER=postgres -e POSTGRES_DB=orgcat_test -p 5433:5432 -d postgres

# run migrations
pushd ../orgcat.postgresdb
dotnet ef database update \
    --connection "Host=localhost;Port=5433;Database=orgcat_test;Username=postgres;Password=footest"
popd

# seed data
docker cp seed_test_data.sql orgcat_pg_test:/seed_test_data.sql
MSYS_NO_PATHCONV=1 docker exec orgcat_pg_test psql -U postgres -d orgcat_test -f /seed_test_data.sql
