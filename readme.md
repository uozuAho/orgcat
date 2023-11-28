# orgcat

A little web server application to test out hosting services.

# Quick start
To run locally in a fresh dev environment:

- install .NET 7
- install docker

```sh
./rundb.sh
dotnet run --project orgcat.web
```

# The app
## functional requirements
- users
    - land on page ~/start/<some random ascii, 7 chars>
    - this starts a survey using the random ascii as the survey id
        - this is an intentional bug (create data on GET)
    - user categorises orgs
    - they can only do the survey once
    - they can continue the survey at any time
    - once all orgs are categorised, they get a thank you message
- admin
    - can download survey results at any time
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
- implement survey stuff
- inline todos

# Maybe/later
- automate arch checks
    - web shouldn't reference db directly, apart from IoC setup