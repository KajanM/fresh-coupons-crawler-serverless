using Amazon.CDK;

namespace FreshCouponsBe
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new FreshCouponsBeStack(app, "FreshCouponsBeStack");
            app.Synth();
        }
    }
}