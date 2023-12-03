# Fly.io infrastructure deployment

# Get started
- sign up for fly.io
- create a database somewhere, eg neon. Get its connection string.

```sh
fly apps create woz-orgcat
fly secrets set ConnectionStrings__OrgCatDb=YOUR_CONNECTION_STRING
./deploy.sh
# check app:
# - https://woz-orgcat.fly.dev
# - logs at https://fly.io/apps/woz-orgcat/monitoring

# fly will scale down to zero with no usage.
# To destory the app and all resources:
./destroy.sh
```
