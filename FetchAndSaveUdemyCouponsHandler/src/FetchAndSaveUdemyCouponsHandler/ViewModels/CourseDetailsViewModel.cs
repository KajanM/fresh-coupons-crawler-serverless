namespace FetchAndSaveUdemyCouponsHandler.ViewModels
{
    public class CourseDetailsViewModel
    {
        public int CourseId { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string Language { get; set; }

        public string CourseUri { get; set; }

        public string ImageUri { get; set; }

        public string Duration { get; set; }

        public string EnrolledStudentsCount { get; set; }

        public string LastUpdated { get; set; }

        public string[] TargetAudiences { get; set; }

        public string[] Tags { get; set; }

        public CouponData CouponData { get; set; }

        public Rating Rating { get; set; }

        public Instructor[] Instructors { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(CourseId)}: {CourseId}, {nameof(Title)}: {Title}, {nameof(ShortDescription)}: {ShortDescription}, {nameof(LongDescription)}: {LongDescription}, {nameof(Language)}: {Language}, {nameof(CourseUri)}: {CourseUri}, {nameof(ImageUri)}: {ImageUri}, {nameof(Duration)}: {Duration}, {nameof(EnrolledStudentsCount)}: {EnrolledStudentsCount}, {nameof(LastUpdated)}: {LastUpdated}, {nameof(TargetAudiences)}: {TargetAudiences}, {nameof(Tags)}: {Tags}, {nameof(CouponData)}: {CouponData}, {nameof(Rating)}: {Rating}, {nameof(Instructors)}: {Instructors}";
        }
    }

    public class CouponData
    {
        public string CouponCode { get; set; }

        public double OriginalPrice { get; set; }

        public double DiscountedPrice { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(CouponCode)}: {CouponCode}, {nameof(OriginalPrice)}: {OriginalPrice}, {nameof(DiscountedPrice)}: {DiscountedPrice}";
        }
    }

    public class Rating
    {
        public int Count { get; set; }

        public string AverageValue { get; set; }

        public override string ToString()
        {
            return $"{nameof(Count)}: {Count}, {nameof(AverageValue)}: {AverageValue}";
        }
    }

    public class Instructor
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public int TotalNumberOfStudents { get; set; }

        public int TotalNumberOfReviews { get; set; }

        public double AverageRating { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Name)}: {Name}, {nameof(Url)}: {Url}, {nameof(TotalNumberOfStudents)}: {TotalNumberOfStudents}, {nameof(TotalNumberOfReviews)}: {TotalNumberOfReviews}, {nameof(AverageRating)}: {AverageRating}";
        }
    }
}