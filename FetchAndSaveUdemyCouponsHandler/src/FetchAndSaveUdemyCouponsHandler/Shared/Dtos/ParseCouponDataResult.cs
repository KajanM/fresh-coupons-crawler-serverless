namespace FetchAndSaveUdemyCouponsHandler.Shared.Dtos
{
    public class ParseCouponDataResult : BaseResult
    {
        public UdemyUrlWithCouponCode Coupon { get; set; }

        public override string ToString()
        {
            return $"{nameof(Coupon)}: {Coupon}";
        }
    }
}