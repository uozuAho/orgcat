#!/bin/bash

set -eu

source .secrets
./db_migrate.sh
./deploy.sh
