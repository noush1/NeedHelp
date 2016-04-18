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
        [DisplayName("קוד משתמש")]
        public string userId { get; set; }

        [Required]
        [DisplayName("שם ארגון")]
        public string name { get; set; }

        [Required]
        [DisplayName("איש קשר")]
        public string contactName { get; set; }

        [Required]
        [DisplayName("טלפון")]
        public string contactPhone { get; set; }

        [DisplayName("אימייל")]
        public string email { get; set; }

        [DisplayName("אתר")]
        public string website { get; set; }

        public List<TrustedUserModels> trustedUsers { get; set; }

        public virtual ICollection<ActivityModels> org_activities { get; set; }
    }
}