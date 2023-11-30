# orgcat

A little web server application to test out hosting services.

# Quick start
To run locally in a fresh dev environment:

- install .NET 7
- install docker

```sh
./db_reset.sh                           # wipes & recreates database
dotnet run --project src/orgcat.web     # runs the web server

# see what's in the db
docker exec -it orgcat_pg psql -U postgres -d orgcat

# to reproduce a concurrency bug:
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
  causes an error

# Infra requirements
- separate staging and prod environments
- CD: deploy on push main, goes to prod when tests pass
- structured logging, metrics
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

# Maybe/later
- implement viewing survey responses (no download)
- fuzz & load test via http
- get rid of new/existing distinction in domain types
- replace survey controller with start page
- automate arch checks
    - web shouldn't reference db directly, apart from IoC setup
    - db entities should be internal
