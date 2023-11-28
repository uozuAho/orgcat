# orgcat

A little web server application to test out hosting services.

# Quick start
To run locally in a fresh dev environment:

**todo**:
```sh
```

# The app
## functional requirements
- users
    - land on page ~/start/<some random ascii, 7 chars>
    - this starts a survey using the random ascii as the survey id
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
- POC: show db data on a page
- implement survey stuff
- inline todos