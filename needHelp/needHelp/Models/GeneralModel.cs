using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;


namespace needHelp.Models
{
    public class GeneralModel: IdentityDbContext
    {
        public GeneralModel()
            : base("DefaultConnection")
        {
        }
        public DbSet<ActivityModels> activities { get; set; }
        public DbSet<CityModels> cities { get; set; }
        public DbSet<VolunteerModels> volunteers { get; set; }
        public DbSet<UserRequestModels> user_requests { get; set; }
        public DbSet<TrustedUserModels> trusted_users { get; set; }
        public DbSet<OrganizationModels> organizations { get; set; }
        public DbSet<UserSearchDataModels> search_data { get; set; }
        public DbSet<HelpTypeModels> help_types { get; set; }
        public DbSet<BullshitModel> bullshit { get; set; }
    }
}