using System;

namespace FetchAndSaveUdemyCouponsHandler.Shared.Dtos
{
    public class UdemyUrlWithCouponCode
    {
        public string Url { get; set; }
        
        public string CouponCode { get; set; }

        public bool IsAlreadyAFreeCourse { get; set; }

        protected bool Equals(UdemyUrlWithCouponCode other)
        {
            return Url == other.Url && CouponCode == other.CouponCode && IsAlreadyAFreeCourse == other.IsAlreadyAFreeCourse;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UdemyUrlWithCouponCode)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Url, CouponCode, IsAlreadyAFreeCourse);
        }

        public static bool operator ==(UdemyUrlWithCouponCode left, UdemyUrlWithCouponCode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UdemyUrlWithCouponCode left, UdemyUrlWithCouponCode right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"{nameof(Url)}: {Url}, {nameof(CouponCode)}: {CouponCode}";
        }
    }
}