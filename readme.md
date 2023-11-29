# orgcat

A little web server application to test out hosting services.

# Quick start
To run locally in a fresh dev environment:

- install .NET 7
- install docker

```sh
./rundb.sh
cd src/orgcat.postgresdb
dotnet ef database update
cd ..
dotnet run --project orgcat.web

# see what's in the db
docker ps   # get container id
docker exec -it CONTAINER_ID psql -U postgres -d orgcat
```

# The app
## functional requirements
- users
    - land on page ~/start/<some random ascii, 7 chars>
    - this starts a survey using the random ascii as the survey id
        - this is an intentional bug (create data on GET)
    - user answers survey questions
    - LATER: they can only do the survey once
    - LATER: they can continue the survey at any time
    - LATER: once all orgs are categorised, they get a thank you message
- admin
    - LATER: can download survey results at any time
        - no auth needed, anyone can view admin page

## non functional
- survey should initially load in < 1s
- survey pages should transition in < 1s
- contains a concurrency bug so that concurrent landing on a new
  survey id causes that id to become unusable
- separate staging and prod environments
- CD: deploy on push main


# Database
## Migrations
If you've changed the data model in `orgcat.postgresdb`, create a migration:

```sh
cd src/orgcat.postgresdb
dotnet ef migrations add <name of migration>
dotnet ef database update   # applies migrations to your local database
```


# To do
- survey
    - question:
        - save response on click next
            - fix db schema:
                - survey id must exist, foreign key
                - response cannot be empty
        - goto next question on click next
    - welcome: say welcome back if survey is already started
- inline todos
- remove dummies

# Maybe/later
- fuzz test via http?
- automate arch checks
    - web shouldn't reference db directly, apart from IoC setup