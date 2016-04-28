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
                        name = "�����"
                    },
                    new CityModels()
                    {
                        id = 2,
                        name = "������"
                    },
                    new CityModels()
                    {
                        id = 3,
                        name = "�����"
                    },
                    new CityModels()
                    {
                        id = 4,
                        name = "��� ��"
                    },
                    new CityModels()
                    {
                        id = 5,
                        name = "�� ����"
                    },
            new CityModels()
                    {
                        id = 6,
                        name = "��� �����"
                    },
            new CityModels()
                    {
                        id = 7,
                        name = "����� ����"
                    },
            new CityModels()
                    {
                        id = 8,
                        name = "��������"
                    },
            new CityModels()
                    {
                        id = 9,
                        name = "����"
                    });

            context.organizations.AddOrUpdate(x => x.id,
                    new OrganizationModels()
                    {
                        id = 1,
                        name = "��� �����",
                        contactName = "���� ���",
                        contactPhone = "0577216601",
                        email = "leket@gmail.com"
                    },
                    new OrganizationModels()
                    {
                        id = 2,
                        name = "��� ��� �����",
                        contactName = "����� ��",
                        contactPhone = "0537212101",
                        email = "megido@gmail.com"
                    },
                    new OrganizationModels()
                    {
                        id = 3,
                        name = "��� ���� ����",
                        contactName = "���� ���",
                        contactPhone = "0547214231",
                        email = "haim@gmail.com"
                    },
                    new OrganizationModels()
                    {
                        id = 4,
                        name = "���� ��������",
                        contactName = "���� ����",
                        contactPhone = "0527212151",
                        email = "lemaan@gmail.com"
                    },
                    new OrganizationModels()
                    {
                        id = 5,
                        name = "�����",
                        contactName = "���� ����",
                        contactPhone = "0545222101",
                        email = "kulanu@gmail.com"
                    });

            context.help_types.AddOrUpdate(x => x.id,
                    new HelpTypeModels()
                    {
                        id = 1,
                        typeName = "����",
                        imagePath = "animals.png"
                    },
                    new HelpTypeModels()
                    {
                        id = 2,
                        typeName = "����",
                        imagePath = "food.png"
                    },
                    new HelpTypeModels()
                    {
                        id = 3,
                        typeName = "�������",
                        imagePath = "learning.png"
                    });

            context.activities.AddOrUpdate(x => x.id,
                    new ActivityModels()
                    {
                        id = 1,
                        name = "���� �� �����",
                        cityId = 1,
                        organizationId = 3,
                        description = "���� ��� ��� ������� �� ����� �������",
                        date = new DateTime(2016,6,14,15,30,0),
                        typeId = 1,
                        address = "������� 12"
                    },
                    new ActivityModels()
                    {
                        id = 2,
                        name = "���� ��������",
                        cityId = 2,
                        organizationId = 5,
                        description = "���� �������� ����� ����� � �� �",
                        date = new DateTime(2016, 6, 5, 14, 0, 0),
                        typeId = 3,
                        address = "�� ������ 35"
                    },
                    new ActivityModels()
                    {
                        id = 3,
                        name = "����� �����",
                        cityId = 1,
                        organizationId = 3,
                        description = "������ ����� ����� ����� ���",
                        date = new DateTime(2016, 5, 12, 17, 10, 0),
                        typeId = 1,
                        address = "�� ���� ���� 5"
                    },
                    new ActivityModels()
                    {
                        id = 4,
                        name = "����� ���� �������",
                        cityId = 2,
                        organizationId = 5,
                        description = "����� ���� ������� ����� ���� ���� �������, �� ����� �� ��� �������� ������. ������ ������ �� ����� ���� ����.",
                        date = new DateTime(2016, 5, 22, 20, 30, 0),
                        typeId = 3,
                        address = "�� ������ 35"
                    });
            
        }
    }
}
