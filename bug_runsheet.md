# Bug run sheet

# Setup
- deploy tag `concbug`
    - if you've already deployed later db migrations, you'll need to drop an
      recreate the database.

# Go
- users complain they can't do survey, eg. they click on a link to
  ~/survey/start/abcd, and just get an error page with a request id
- check logs, metrics, traces
- reproduce in staging. Hint: try clicking on a link to a new survey multiple
  times quickly
- reproduce locally
- fix, confirm locally
    - the quick fix: deploy tag `fixed-concbug`
- confirm fixed in staging
    - fix any stuck surveys in staging
    - run db migration (won't work if dupe ids exist)
- fix any stuck surveys in prod
- deploy to prod
- confirm fixed in prod
