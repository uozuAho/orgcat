# orgcat.web.test

Headless/: these tests use the web app configuration, but not the web
app itself. These are for testing business logic using the web app's
configuration, without needing to use HTTP.

# Run tests
```sh
./reset_test_db.sh    # needed before each test run
dotnet test           # or use rider/vs

# See what's in the db
docker exec -it orgcat_pg_test psql -U postgres -d orgcat_test
```
