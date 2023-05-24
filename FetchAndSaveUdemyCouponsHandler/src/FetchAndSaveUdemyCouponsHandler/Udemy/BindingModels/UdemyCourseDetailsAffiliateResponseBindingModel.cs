using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FetchAndSaveUdemyCouponsHandler.Udemy.BindingModels;

    public class AppleInAppPriceDetail
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("price_string")]
        public string PriceString { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class Badge
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("badge_text")]
        public string BadgeText { get; set; }

        [JsonProperty("badge_family")]
        public string BadgeFamily { get; set; }

        [JsonProperty("context_info")]
        public ContextInfo ContextInfo { get; set; }
    }

    public class Buyable
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Campaign
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        [JsonProperty("is_instructor_created")]
        public bool IsInstructorCreated { get; set; }

        [JsonProperty("is_public")]
        public bool IsPublic { get; set; }

        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("campaign_type")]
        public string CampaignType { get; set; }

        [JsonProperty("uses_remaining")]
        public object UsesRemaining { get; set; }

        [JsonProperty("maximum_uses")]
        public object MaximumUses { get; set; }

        [JsonProperty("show_code")]
        public bool ShowCode { get; set; }
    }

    public class CaptionLocale
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("english_title")]
        public string EnglishTitle { get; set; }
    }

    public class Category
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("tracking_object_type")]
        public string TrackingObjectType { get; set; }
    }

    public class ContentCollection
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public class ContextInfo
    {
        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("subcategory")]
        public object Subcategory { get; set; }

        [JsonProperty("label")]
        public Label Label { get; set; }
    }

    public class CourseHasLabel
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("label")]
        public Label Label { get; set; }

        [JsonProperty("is_primary")]
        public bool IsPrimary { get; set; }
    }

    public class Discount
    {
        [JsonProperty("price_serve_tracking_id")]
        public string PriceServeTrackingId { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("list_price")]
        public ListPrice ListPrice { get; set; }

        [JsonProperty("saving_price")]
        public SavingPrice SavingPrice { get; set; }

        [JsonProperty("has_discount_saving")]
        public bool HasDiscountSaving { get; set; }

        [JsonProperty("discount_percent")]
        public int? DiscountPercent { get; set; }

        [JsonProperty("discount_percent_for_display")]
        public int? DiscountPercentForDisplay { get; set; }

        [JsonProperty("buyable")]
        public Buyable Buyable { get; set; }

        [JsonProperty("campaign")]
        public Campaign Campaign { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("is_public")]
        public bool IsPublic { get; set; }
    }

    public class DiscountPrice
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("price_string")]
        public string PriceString { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class DownloadUrls
    {
        [JsonProperty("Video")]
        public List<Video> Video { get; set; }
    }

    public class Faq
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }
    }

    public class Features
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("discussions_create")]
        public bool DiscussionsCreate { get; set; }

        [JsonProperty("discussions_view")]
        public bool DiscussionsView { get; set; }

        [JsonProperty("discussions_replies_create")]
        public bool DiscussionsRepliesCreate { get; set; }

        [JsonProperty("enroll")]
        public bool Enroll { get; set; }

        [JsonProperty("reviews_create")]
        public bool ReviewsCreate { get; set; }

        [JsonProperty("reviews_view")]
        public bool ReviewsView { get; set; }

        [JsonProperty("reviews_responses_create")]
        public bool ReviewsResponsesCreate { get; set; }

        [JsonProperty("announcements_comments_view")]
        public bool AnnouncementsCommentsView { get; set; }

        [JsonProperty("educational_announcements_create")]
        public bool EducationalAnnouncementsCreate { get; set; }

        [JsonProperty("promotional_announcements_create")]
        public bool PromotionalAnnouncementsCreate { get; set; }

        [JsonProperty("promotions_create")]
        public bool PromotionsCreate { get; set; }

        [JsonProperty("promotions_view")]
        public bool PromotionsView { get; set; }

        [JsonProperty("students_view")]
        public bool StudentsView { get; set; }
    }

    public class GoogleInAppPriceDetail
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("price_string")]
        public string PriceString { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class IntendedCategory
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("icon_class")]
        public string IconClass { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title_cleaned")]
        public string TitleCleaned { get; set; }

        [JsonProperty("channel_id")]
        public int? ChannelId { get; set; }
    }

    public class Label
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("icon_class")]
        public string IconClass { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("topic_channel_url")]
        public string TopicChannelUrl { get; set; }

        [JsonProperty("tracking_object_type")]
        public string TrackingObjectType { get; set; }
    }

    public class ListPrice
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("price_string")]
        public string PriceString { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class Locale
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("locale")]
        public string LocaleStr { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("english_title")]
        public string EnglishTitle { get; set; }

        [JsonProperty("simple_english_title")]
        public string SimpleEnglishTitle { get; set; }
    }

    public class NotificationSettings
    {
    }

    public class Price
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("price_string")]
        public string PriceString { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class PriceDetail
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("price_string")]
        public string PriceString { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class PrimaryCategory
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("icon_class")]
        public string IconClass { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title_cleaned")]
        public string TitleCleaned { get; set; }

        [JsonProperty("channel_id")]
        public int? ChannelId { get; set; }
    }

    public class PrimarySubcategory
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_cleaned")]
        public string TitleCleaned { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("icon_class")]
        public string IconClass { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("channel_id")]
        public object ChannelId { get; set; }

        [JsonProperty("_class")]
        public string Class { get; set; }
    }

    public class PromoAsset
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("asset_type")]
        public string AssetType { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_cleaned")]
        public string TitleCleaned { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("length")]
        public int? Length { get; set; }

        [JsonProperty("status")]
        public int? Status { get; set; }

        [JsonProperty("download_urls")]
        public DownloadUrls DownloadUrls { get; set; }
    }

    public class QualityReviewProcess
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("score")]
        public string Score { get; set; }

        [JsonProperty("admin_rating")]
        public int? AdminRating { get; set; }
    }

    public class RatingDistribution
    {
        [JsonProperty("rating")]
        public int? Rating { get; set; }

        [JsonProperty("count")]
        public int? Count { get; set; }
    }

    public class RequirementsData
    {
        [JsonProperty("items")]
        public List<string> Items { get; set; }
    }

    public class UdemyCourseDetailsAffiliateResponseBindingModel
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// "/course/mobile-app-development-for-people-with-autism-dyslexia-etc/"
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("is_paid")]
        public bool IsPaid { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("price_detail")]
        public PriceDetail PriceDetail { get; set; }

        [JsonProperty("price_serve_tracking_id")]
        public string PriceServeTrackingId { get; set; }

        [JsonProperty("visible_instructors")]
        public List<VisibleInstructor> VisibleInstructors { get; set; }

        [JsonProperty("image_125_H")]
        public string Image125H { get; set; }

        [JsonProperty("image_240x135")]
        public string Image240x135 { get; set; }

        [JsonProperty("is_practice_test_course")]
        public bool IsPracticeTestCourse { get; set; }

        [JsonProperty("image_480x270")]
        public string Image480x270 { get; set; }

        [JsonProperty("published_title")]
        public string PublishedTitle { get; set; }

        [JsonProperty("tracking_id")]
        public string TrackingId { get; set; }

        [JsonProperty("locale")]
        public Locale Locale { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("headline")]
        public string Headline { get; set; }

        [JsonProperty("num_subscribers")]
        public int? NumSubscribers { get; set; }

        [JsonProperty("caption_locales")]
        public List<CaptionLocale> CaptionLocales { get; set; }

        [JsonProperty("discount")]
        public Discount Discount { get; set; }

        [JsonProperty("discount_price")]
        public DiscountPrice DiscountPrice { get; set; }

        [JsonProperty("avg_rating")]
        public double AvgRating { get; set; }

        [JsonProperty("avg_rating_recent")]
        public double AvgRatingRecent { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("num_reviews")]
        public int? NumReviews { get; set; }

        [JsonProperty("num_reviews_recent")]
        public int? NumReviewsRecent { get; set; }

        [JsonProperty("rating_distribution")]
        public List<RatingDistribution> RatingDistribution { get; set; }

        [JsonProperty("favorite_time")]
        public object FavoriteTime { get; set; }

        [JsonProperty("archive_time")]
        public object ArchiveTime { get; set; }

        [JsonProperty("earnings")]
        public string Earnings { get; set; }

        [JsonProperty("completion_ratio")]
        public int? CompletionRatio { get; set; }

        [JsonProperty("is_wishlisted")]
        public bool IsWishlisted { get; set; }

        [JsonProperty("num_quizzes")]
        public int? NumQuizzes { get; set; }

        [JsonProperty("num_lectures")]
        public int? NumLectures { get; set; }

        [JsonProperty("num_published_lectures")]
        public int? NumPublishedLectures { get; set; }

        [JsonProperty("num_published_quizzes")]
        public int? NumPublishedQuizzes { get; set; }

        [JsonProperty("num_curriculum_items")]
        public int? NumCurriculumItems { get; set; }

        [JsonProperty("num_of_published_curriculum_objects")]
        public int? NumOfPublishedCurriculumObjects { get; set; }

        [JsonProperty("num_cpe_credits")]
        public object NumCpeCredits { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("num_practice_tests")]
        public int? NumPracticeTests { get; set; }

        [JsonProperty("num_published_practice_tests")]
        public int? NumPublishedPracticeTests { get; set; }

        [JsonProperty("original_price_text")]
        public string OriginalPriceText { get; set; }

        [JsonProperty("quality_status")]
        public string QualityStatus { get; set; }

        [JsonProperty("status_label")]
        public string StatusLabel { get; set; }

        [JsonProperty("can_edit")]
        public bool CanEdit { get; set; }

        [JsonProperty("features")]
        public Features Features { get; set; }

        [JsonProperty("gift_url")]
        public string GiftUrl { get; set; }

        [JsonProperty("num_invitation_requests")]
        public int? NumInvitationRequests { get; set; }

        [JsonProperty("notification_settings")]
        public NotificationSettings NotificationSettings { get; set; }

        [JsonProperty("is_banned")]
        public bool IsBanned { get; set; }

        [JsonProperty("is_published")]
        public bool IsPublished { get; set; }

        [JsonProperty("intended_category")]
        public IntendedCategory IntendedCategory { get; set; }

        [JsonProperty("image_48x27")]
        public string Image48x27 { get; set; }

        [JsonProperty("image_50x50")]
        public string Image50x50 { get; set; }

        [JsonProperty("image_75x75")]
        public string Image75x75 { get; set; }

        [JsonProperty("image_96x54")]
        public string Image96x54 { get; set; }

        [JsonProperty("image_100x100")]
        public string Image100x100 { get; set; }

        [JsonProperty("image_200_H")]
        public string Image200H { get; set; }

        [JsonProperty("image_304x171")]
        public string Image304x171 { get; set; }

        [JsonProperty("image_750x422")]
        public string Image750x422 { get; set; }

        [JsonProperty("has_certificate")]
        public bool HasCertificate { get; set; }

        [JsonProperty("primary_category")]
        public PrimaryCategory PrimaryCategory { get; set; }

        [JsonProperty("primary_subcategory")]
        public PrimarySubcategory PrimarySubcategory { get; set; }

        [JsonProperty("is_enrollable_on_mobile")]
        public bool IsEnrollableOnMobile { get; set; }

        [JsonProperty("is_in_any_ufb_content_collection")]
        public bool IsInAnyUfbContentCollection { get; set; }

        [JsonProperty("is_in_user_subscription")]
        public bool IsInUserSubscription { get; set; }

        [JsonProperty("is_in_subscribed_content_collections")]
        public bool IsInSubscribedContentCollections { get; set; }

        [JsonProperty("has_closed_caption")]
        public bool HasClosedCaption { get; set; }

        [JsonProperty("caption_languages")]
        public List<string> CaptionLanguages { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("instructional_level")]
        public string InstructionalLevel { get; set; }

        [JsonProperty("instructional_level_simple")]
        public string InstructionalLevelSimple { get; set; }

        [JsonProperty("estimated_content_length")]
        public int? EstimatedContentLength { get; set; }

        [JsonProperty("content_info")]
        public string ContentInfo { get; set; }

        [JsonProperty("content_info_short")]
        public string ContentInfoShort { get; set; }

        [JsonProperty("content_length_practice_test_questions")]
        public int? ContentLengthPracticeTestQuestions { get; set; }

        [JsonProperty("requirements_data")]
        public RequirementsData RequirementsData { get; set; }

        [JsonProperty("what_you_will_learn_data")]
        public WhatYouWillLearnData WhatYouWillLearnData { get; set; }

        [JsonProperty("who_should_attend_data")]
        public WhoShouldAttendData WhoShouldAttendData { get; set; }

        [JsonProperty("is_available_on_google_app")]
        public bool IsAvailableOnGoogleApp { get; set; }

        [JsonProperty("organization_id")]
        public object OrganizationId { get; set; }

        [JsonProperty("google_in_app_purchase_price_text")]
        public string GoogleInAppPurchasePriceText { get; set; }

        [JsonProperty("promo_asset")]
        public PromoAsset PromoAsset { get; set; }

        [JsonProperty("is_user_subscribed")]
        public bool IsUserSubscribed { get; set; }

        [JsonProperty("apple_in_app_product_id")]
        public string AppleInAppProductId { get; set; }

        [JsonProperty("is_available_on_ios")]
        public bool IsAvailableOnIos { get; set; }

        [JsonProperty("google_in_app_product_id")]
        public string GoogleInAppProductId { get; set; }

        [JsonProperty("faq")]
        public List<Faq> Faq { get; set; }

        [JsonProperty("apple_in_app_purchase_price_text")]
        public string AppleInAppPurchasePriceText { get; set; }

        [JsonProperty("type_label")]
        public string TypeLabel { get; set; }

        [JsonProperty("google_in_app_price_detail")]
        public GoogleInAppPriceDetail GoogleInAppPriceDetail { get; set; }

        [JsonProperty("apple_in_app_price_detail")]
        public AppleInAppPriceDetail AppleInAppPriceDetail { get; set; }

        [JsonProperty("quality_review_process")]
        public QualityReviewProcess QualityReviewProcess { get; set; }

        [JsonProperty("is_organization_only")]
        public bool IsOrganizationOnly { get; set; }

        [JsonProperty("is_cpe_compliant")]
        public bool IsCpeCompliant { get; set; }

        [JsonProperty("cpe_field_of_study")]
        public object CpeFieldOfStudy { get; set; }

        [JsonProperty("cpe_program_level")]
        public object CpeProgramLevel { get; set; }

        [JsonProperty("was_ever_published")]
        public bool WasEverPublished { get; set; }

        [JsonProperty("buyable_object_type")]
        public string BuyableObjectType { get; set; }

        [JsonProperty("published_time")]
        public DateTime PublishedTime { get; set; }

        [JsonProperty("is_marketing_boost_agreed")]
        public bool IsMarketingBoostAgreed { get; set; }

        [JsonProperty("is_owned_by_instructor_team")]
        public bool IsOwnedByInstructorTeam { get; set; }

        [JsonProperty("is_owner_terms_banned")]
        public bool IsOwnerTermsBanned { get; set; }

        [JsonProperty("is_taking_disabled")]
        public bool IsTakingDisabled { get; set; }

        [JsonProperty("content_length_video")]
        public int? ContentLengthVideo { get; set; }

        [JsonProperty("checkout_url")]
        public string CheckoutUrl { get; set; }

        [JsonProperty("prerequisites")]
        public List<string> Prerequisites { get; set; }

        [JsonProperty("objectives")]
        public List<string> Objectives { get; set; }

        [JsonProperty("objectives_summary")]
        public List<string> ObjectivesSummary { get; set; }

        [JsonProperty("target_audiences")]
        public List<string> TargetAudiences { get; set; }

        [JsonProperty("last_accessed_time")]
        public object LastAccessedTime { get; set; }

        [JsonProperty("enrollment_time")]
        public object EnrollmentTime { get; set; }

        [JsonProperty("course_has_labels")]
        public List<CourseHasLabel> CourseHasLabels { get; set; }

        [JsonProperty("bestseller_badge_content")]
        public object BestsellerBadgeContent { get; set; }

        [JsonProperty("badges")]
        public List<Badge> Badges { get; set; }

        [JsonProperty("free_course_subscribe_url")]
        public object FreeCourseSubscribeUrl { get; set; }

        [JsonProperty("is_recently_published")]
        public bool IsRecentlyPublished { get; set; }

        [JsonProperty("last_update_date")]
        public string LastUpdateDate { get; set; }

        [JsonProperty("num_article_assets")]
        public int? NumArticleAssets { get; set; }

        [JsonProperty("num_coding_exercises")]
        public int? NumCodingExercises { get; set; }

        [JsonProperty("num_assignments")]
        public int? NumAssignments { get; set; }

        [JsonProperty("num_additional_assets")]
        public int? NumAdditionalAssets { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }

        [JsonProperty("landing_preview_as_guest_url")]
        public string LandingPreviewAsGuestUrl { get; set; }

        [JsonProperty("context_info")]
        public ContextInfo ContextInfo { get; set; }

        [JsonProperty("has_sufficient_preview_length")]
        public bool HasSufficientPreviewLength { get; set; }

        [JsonProperty("has_org_only_setting")]
        public bool HasOrgOnlySetting { get; set; }

        [JsonProperty("has_labs_in_course_prompt_setting")]
        public bool HasLabsInCoursePromptSetting { get; set; }

        [JsonProperty("is_draft")]
        public bool IsDraft { get; set; }

        [JsonProperty("common_review_attributes")]
        public List<object> CommonReviewAttributes { get; set; }

        [JsonProperty("custom_category_ids")]
        public List<object> CustomCategoryIds { get; set; }

        [JsonProperty("alternate_redirect_course_id")]
        public int? AlternateRedirectCourseId { get; set; }

        [JsonProperty("is_approved")]
        public bool IsApproved { get; set; }

        [JsonProperty("is_organization_eligible")]
        public bool IsOrganizationEligible { get; set; }

        [JsonProperty("instructor_status")]
        public object InstructorStatus { get; set; }

        [JsonProperty("available_features")]
        public List<string> AvailableFeatures { get; set; }

        [JsonProperty("enroll_url")]
        public string EnrollUrl { get; set; }

        [JsonProperty("learn_url")]
        public string LearnUrl { get; set; }

        [JsonProperty("retirement_date")]
        public object RetirementDate { get; set; }

        [JsonProperty("is_course_available_in_org")]
        public object IsCourseAvailableInOrg { get; set; }

        [JsonProperty("is_in_personal_plan_collection")]
        public bool IsInPersonalPlanCollection { get; set; }

        [JsonProperty("is_in_premium")]
        public bool IsInPremium { get; set; }

        [JsonProperty("is_language_course")]
        public bool IsLanguageCourse { get; set; }

        [JsonProperty("has_508_closed_captions")]
        public bool Has508ClosedCaptions { get; set; }

        [JsonProperty("is_coding_exercises_badge_eligible")]
        public bool IsCodingExercisesBadgeEligible { get; set; }

        [JsonProperty("content_collections")]
        public List<ContentCollection> ContentCollections { get; set; }

        [JsonProperty("is_course_in_ub_ever")]
        public bool IsCourseInUbEver { get; set; }

        [JsonProperty("has_course_a_published_coding_exercise")]
        public bool HasCourseAPublishedCodingExercise { get; set; }
    }

    public class SavingPrice
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("price_string")]
        public string PriceString { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class Video
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }
    }

    public class VisibleInstructor
    {
        [JsonProperty("_class")]
        public string Class { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("job_title")]
        public string JobTitle { get; set; }

        [JsonProperty("image_50x50")]
        public string Image50x50 { get; set; }

        [JsonProperty("image_100x100")]
        public string Image100x100 { get; set; }

        [JsonProperty("initials")]
        public string Initials { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class WhatYouWillLearnData
    {
        [JsonProperty("items")]
        public List<string> Items { get; set; }
    }

    public class WhoShouldAttendData
    {
        [JsonProperty("items")]
        public List<string> Items { get; set; }
    }

