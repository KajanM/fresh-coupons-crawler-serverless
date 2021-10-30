using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;

namespace FreshCouponsBe
{
    public class FreshCouponsBeStack : Stack
    {
        internal FreshCouponsBeStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            new Function(this, "FetchAndSaveUdemyCoupons", new FunctionProps
            {
                FunctionName = "FetchAndSaveUdemyCoupons",
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset(
                    "./FetchAndSaveUdemyCouponsHandler/src/FetchAndSaveUdemyCouponsHandler/bin/Release/netcoreapp3.1/publish"),
                Handler = "FetchAndSaveUdemyCouponsHandler::FetchAndSaveUdemyCouponsHandler.Function::FunctionHandler"
            });
        }
    }
}