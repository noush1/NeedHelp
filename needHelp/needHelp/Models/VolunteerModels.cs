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
        [DisplayName("First name")]
        public string firstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        public string lastName { get; set; }

        [Required]
        [DisplayName("E-mail")]
        public string email { get; set; }

        [DisplayName("Phone")]
        public string phone { get; set; }

        public virtual ICollection<ActivityModels> registered_activities { get; set; }
    }
}