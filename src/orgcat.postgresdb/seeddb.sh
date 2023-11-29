#!/bin/bash
#
# Seed DB test data

docker cp seeddb.sql orgcat_pg:/seeddb.sql
MSYS_NO_PATHCONV=1 docker exec orgcat_pg psql -U postgres -d orgcat -f /seeddb.sql
