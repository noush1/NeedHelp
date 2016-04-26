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
    public class VolunteerModels
    {
        [Key]
        public int id { get; set; }

        // TODO: this field is foreignKey - supposed to link between volunteer and user data like username and password
        public string userId { get; set; }

        [Required]
        [DisplayName("שם פרטי")]
        public string firstName { get; set; }

        [Required]
        [DisplayName("שם משפחה")]
        public string lastName { get; set; }

        [Required]
        [DisplayName("אימייל")]
        public string email { get; set; }

        [DisplayName("טלפון")]
        public string phone { get; set; }

        public virtual ICollection<ActivityModels> registered_activities { get; set; }
    }
}