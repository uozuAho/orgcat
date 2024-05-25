# Fly.io infrastructure deployment

# Get started
- sign up for fly.io
- create a database somewhere, eg neon. Add its connection string to a file
  named `.secrets` in this directory. See the [example file](.secrets.example).

```sh
# first time deployment:
./initial_deployment.sh
# all subsequent deployments:
./pipeline.sh
# check app:
# - https://woz-orgcat.fly.dev
# - `fly logs`, or logs at https://fly.io/apps/woz-orgcat/monitoring

# fly will scale down to zero with no usage.
# To destory the app and all resources:
./destroy.sh
```

# Fly assessment vs requirements
- **todo**: this
- survey should initially load in < 1s
- survey pages should transition in < 1s
- separate staging and prod environments
- CD: deploy on push main, goes to prod when tests pass
- logging, metrics
- fast rollback (skip full build + test, revert to previous working deployment)
- maybe: structured logs with nice querying
- maybe: tracing

# Thoughts
- very slick, deploying is easy and pretty fast, all from the console
- grafana metrics are nice: cpu, memory, http traffic, latency, responses
    - **todo**: are more metrics easily added with prometheus?
