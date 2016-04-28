namespace needHelp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;
    using needHelp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<needHelp.Models.GeneralModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(needHelp.Models.GeneralModel context)
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.cities.AddOrUpdate(x => x.id,
                    new CityModels()
                    {
                        id = 1,
                        name = "רעננה"
                    },
                    new CityModels()
                    {
                        id = 2,
                        name = "הרצליה"
                    },
                    new CityModels()
                    {
                        id = 3,
                        name = "נתניה"
                    },
                    new CityModels()
                    {
                        id = 4,
                        name = "רמת גן"
                    },
                    new CityModels()
                    {
                        id = 5,
                        name = "תל אביב"
                    },
            new CityModels()
                    {
                        id = 6,
                        name = "פתח תקווה"
                    },
            new CityModels()
                    {
                        id = 7,
                        name = "קריית אונו"
                    },
            new CityModels()
                    {
                        id = 8,
                        name = "ירושליים"
                    },
            new CityModels()
                    {
                        id = 9,
                        name = "אילת"
                    });

            context.organizations.AddOrUpdate(x => x.id,
                    new OrganizationModels()
                    {
                        id = 1,
                        name = "לקט ישראל",
                        contactName = "אפרת גוש",
                        contactPhone = "0577216601",
                        email = "leket@gmail.com"
                    },
                    new OrganizationModels()
                    {
                        id = 2,
                        name = "בית ספר מגידו",
                        contactName = "אורנה דץ",
                        contactPhone = "0537212101",
                        email = "megido@gmail.com"
                    },
                    new OrganizationModels()
                    {
                        id = 3,
                        name = "צער בעלי חיים",
                        contactName = "חיים לבי",
                        contactPhone = "0547214231",
                        email = "haim@gmail.com"
                    },
                    new OrganizationModels()
                    {
                        id = 4,
                        name = "למען הניצולים",
                        contactName = "יובל דרור",
                        contactPhone = "0527212151",
                        email = "lemaan@gmail.com"
                    },
                    new OrganizationModels()
                    {
                        id = 5,
                        name = "כולנו",
                        contactName = "קובי קבבי",
                        contactPhone = "0545222101",
                        email = "kulanu@gmail.com"
                    });

            context.help_types.AddOrUpdate(x => x.id,
                    new HelpTypeModels()
                    {
                        id = 1,
                        typeName = "חיות",
                        imagePath = "animals.png"
                    },
                    new HelpTypeModels()
                    {
                        id = 2,
                        typeName = "מזון",
                        imagePath = "food.png"
                    },
                    new HelpTypeModels()
                    {
                        id = 3,
                        typeName = "לימודים",
                        imagePath = "learning.png"
                    });

            context.activities.AddOrUpdate(x => x.id,
                    new ActivityModels()
                    {
                        id = 1,
                        name = "טיול עם כלבים",
                        cityId = 1,
                        organizationId = 3,
                        description = "טיול קצר אחר הצהריים עם תושבי הכלבייה",
                        date = new DateTime(2016,6,14,15,30,0),
                        typeId = 1,
                        address = "הנביאים 12"
                    },
                    new ActivityModels()
                    {
                        id = 2,
                        name = "עזרה בשיעורים",
                        cityId = 2,
                        organizationId = 5,
                        description = "עזרה בשיעורים לילדי כחתות א עד ג",
                        date = new DateTime(2016, 6, 5, 14, 0, 0),
                        typeId = 3,
                        address = "בן גוריון 35"
                    },
                    new ActivityModels()
                    {
                        id = 3,
                        name = "האכלת יונים",
                        cityId = 1,
                        organizationId = 3,
                        description = "להאכיל יונים רעבות וקשות יום",
                        date = new DateTime(2016, 5, 12, 17, 10, 0),
                        typeId = 1,
                        address = "בן פורט יוסף 5"
                    },
                    new ActivityModels()
                    {
                        id = 4,
                        name = "איסוף מזון ממסעדות",
                        cityId = 2,
                        organizationId = 5,
                        description = "איסוף מזון ממסעדות בשעות הערב למען הנזקקים, יש להגיע עם רכב וקופסאות לאיסוף. לתיאום הפרטים יש לפנות לאיש הקשר.",
                        date = new DateTime(2016, 5, 22, 20, 30, 0),
                        typeId = 3,
                        address = "בן גוריון 35"
                    });
            
        }
    }
}
