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


# To do
- fix the bug
    - DONE: unique constraint on response id
    - DONE: transaction for check and create
    - test with curl
- create a run sheet to reproduce & fix bug
    - users complain they can't do survey
    - check logs, metrics, traces
    - reproduce locally
    - reproduce in staging
    - fix, confirm locally
    - confirm fixed in staging
        - fix any stuck surveys in staging
        - run db migration (won't work if dupe ids exist)
    - confirm fixed in prod
    - fix any stuck surveys in prod
- check fly + neon infra vs requirements
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
- set log level to warn (appsettings)
- fuzz & load test via http
- remove test project(s) from docker image
- implement viewing survey responses (no download)
- get rid of new/existing distinction in domain types
- replace survey controller with start page
- automate arch checks
    - web shouldn't reference db directly, apart from IoC setup
    - db entities should be internal
