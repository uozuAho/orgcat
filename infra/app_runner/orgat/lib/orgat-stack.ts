import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import { aws_apprunner as apprunner } from 'aws-cdk-lib';
import * as ecr from 'aws-cdk-lib/aws-ecr';

export class OrgatStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    const ecrRepo = new ecr.Repository(this, 'OrgCatRepo', {
        repositoryName: 'orgcat-repo',
        removalPolicy: cdk.RemovalPolicy.DESTROY,
    });

    const appRunner = new apprunner.CfnService(this, 'OrgCatRunner', {
        serviceName: 'orgcat',
        sourceConfiguration: {
            autoDeploymentsEnabled: true,
            authenticationConfiguration: {
                accessRoleArn: 'arn:aws:iam::123456789012:role/aws-service-role/apprunner.amazonaws.com/AWSServiceRoleForAppRunner',
            },
            imageRepository: {
                imageIdentifier: ecrRepo.repositoryUri,
                imageRepositoryType: 'ECR',
            },
        },
    });
  }
}
