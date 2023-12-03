# Fly.io infrastructure deployment

# Get started
- sign up for fly.io
- create a database somewhere, eg neon. Add its connection string to a file
  named `.secrets` in this directory. See the [example file](.secrets.example).

```sh
fly apps create woz-orgcat
source .secrets
fly secrets set ConnectionStrings__OrgCatDb=$NEONDB_CONNSTRING
./db_migrate.sh
./db_seed.sh   # only required on first run
./deploy.sh
# check app:
# - https://woz-orgcat.fly.dev
# - `fly logs`, or logs at https://fly.io/apps/woz-orgcat/monitoring

# fly will scale down to zero with no usage.
# To destory the app and all resources:
./destroy.sh
```
