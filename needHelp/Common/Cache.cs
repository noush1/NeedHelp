using needHelp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace needHelp.Common
{
    public class Cache
    {
        private GeneralModel _db;
        private static Cache _instance;

        public DbSet<ActivityModels> activities { get; set; }
        public DbSet<CityModels> cities { get; set; }
        public DbSet<VolunteerModels> volunteers { get; set; }
        public DbSet<UserRequestModels> user_requests { get; set; }
        public DbSet<TrustedUserModels> trusted_users { get; set; }
        public DbSet<OrganizationModels> organizations { get; set; }
        public DbSet<HelpTypeModels> help_types { get; set; } 

        private Cache()
        {
            _db = GeneralModel.Instance();
            UpdateCache();
        }

        public static Cache Instance()
        {
            if (_instance == null)
            {
                _instance = new Cache();
            }

            return _instance;
        }

        public void UpdateCache()
        {
            activities = _db.activities;
            cities = _db.cities;
            volunteers = _db.volunteers;
            user_requests = _db.user_requests;
            trusted_users = _db.trusted_users;
            organizations = _db.organizations;
            help_types = _db.help_types;
        }
    }
}