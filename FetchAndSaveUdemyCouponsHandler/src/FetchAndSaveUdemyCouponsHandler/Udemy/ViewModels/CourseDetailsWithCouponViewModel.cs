namespace FetchAndSaveUdemyCouponsHandler.Shared.ViewModels
{
    public class CourseDetailsWithCouponViewModel
    {
        public CourseDetailsViewModel CourseDetails { get; set; }
        
        public bool IsAlreadyAFreeCourse { get; set; }
        
        public CouponData CouponData { get; set; }

        public override string ToString()
        {
            return $"{nameof(CourseDetails)}: {CourseDetails}, {nameof(IsAlreadyAFreeCourse)}: {IsAlreadyAFreeCourse}, {nameof(CouponData)}: {CouponData}";
        }
    }

    public class CouponData
    {
        public string CouponCode { get; set; }

        public string OriginalPrice { get; set; }

        public string DiscountedPrice { get; set; }

        public string ExpirationText { get; set; }

        public int DiscountPercentage { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(CouponCode)}: {CouponCode}, {nameof(OriginalPrice)}: {OriginalPrice}, {nameof(DiscountedPrice)}: {DiscountedPrice}, {nameof(ExpirationText)}: {ExpirationText}, {nameof(DiscountPercentage)}: {DiscountPercentage}";
        }
    }
}