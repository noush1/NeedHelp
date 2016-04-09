using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace needHelp.Models
{
    public class OrganizationModels
    {
        public OrganizationModels()
        {
            trustedUsers = new List<TrustedUserModels>();
        }

        [Key]
        public int id { get; set; }

        // TODO: this field is foreignKey - supposed to link between volunteer and user data like username and password
        [Required]
        [DisplayName("User")]
        public string userId { get; set; }

        [Required]
        [DisplayName("Organization name")]
        public string name { get; set; }

        [Required]
        [DisplayName("Contact name")]
        public string contactName { get; set; }

        [Required]
        [DisplayName("Phone")]
        public string contactPhone { get; set; }

        [DisplayName("E-mail")]
        public string email { get; set; }

        [DisplayName("Website")]
        public string website { get; set; }

        public List<TrustedUserModels> trustedUsers { get; set; }

        public virtual ICollection<ActivityModels> org_activities { get; set; }
    }
}