using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FetchAndSaveUdemyCouponsHandler.Udemy.Helpers
{
// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class PriceDetail
    {
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("price_string")]
        public string PriceString { get; set; }

        [JsonPropertyName("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class VisibleInstructor
    {
        [JsonPropertyName("_class")]
        public string Class { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("job_title")]
        public string JobTitle { get; set; }

        [JsonPropertyName("image_50x50")]
        public string Image50x50 { get; set; }

        [JsonPropertyName("image_100x100")]
        public string Image100x100 { get; set; }

        [JsonPropertyName("initials")]
        public string Initials { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class CaptionLocale
    {
        [JsonPropertyName("_class")]
        public string Class { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("english_title")]
        public string EnglishTitle { get; set; }
    }

    public class Price
    {
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("price_string")]
        public string PriceString { get; set; }

        [JsonPropertyName("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class ListPrice
    {
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("price_string")]
        public string PriceString { get; set; }

        [JsonPropertyName("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class SavingPrice
    {
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("price_string")]
        public string PriceString { get; set; }

        [JsonPropertyName("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class Buyable
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Campaign
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("is_instructor_created")]
        public bool IsInstructorCreated { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("campaign_type")]
        public string CampaignType { get; set; }

        [JsonPropertyName("uses_remaining")]
        public object UsesRemaining { get; set; }

        [JsonPropertyName("maximum_uses")]
        public object MaximumUses { get; set; }
    }

    public class Discount
    {
        [JsonPropertyName("price_serve_tracking_id")]
        public string PriceServeTrackingId { get; set; }

        [JsonPropertyName("price")]
        public Price Price { get; set; }

        [JsonPropertyName("list_price")]
        public ListPrice ListPrice { get; set; }

        [JsonPropertyName("saving_price")]
        public SavingPrice SavingPrice { get; set; }

        [JsonPropertyName("has_discount_saving")]
        public bool HasDiscountSaving { get; set; }

        [JsonPropertyName("discount_percent")]
        public int DiscountPercent { get; set; }

        [JsonPropertyName("discount_percent_for_display")]
        public int DiscountPercentForDisplay { get; set; }

        [JsonPropertyName("buyable")]
        public Buyable Buyable { get; set; }

        [JsonPropertyName("campaign")]
        public Campaign Campaign { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }
    }

    public class DiscountPrice
    {
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("price_string")]
        public string PriceString { get; set; }

        [JsonPropertyName("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class RatingDistribution
    {
        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    public class Features
    {
        [JsonPropertyName("_class")]
        public string Class { get; set; }

        [JsonPropertyName("discussions_create")]
        public bool DiscussionsCreate { get; set; }

        [JsonPropertyName("discussions_view")]
        public bool DiscussionsView { get; set; }

        [JsonPropertyName("discussions_replies_create")]
        public bool DiscussionsRepliesCreate { get; set; }

        [JsonPropertyName("enroll")]
        public bool Enroll { get; set; }

        [JsonPropertyName("reviews_create")]
        public bool ReviewsCreate { get; set; }

        [JsonPropertyName("reviews_view")]
        public bool ReviewsView { get; set; }

        [JsonPropertyName("reviews_responses_create")]
        public bool ReviewsResponsesCreate { get; set; }

        [JsonPropertyName("announcements_comments_view")]
        public bool AnnouncementsCommentsView { get; set; }

        [JsonPropertyName("educational_announcements_create")]
        public bool EducationalAnnouncementsCreate { get; set; }

        [JsonPropertyName("promotional_announcements_create")]
        public bool PromotionalAnnouncementsCreate { get; set; }

        [JsonPropertyName("promotions_create")]
        public bool PromotionsCreate { get; set; }

        [JsonPropertyName("promotions_view")]
        public bool PromotionsView { get; set; }

        [JsonPropertyName("students_view")]
        public bool StudentsView { get; set; }
    }

    public class NotificationSettings
    {
    }

    public class IntendedCategory
    {
        [JsonPropertyName("_class")]
        public string Class { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("icon_class")]
        public string IconClass { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("title_cleaned")]
        public string TitleCleaned { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }
    }

    public class PrimaryCategory
    {
        [JsonPropertyName("_class")]
        public string Class { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("icon_class")]
        public string IconClass { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("title_cleaned")]
        public string TitleCleaned { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }
    }

    public class PrimarySubcategory
    {
        [JsonPropertyName("_class")]
        public string Class { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("icon_class")]
        public string IconClass { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("title_cleaned")]
        public string TitleCleaned { get; set; }

        [JsonPropertyName("channel_id")]
        public int ChannelId { get; set; }
    }

    public class LocaleModel
    {
        [JsonPropertyName("_class")]
        public string Class { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("english_title")]
        public string EnglishTitle { get; set; }

        [JsonPropertyName("simple_english_title")]
        public string SimpleEnglishTitle { get; set; }
    }

    public class RequirementsData
    {
        [JsonPropertyName("items")]
        public List<string> Items { get; set; }
    }

    public class WhatYouWillLearnData
    {
        [JsonPropertyName("items")]
        public List<string> Items { get; set; }
    }

    public class WhoShouldAttendData
    {
        [JsonPropertyName("items")]
        public List<string> Items { get; set; }
    }

    public class Video
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("file")]
        public string File { get; set; }
    }

    public class DownloadUrls
    {
        [JsonPropertyName("Video")]
        public List<Video> Video { get; set; }
    }

    public class PromoAsset
    {
        [JsonPropertyName("_class")]
        public string Class { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("asset_type")]
        public string AssetType { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("title_cleaned")]
        public string TitleCleaned { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("download_urls")]
        public DownloadUrls DownloadUrls { get; set; }
    }

    public class Faq
    {
        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("answer")]
        public string Answer { get; set; }
    }

    public class GoogleInAppPriceDetail
    {
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("price_string")]
        public string PriceString { get; set; }

        [JsonPropertyName("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class AppleInAppPriceDetail
    {
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("price_string")]
        public string PriceString { get; set; }

        [JsonPropertyName("currency_symbol")]
        public string CurrencySymbol { get; set; }
    }

    public class ClientSettings
    {
        [JsonPropertyName("coding_exercises_enabled")]
        public bool CodingExercisesEnabled { get; set; }

        [JsonPropertyName("lecture_export_enabled")]
        public bool LectureExportEnabled { get; set; }

        [JsonPropertyName("machine_cc_enabled")]
        public bool MachineCcEnabled { get; set; }

        [JsonPropertyName("is_cpe_compliant")]
        public bool IsCpeCompliant { get; set; }

        [JsonPropertyName("cpe_field_of_study")]
        public bool CpeFieldOfStudy { get; set; }

        [JsonPropertyName("cpe_program_level")]
        public bool CpeProgramLevel { get; set; }
    }

    public class QualityReviewProcess
    {
        [JsonPropertyName("_class")]
        public string Class { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("score")]
        public double Score { get; set; }

        [JsonPropertyName("admin_rating")]
        public int AdminRating { get; set; }
    }

    public class Label
    {
        [JsonPropertyName("_class")]
        public string Class { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("icon_class")]
        public string IconClass { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("topic_channel_url")]
        public string TopicChannelUrl { get; set; }

        [JsonPropertyName("tracking_object_type")]
        public string TrackingObjectType { get; set; }
    }

    public class CourseHasLabel
    {
        [JsonPropertyName("_class")]
        public string Class { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("label")]
        public Label Label { get; set; }

        [JsonPropertyName("is_primary")]
        public bool IsPrimary { get; set; }
    }

    public class Category
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("tracking_object_type")]
        public string TrackingObjectType { get; set; }
    }

    public class ContextInfo
    {
        [JsonPropertyName("category")]
        public Category Category { get; set; }

        [JsonPropertyName("subcategory")]
        public object Subcategory { get; set; }

        [JsonPropertyName("label")]
        public Label Label { get; set; }
    }

    public class UdemyCourseDetailsResponse
    {
        [JsonPropertyName("_class")]
        public string Class { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("is_paid")]
        public bool IsPaid { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("price_detail")]
        public PriceDetail PriceDetail { get; set; }

        [JsonPropertyName("price_serve_tracking_id")]
        public string PriceServeTrackingId { get; set; }

        [JsonPropertyName("visible_instructors")]
        public List<VisibleInstructor> VisibleInstructors { get; set; }

        [JsonPropertyName("image_125_H")]
        public string Image125H { get; set; }

        [JsonPropertyName("image_240x135")]
        public string Image240x135 { get; set; }

        [JsonPropertyName("is_practice_test_course")]
        public bool IsPracticeTestCourse { get; set; }

        [JsonPropertyName("image_480x270")]
        public string Image480x270 { get; set; }

        [JsonPropertyName("published_title")]
        public string PublishedTitle { get; set; }

        [JsonPropertyName("tracking_id")]
        public string TrackingId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("headline")]
        public string Headline { get; set; }

        [JsonPropertyName("num_subscribers")]
        public int NumSubscribers { get; set; }

        [JsonPropertyName("caption_locales")]
        public List<CaptionLocale> CaptionLocales { get; set; }

        [JsonPropertyName("discount")]
        public Discount Discount { get; set; }

        [JsonPropertyName("discount_price")]
        public DiscountPrice DiscountPrice { get; set; }

        [JsonPropertyName("avg_rating")]
        public double AvgRating { get; set; }

        [JsonPropertyName("avg_rating_recent")]
        public double AvgRatingRecent { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        [JsonPropertyName("num_reviews")]
        public int NumReviews { get; set; }

        [JsonPropertyName("num_reviews_recent")]
        public int NumReviewsRecent { get; set; }

        [JsonPropertyName("rating_distribution")]
        public List<RatingDistribution> RatingDistribution { get; set; }

        [JsonPropertyName("favorite_time")]
        public object FavoriteTime { get; set; }

        [JsonPropertyName("archive_time")]
        public object ArchiveTime { get; set; }

        [JsonPropertyName("earnings")]
        public string Earnings { get; set; }

        [JsonPropertyName("completion_ratio")]
        public int CompletionRatio { get; set; }

        [JsonPropertyName("is_wishlisted")]
        public bool IsWishlisted { get; set; }

        [JsonPropertyName("num_quizzes")]
        public int NumQuizzes { get; set; }

        [JsonPropertyName("num_lectures")]
        public int NumLectures { get; set; }

        [JsonPropertyName("num_published_lectures")]
        public int NumPublishedLectures { get; set; }

        [JsonPropertyName("num_published_quizzes")]
        public int NumPublishedQuizzes { get; set; }

        [JsonPropertyName("num_curriculum_items")]
        public int NumCurriculumItems { get; set; }

        [JsonPropertyName("num_of_published_curriculum_objects")]
        public int NumOfPublishedCurriculumObjects { get; set; }

        [JsonPropertyName("num_cpe_credits")]
        public object NumCpeCredits { get; set; }

        [JsonPropertyName("is_private")]
        public bool IsPrivate { get; set; }

        [JsonPropertyName("num_practice_tests")]
        public int NumPracticeTests { get; set; }

        [JsonPropertyName("num_published_practice_tests")]
        public int NumPublishedPracticeTests { get; set; }

        [JsonPropertyName("original_price_text")]
        public string OriginalPriceText { get; set; }

        [JsonPropertyName("quality_status")]
        public string QualityStatus { get; set; }

        [JsonPropertyName("status_label")]
        public string StatusLabel { get; set; }

        [JsonPropertyName("can_edit")]
        public bool CanEdit { get; set; }

        [JsonPropertyName("features")]
        public Features Features { get; set; }

        [JsonPropertyName("gift_url")]
        public string GiftUrl { get; set; }

        [JsonPropertyName("num_invitation_requests")]
        public int NumInvitationRequests { get; set; }

        [JsonPropertyName("notification_settings")]
        public NotificationSettings NotificationSettings { get; set; }

        [JsonPropertyName("is_banned")]
        public bool IsBanned { get; set; }

        [JsonPropertyName("is_published")]
        public bool IsPublished { get; set; }

        [JsonPropertyName("intended_category")]
        public IntendedCategory IntendedCategory { get; set; }

        [JsonPropertyName("image_48x27")]
        public string Image48x27 { get; set; }

        [JsonPropertyName("image_50x50")]
        public string Image50x50 { get; set; }

        [JsonPropertyName("image_75x75")]
        public string Image75x75 { get; set; }

        [JsonPropertyName("image_96x54")]
        public string Image96x54 { get; set; }

        [JsonPropertyName("image_100x100")]
        public string Image100x100 { get; set; }

        [JsonPropertyName("image_200_H")]
        public string Image200H { get; set; }

        [JsonPropertyName("image_304x171")]
        public string Image304x171 { get; set; }

        [JsonPropertyName("image_750x422")]
        public string Image750x422 { get; set; }

        [JsonPropertyName("has_certificate")]
        public bool HasCertificate { get; set; }

        [JsonPropertyName("primary_category")]
        public PrimaryCategory PrimaryCategory { get; set; }

        [JsonPropertyName("primary_subcategory")]
        public PrimarySubcategory PrimarySubcategory { get; set; }

        [JsonPropertyName("is_enrollable_on_mobile")]
        public bool IsEnrollableOnMobile { get; set; }

        [JsonPropertyName("is_in_any_ufb_content_collection")]
        public bool IsInAnyUfbContentCollection { get; set; }

        [JsonPropertyName("is_in_user_subscription")]
        public bool IsInUserSubscription { get; set; }

        [JsonPropertyName("is_in_subscribed_content_collections")]
        public bool IsInSubscribedContentCollections { get; set; }

        [JsonPropertyName("locale")]
        public LocaleModel Locale { get; set; }

        [JsonPropertyName("has_closed_caption")]
        public bool HasClosedCaption { get; set; }

        [JsonPropertyName("caption_languages")]
        public List<string> CaptionLanguages { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("instructional_level")]
        public string InstructionalLevel { get; set; }

        [JsonPropertyName("instructional_level_simple")]
        public string InstructionalLevelSimple { get; set; }

        [JsonPropertyName("estimated_content_length")]
        public int EstimatedContentLength { get; set; }

        [JsonPropertyName("content_info")]
        public string ContentInfo { get; set; }

        [JsonPropertyName("content_info_short")]
        public string ContentInfoShort { get; set; }

        [JsonPropertyName("content_length_practice_test_questions")]
        public int ContentLengthPracticeTestQuestions { get; set; }

        [JsonPropertyName("requirements_data")]
        public RequirementsData RequirementsData { get; set; }

        [JsonPropertyName("what_you_will_learn_data")]
        public WhatYouWillLearnData WhatYouWillLearnData { get; set; }

        [JsonPropertyName("who_should_attend_data")]
        public WhoShouldAttendData WhoShouldAttendData { get; set; }

        [JsonPropertyName("is_available_on_google_app")]
        public bool IsAvailableOnGoogleApp { get; set; }

        [JsonPropertyName("organization_id")]
        public object OrganizationId { get; set; }

        [JsonPropertyName("google_in_app_purchase_price_text")]
        public string GoogleInAppPurchasePriceText { get; set; }

        [JsonPropertyName("promo_asset")]
        public PromoAsset PromoAsset { get; set; }

        [JsonPropertyName("is_user_subscribed")]
        public bool IsUserSubscribed { get; set; }

        [JsonPropertyName("apple_in_app_product_id")]
        public string AppleInAppProductId { get; set; }

        [JsonPropertyName("is_available_on_ios")]
        public bool IsAvailableOnIos { get; set; }

        [JsonPropertyName("google_in_app_product_id")]
        public string GoogleInAppProductId { get; set; }

        [JsonPropertyName("faq")]
        public List<Faq> Faq { get; set; }

        [JsonPropertyName("apple_in_app_purchase_price_text")]
        public string AppleInAppPurchasePriceText { get; set; }

        [JsonPropertyName("type_label")]
        public string TypeLabel { get; set; }

        [JsonPropertyName("google_in_app_price_detail")]
        public GoogleInAppPriceDetail GoogleInAppPriceDetail { get; set; }

        [JsonPropertyName("apple_in_app_price_detail")]
        public AppleInAppPriceDetail AppleInAppPriceDetail { get; set; }

        [JsonPropertyName("client_settings")]
        public ClientSettings ClientSettings { get; set; }

        [JsonPropertyName("quality_review_process")]
        public QualityReviewProcess QualityReviewProcess { get; set; }

        [JsonPropertyName("is_organization_only")]
        public bool IsOrganizationOnly { get; set; }

        [JsonPropertyName("is_cpe_compliant")]
        public bool IsCpeCompliant { get; set; }

        [JsonPropertyName("cpe_field_of_study")]
        public object CpeFieldOfStudy { get; set; }

        [JsonPropertyName("cpe_program_level")]
        public object CpeProgramLevel { get; set; }

        [JsonPropertyName("was_ever_published")]
        public bool WasEverPublished { get; set; }

        [JsonPropertyName("buyable_object_type")]
        public string BuyableObjectType { get; set; }

        [JsonPropertyName("published_time")]
        public DateTime PublishedTime { get; set; }

        [JsonPropertyName("is_marketing_boost_agreed")]
        public bool IsMarketingBoostAgreed { get; set; }

        [JsonPropertyName("is_owned_by_instructor_team")]
        public bool IsOwnedByInstructorTeam { get; set; }

        [JsonPropertyName("is_owner_terms_banned")]
        public bool IsOwnerTermsBanned { get; set; }

        [JsonPropertyName("is_taking_disabled")]
        public bool IsTakingDisabled { get; set; }

        [JsonPropertyName("content_length_video")]
        public int ContentLengthVideo { get; set; }

        [JsonPropertyName("checkout_url")]
        public string CheckoutUrl { get; set; }

        [JsonPropertyName("prerequisites")]
        public List<string> Prerequisites { get; set; }

        [JsonPropertyName("objectives")]
        public List<string> Objectives { get; set; }

        [JsonPropertyName("objectives_summary")]
        public List<string> ObjectivesSummary { get; set; }

        [JsonPropertyName("target_audiences")]
        public List<string> TargetAudiences { get; set; }

        [JsonPropertyName("last_accessed_time")]
        public object LastAccessedTime { get; set; }

        [JsonPropertyName("enrollment_time")]
        public object EnrollmentTime { get; set; }

        [JsonPropertyName("course_has_labels")]
        public List<CourseHasLabel> CourseHasLabels { get; set; }

        [JsonPropertyName("bestseller_badge_content")]
        public object BestsellerBadgeContent { get; set; }

        [JsonPropertyName("badges")]
        public List<object> Badges { get; set; }

        [JsonPropertyName("free_course_subscribe_url")]
        public object FreeCourseSubscribeUrl { get; set; }

        [JsonPropertyName("is_recently_published")]
        public bool IsRecentlyPublished { get; set; }

        [JsonPropertyName("last_update_date")]
        public string LastUpdateDate { get; set; }

        [JsonPropertyName("num_article_assets")]
        public int NumArticleAssets { get; set; }

        [JsonPropertyName("num_coding_exercises")]
        public int NumCodingExercises { get; set; }

        [JsonPropertyName("num_assignments")]
        public int NumAssignments { get; set; }

        [JsonPropertyName("num_additional_assets")]
        public int NumAdditionalAssets { get; set; }

        [JsonPropertyName("preview_url")]
        public string PreviewUrl { get; set; }

        [JsonPropertyName("landing_preview_as_guest_url")]
        public string LandingPreviewAsGuestUrl { get; set; }

        [JsonPropertyName("context_info")]
        public ContextInfo ContextInfo { get; set; }

        [JsonPropertyName("has_sufficient_preview_length")]
        public bool HasSufficientPreviewLength { get; set; }

        [JsonPropertyName("has_org_only_setting")]
        public bool HasOrgOnlySetting { get; set; }

        [JsonPropertyName("is_draft")]
        public bool IsDraft { get; set; }

        [JsonPropertyName("common_review_attributes")]
        public List<object> CommonReviewAttributes { get; set; }

        [JsonPropertyName("subscription_locale")]
        public object SubscriptionLocale { get; set; }

        [JsonPropertyName("custom_category_ids")]
        public List<object> CustomCategoryIds { get; set; }

        [JsonPropertyName("alternate_redirect_course_id")]
        public int AlternateRedirectCourseId { get; set; }

        [JsonPropertyName("is_approved")]
        public bool IsApproved { get; set; }

        [JsonPropertyName("is_organization_eligible")]
        public bool IsOrganizationEligible { get; set; }

        [JsonPropertyName("instructor_status")]
        public object InstructorStatus { get; set; }

        [JsonPropertyName("available_features")]
        public List<string> AvailableFeatures { get; set; }

        [JsonPropertyName("enroll_url")]
        public string EnrollUrl { get; set; }

        [JsonPropertyName("learn_url")]
        public string LearnUrl { get; set; }

        [JsonPropertyName("retirement_date")]
        public object RetirementDate { get; set; }

        [JsonPropertyName("is_course_available_in_org")]
        public object IsCourseAvailableInOrg { get; set; }
    }

}