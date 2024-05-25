# orgcat

A little buggy web server + DB application to test out hosting services. The
goal is to be able to test hosting services for ease of (re)deployment,
monitoring and debugging and more.

# Quick start
To run locally in a fresh dev environment:

- install .NET 7
- install docker

```sh
# Dev:
./db_reset.sh                           # wipes & recreates database
dotnet run --project src/orgcat.web     # runs the web server

# see what's in the db
docker exec -it orgcat_pg psql -U postgres -d orgcat


# Run locally in a production-like mode in docker
cd src
docker-compose build && docker-compose up -d
cd orgcat.postgresdb
docker cp seeddb.sql src-db-1:/seeddb.sql
MSYS_NO_PATHCONV=1 docker exec src-db-1 psql -U postgres -d orgcat -f /seeddb.sql
# goto localhost:5057


# Deployment:
# See readmes under infra/ for deployment options.


# Other stuff:
# to reproduce the concurrency bug that makes a new survey id unusable:
seq 1 2 | xargs -n1 -P3 curl localhost:5056/survey/start/<some_new_id>
```

# The app
## functional requirements
- users
    - land on page ~/survey/start/<some random ascii, 7 chars>
    - this starts a survey using the random ascii as the survey id
        - this is an intentional bad practice (create data on GET)
    - user answers survey questions
    - once questions are answered, they get a thank you message
    - they can continue the survey at any time
    - they can only do the survey once
- admin
    - LATER: can view survey results at any time
        - no auth needed, anyone can view admin page

## non functional
- survey should initially load in < 1s
- survey pages should transition in < 1s
- contains a concurrency bug so that concurrent landing on a new survey id
  causes an error that makes that survey id unusable

# Infra requirements
- at least know how to do these, don't have to implement them
- separate staging and prod environments
- CD: deploy on push main, goes to prod when tests pass
- logging, metrics
- fast rollback (skip full build, revert to previous working deployment)
- maybe: structured logs with nice querying
- maybe: tracing


# Database
## Migrations
If you've changed the data model in `orgcat.postgresdb`, create a migration:

```sh
cd src/orgcat.postgresdb
dotnet ef migrations add <name of migration>
dotnet ef database update   # applies migrations to your local database
```


# Testing out hosting services
Deploy the site + DB, then go through the [bug runsheet](./bug_runsheet.md).
Pretend you don't know what the bug is. How easy is it to restore service and
fix the bug?


# To do
- check fly + neon infra vs requirements
- set log level to warn (appsettings)
    - update concbug tag with this
- try other deployment options
    - app
        - app runner
        - ecs + EC2
        - ECS + fargate
        - more aws? amplify?
        - more container runners?
        - lambda?
        - EC2 (maybe later, not a likely contender)
    - db
        - supabase
        - fly postgres
        - fly litefs (replicated sqlite)
        - aurora serverless
        - RDS

# Maybe/later
- assess observability options
    - add structured logging
    - add tracing?
    - add asp http metrics (still in prom package?)
    - do any services give you errors by endpoint out of the box? akita?
- fuzz & load test via http
- assess CICD tools: buildkite, github actions, aws, more?
