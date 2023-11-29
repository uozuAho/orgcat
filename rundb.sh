#!/bin/bash
docker run --rm -e POSTGRES_PASSWORD=asdfoot -e POSTGRES_USER=postgres -e POSTGRES_DB=orgcat -p 5432:5432 -d postgres