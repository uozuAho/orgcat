# AWS ECS infrastructure

# Requirements
- nodejs (tested with v20)
- docker
- aws cdk (npm i -g aws-cdk)
- an AWS account with cli access

# infra commands
```sh
cdk deploy
cdk destroy
```

# todo
- follow this first: https://docs.aws.amazon.com/AmazonECS/latest/developerguide/tutorial-ecs-web-server-cdk.html
    - in new infra project
- set removal policy of all resources to destroy
- database:
    - in ecs: orgcat.web.WebBuilder.ConfigureBuilder(WebApplicationBuilder builder) in /App/orgcat.web/WebBuilder.cs:line 15
        - connection string is empty
- db migrations
- add output: service url, log group (plus command to view logs)

# thoughts
- using 'raw' cdk is harder to get up and running than fly. Very configurable,
  tutorials use ECS pattern libraries instead of 'raw' cdk.
