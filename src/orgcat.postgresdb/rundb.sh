#!/bin/bash
docker run --rm --name orgcat_pg -e POSTGRES_PASSWORD=asdfoot -e POSTGRES_USER=postgres -e POSTGRES_DB=orgcat -p 5432:5432 -d postgres