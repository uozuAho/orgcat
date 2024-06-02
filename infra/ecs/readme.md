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
- database:
    - in ecs: orgcat.web.WebBuilder.ConfigureBuilder(WebApplicationBuilder builder) in /App/orgcat.web/WebBuilder.cs:line 15
        - connection string is empty
- db migrations
- add output: service url
