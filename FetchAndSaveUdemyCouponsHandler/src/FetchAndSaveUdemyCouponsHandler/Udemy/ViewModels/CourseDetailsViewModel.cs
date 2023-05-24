namespace FetchAndSaveUdemyCouponsHandler.Shared.ViewModels
{
    public class CourseDetailsViewModel
    {
        public int CourseId { get; set; } // id

        public string Title { get; set; } // title

        public string ShortDescription { get; set; } // headline

        public string LongDescription { get; set; } // description

        public string Language { get; set; } // locale.locale; locale.simple_english_title

        public string CourseUri { get; set; } // url

        public string ImageUri { get; set; } // image_100x100; 750x422

        public string Duration { get; set; } // content_info

        public string EnrolledStudentsCount { get; set; } // num_subscribers; format with , and add students

        public string LastUpdated { get; set; } // todo

        public string[] TargetAudiences { get; set; } // who_should_attend_data.items; target_audiences

        public string[] Tags { get; set; } // course_has_labels.label.display_name


        public Rating Rating { get; set; }

        public Instructor[] Instructors { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(CourseId)}: {CourseId}, {nameof(Title)}: {Title}, {nameof(ShortDescription)}: {ShortDescription}, {nameof(LongDescription)}: {LongDescription}, {nameof(Language)}: {Language}, {nameof(CourseUri)}: {CourseUri}, {nameof(ImageUri)}: {ImageUri}, {nameof(Duration)}: {Duration}, {nameof(EnrolledStudentsCount)}: {EnrolledStudentsCount}, {nameof(LastUpdated)}: {LastUpdated}, {nameof(TargetAudiences)}: {TargetAudiences}, {nameof(Tags)}: {Tags}, {nameof(Rating)}: {Rating}, {nameof(Instructors)}: {Instructors}";
        }
    }

    public class Rating
    {
        public int? Count { get; set; } // num_reviews

        public string AverageValue { get; set; } // rating

        public override string ToString()
        {
            return $"{nameof(Count)}: {Count}, {nameof(AverageValue)}: {AverageValue}";
        }
    }

    public class Instructor
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public int? TotalNumberOfStudents { get; set; } // todo

        public int? TotalNumberOfReviews { get; set; } // todo

        public double? AverageRating { get; set; } // todo

        public override string ToString()
        {
            return
                $"{nameof(Name)}: {Name}, {nameof(Url)}: {Url}, {nameof(TotalNumberOfStudents)}: {TotalNumberOfStudents}, {nameof(TotalNumberOfReviews)}: {TotalNumberOfReviews}, {nameof(AverageRating)}: {AverageRating}";
        }
    }
}