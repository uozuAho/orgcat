#!/bin/bash

set -eu

fly apps create woz-orgcat
source .secrets
fly secrets set ConnectionStrings__OrgCatDb=$NEONDB_CONNSTRING
./db_migrate.sh
./db_seed.sh
./deploy.sh
