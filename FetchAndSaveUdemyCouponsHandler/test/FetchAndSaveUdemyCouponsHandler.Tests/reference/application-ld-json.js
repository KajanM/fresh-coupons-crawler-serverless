// JSON.parse(document.querySelector('script[type="application/ld+json"]').innerText)

const applicationLdJson = [
  {
    "@type": "Course",
    "@context": "http://schema.org",
    "publisher": {
      "@type": "Organization",
      "name": "Udemy",
      "sameAs": "www.udemy.com"
    },
    "provider": {
      "@type": "Organization",
      "name": "Subhana Hye",
      "sameAs": "www.udemy.com/user/subhana-8/"
    },
    "@id": "https://www.udemy.com/course/introduction-to-artificial-intelligence-in-the-workplace/",
    "name": "Introduction to Artificial Intelligence in the Workplace",
    "description": "A Learning & Development (L&D) - HR Perspective",
    "isAccessibleForFree": false,
    "image": "https://img-c.udemycdn.com/course/750x422/3482168_ba3e_3.jpg",
    "inLanguage": "en",
    "audience": {
      "@type": "Audience",
      "audienceType": [
        "People who are curious about Artificial Intelligence and want to learn the impact of AI in a workplace",
        "L&D and Human Resources professionals"
      ]
    },
    "about": {
      "name": "Business"
    },
    "creator": [
      {
        "@type": "Person",
        "name": "Subhana Hye"
      }
    ],
    "aggregateRating": {
      "@type": "AggregateRating",
      "ratingValue": "4.5",
      "ratingCount": 114,
      "bestRating": 5,
      "worstRating": 0.5
    }
  },
  {
    "@context": "http://schema.org",
    "@type": "BreadcrumbList",
    "itemListElement": [
      {
        "@type": "ListItem",
        "position": 1,
        "name": "Business",
        "item": "https://www.udemy.com/courses/business/"
      },
      {
        "@type": "ListItem",
        "position": 2,
        "name": "Human Resources",
        "item": "https://www.udemy.com/courses/business/human-resources/"
      },
      {
        "@type": "ListItem",
        "position": 3,
        "name": "Artificial Intelligence",
        "item": "https://www.udemy.com/topic/artificial-intelligence/"
      }
    ]
  }
]