// https://www.udemy.com/api-2.0/course-landing-components/4412876/me/?components=slider_menu,buy_button,deal_badge,discount_expiration,price_text,incentives,purchase,redeem_coupon,money_back_guarantee,base_purchase_section,purchase_tabs_context,lifetime_access_context,available_coupons,gift_this_course,buy_for_team

const response = {
  "slider_menu": {
    "data": {
      "title": "The Melody of English",
      "badge_family": null,
      "is_free_seo_exp": false,
      "rating": 4.626419,
      "num_reviews": 109,
      "num_students": 11512,
      "showCodingExercisesBadge": false,
      "is_coding_exercises_badge_eligible": false
    }
  },
  "buy_button": {
    "button": {
      "add_to_cart_redirect_url": "/cart/added/course/4412876/",
      "replace_with_add_to_cart": false,
      "base_express_checkout": "/cart/checkout/express/course/4412876/",
      "enrollment_disabled": false,
      "event_type": "buy_now",
      "icon": null,
      "is_free_with_discount": false,
      "require_popup": true,
      "text": "Buy now",
      "buy_url": "/cart/checkout/express/course/4412876/?discountCode=BIGDREAMSALE",
      "preview_url": "?couponCode=BIGDREAMSALE",
      "payment_data": {
        "buyableId": 4412876,
        "buyableType": "course",
        "discountInfo": {
          "code": "BIGDREAMSALE"
        },
        "purchasePrice": {
          "amount": 9.99,
          "currency": "USD",
          "price_string": "$9.99",
          "currency_symbol": "$"
        }
      },
      "size": "large",
      "style": "primary",
      "is_paid": true,
      "is_enabled": true
    }
  },
  "discount_expiration": {
    "data": {
      "discount_deadline_text": "3 days",
      "is_enabled": true
    }
  },
  "price_text": {
    "data": {
      "is_valid_student": false,
      "purchase_date": null,
      "is_in_subscription": false,
      "show_discount_info": true,
      "pricing_result": {
        "price_serve_tracking_id": "CWszNdyfRS-diUlZYv_Gnw",
        "price": {
          "amount": 9.99,
          "currency": "USD",
          "price_string": "$9.99",
          "currency_symbol": "$"
        },
        "list_price": {
          "amount": 54.99,
          "currency": "USD",
          "price_string": "$54.99",
          "currency_symbol": "$"
        },
        "saving_price": {
          "amount": 45,
          "currency": "USD",
          "price_string": "$45.00",
          "currency_symbol": "$"
        },
        "has_discount_saving": true,
        "discount_percent": 82,
        "discount_percent_for_display": 82,
        "buyable": {
          "id": 4412876,
          "type": "course"
        },
        "campaign": {
          "code": "BIGDREAMSALE",
          "end_time": "2023-05-24 18:30:00+00:00",
          "is_instructor_created": false,
          "is_public": true,
          "start_time": "2023-05-16 07:00:00+00:00",
          "campaign_type": "deal",
          "uses_remaining": null,
          "maximum_uses": null,
          "show_code": true
        },
        "code": "BIGDREAMSALE",
        "is_public": true
      },
      "course_id": 4412876,
      "list_price": {
        "amount": 54.99,
        "currency": "USD",
        "price_string": "$54.99",
        "currency_symbol": "$"
      },
      "is_organization_only": false,
      "is_free_for_organization": false,
      "show_percent_discount": true,
      "is_enabled": true
    }
  },
  "incentives": {
    "is_free_seo_exp": false,
    "video_content_length": "2.5 hours",
    "audio_content_length": "",
    "num_articles": 0,
    "num_quizzes": 0,
    "num_practice_tests": 0,
    "num_coding_exercises": 0,
    "num_additional_resources": 27,
    "num_projects": 0,
    "has_lifetime_access": true,
    "devices_access": "Access on mobile and TV",
    "has_assignments": false,
    "has_certificate": true,
    "num_cpe_credits": 0,
    "placement": "sidebar",
    "reorder_incentives": true,
    "show_incentives_on_tablet": true,
    "show_quizzes": false,
    "move_lifetime_access_to_purchase_section": false,
    "has_closed_captions": false,
    "has_audio_description": false
  },
  "purchase": {
    "data": {
      "is_valid_student": false,
      "purchase_date": null,
      "is_in_subscription": false,
      "show_discount_info": true,
      "pricing_result": {
        "price_serve_tracking_id": "CWszNdyfRS-diUlZYv_Gnw",
        "price": {
          "amount": 9.99,
          "currency": "USD",
          "price_string": "$9.99",
          "currency_symbol": "$"
        },
        "list_price": {
          "amount": 54.99,
          "currency": "USD",
          "price_string": "$54.99",
          "currency_symbol": "$"
        },
        "saving_price": {
          "amount": 45,
          "currency": "USD",
          "price_string": "$45.00",
          "currency_symbol": "$"
        },
        "has_discount_saving": true,
        "discount_percent": 82,
        "discount_percent_for_display": 82,
        "buyable": {
          "id": 4412876,
          "type": "course"
        },
        "campaign": {
          "code": "BIGDREAMSALE",
          "end_time": "2023-05-24 18:30:00+00:00",
          "is_instructor_created": false,
          "is_public": true,
          "start_time": "2023-05-16 07:00:00+00:00",
          "campaign_type": "deal",
          "uses_remaining": null,
          "maximum_uses": null,
          "show_code": true
        },
        "code": "BIGDREAMSALE",
        "is_public": true
      },
      "course_id": 4412876,
      "list_price": {
        "amount": 54.99,
        "currency": "USD",
        "price_string": "$54.99",
        "currency_symbol": "$"
      },
      "is_organization_only": false,
      "is_free_for_organization": false
    }
  },
  "redeem_coupon": {
    "discount_attempts": [],
    "has_already_purchased": false
  },
  "money_back_guarantee": {
    "is_enabled": true,
    "cta_refund_policy": null
  },
  "base_purchase_section": {
    "purchaseInfo": {
      "isValidStudent": false,
      "purchaseDate": null
    },
    "purchaseSection": {
      "is_course_paid": true,
      "has_subscription_offerings": false,
      "subscriptionContext": null,
      "style_full_lifetime_access": "full-lifetime-access",
      "style_money_back_guarantee": "money-back-guarantee",
      "showCancelAnytime": false
    }
  },
  "purchase_tabs_context": {
    "selectedTab": "personal",
    "purchaseInfo": null,
    "buttonText": "Try Udemy Business",
    "primaryLink": "/udemy-business/?locale=en_US&path=request-demo-mx%2F&ref=marketplace_teaching-and-academics&utm_campaign=mx-hooks&utm_content=clp&utm_medium=referral&utm_source=marketplace&utm_term=",
    "subscriptionContext": null,
    "selectedPurchaseOption": "subscription",
    "isAnnualPlanEnabled": null,
    "subscriptionPlanPriceIds": null,
    "enableSubsCtaAuthModal": null
  },
  "lifetime_access_context": {
    "hasLifetimeAccess": true
  },
  "gift_this_course": {
    "gift_this_course_link": "/gift/the-melody-of-english/?couponCode=BIGDREAMSALE",
    "round": null
  },
  "buy_for_team": {
    "data": {
      "ufb_demo_link": "/udemy-business/?locale=en_US&path=request-demo-mx%2F&ref=marketplace_control_teaching-and-academics&utm_campaign=mx-hooks&utm_content=clp&utm_medium=referral&utm_source=marketplace&utm_term=",
      "ufb_copy_context": {
        "title": "Training 5 or more people?",
        "content": "Get your team access to 22,000+ top Udemy courses anytime, anywhere."
      },
      "ufb_button_copy": "Try Udemy Business",
      "buy_for_team_ref": "marketplace",
      "is_enabled": true,
      "isOnsiteRequestDemo": false
    }
  }
}