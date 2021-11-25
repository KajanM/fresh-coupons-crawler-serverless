using System;
using Amazon.CDK;
using Amazon.CDK.AWS.Events;
using Amazon.CDK.AWS.Events.Targets;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.SSM;

namespace FreshCouponsBe
{
    public class FreshCouponsBeStack : Stack
    {
        internal FreshCouponsBeStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var fetchAndSaveUdemyCouponsFunction = new Function(this, "FetchAndSaveUdemyCoupons", new FunctionProps
            {
                FunctionName = "FetchAndSaveUdemyCoupons",
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset(
                    "./FetchAndSaveUdemyCouponsHandler/src/FetchAndSaveUdemyCouponsHandler/bin/Release/netcoreapp3.1/publish"),
                Handler = "FetchAndSaveUdemyCouponsHandler::FetchAndSaveUdemyCouponsHandler.Function::FunctionHandler"
            });

            fetchAndSaveUdemyCouponsFunction.Role.AddManagedPolicy(
                ManagedPolicy.FromAwsManagedPolicyName("AmazonSSMReadOnlyAccess"));
            
            var branchParam = new StringParameter(this, "fc-branch", new StringParameterProps
            {
                Type = ParameterType.STRING,
                ParameterName = "fc.branch",
                StringValue = "<branch-name>"
            });
            branchParam.GrantRead(fetchAndSaveUdemyCouponsFunction.Role);
            var ownerParam = new StringParameter(this, "fc-owner", new StringParameterProps
            {
                Type = ParameterType.STRING,
                ParameterName = "fc.owner",
                StringValue = "<owner-name>"
            });
            ownerParam.GrantRead(fetchAndSaveUdemyCouponsFunction.Role);
            var repositoryParam = new StringParameter(this, "fc-repository", new StringParameterProps
            {
                Type = ParameterType.STRING,
                ParameterName = "fc.repository",
                StringValue = "<repository-name>"
            });
            repositoryParam.GrantRead(fetchAndSaveUdemyCouponsFunction.Role);
            new Rule(this, "fresh-coupons-schedule", new RuleProps
            {
                Schedule = Schedule.Cron(new CronOptions
                {
                    Day = "*",
                    Hour = "*",
                    Minute = "0",
                    Year = "*",
                    Month = "*",
                }),
                RuleName = "fresh-coupons-schedule",
                Targets = new []{new LambdaFunction(fetchAndSaveUdemyCouponsFunction)}
            });

            // var tokenParam = new StringParameter(this, "fc-token", new StringParameterProps
            // {
            //     Type = ParameterType.SECURE_STRING,
            //     ParameterName = "fc.token",
            //     StringValue = "<token-name>"
            // });
            // tokenParam.GrantRead(fetchAndSaveUdemyCouponsFunction.Role);
        }
    }
}