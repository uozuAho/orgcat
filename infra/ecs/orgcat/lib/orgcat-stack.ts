import * as cdk from 'aws-cdk-lib';
import * as ec2 from 'aws-cdk-lib/aws-ec2';
import * as ecs from 'aws-cdk-lib/aws-ecs';
import { Construct } from 'constructs';
import { DockerImageAsset } from 'aws-cdk-lib/aws-ecr-assets';
import path = require('path');

const projectRoot = path.resolve(__dirname, '../../../..');
const srcDir = path.join(projectRoot, 'src');

export class OrgcatStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    const vpc = new ec2.Vpc(this, 'MyVpc', { maxAzs: 2 });
    const cluster = new ecs.Cluster(this, 'Cluster', { vpc });
    const logging = new ecs.AwsLogDriver({
        streamPrefix: "orgcat",
        logRetention: 1,
    });
    const appImage = new DockerImageAsset(this, 'AppImage', {
        directory: srcDir,
    });
    const taskDef = new ecs.FargateTaskDefinition(this, "TaskDef");
    taskDef.addContainer("AppContainer", {
      image: ecs.ContainerImage.fromDockerImageAsset(appImage),
      memoryLimitMiB: 512,
      logging,
    });
    new ecs.FargateService(this, "Service", {
      cluster,
      taskDefinition: taskDef,
      assignPublicIp: true,
    });
  }
}
