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
    public class ActivityModels
    {
        [Key]
        public int id { get; set; }

        [Required]
        [DisplayName("Activity name")]
        public string name { get; set; }

        // TODO: this field is foreignKey - supposed to link between organization and activity
        [Required]
        [DisplayName("Organization")]
        public int organizationId { get; set; }

        [Required]
        [DisplayName("Address")]
        public string address { get; set; }

        [Required]
        [DisplayName("City")]
        public int cityId { get; set; }

        [Required]
        [DisplayName("Date and time of the event")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime date { get; set; }

        [Required]
        [DisplayName("Type")]
        public int typeId { get; set; }

        [Required]
        [DisplayName("Description")]
        public string description { get; set; }

        public virtual OrganizationModels org { get; set; }
        public virtual CityModels city { get; set; }
        public virtual HelpTypeModels type { get; set; }
        public virtual ICollection<VolunteerModels> volunteers { get; set; }
    }

    public class ActivitiesAndOrganizations
    {
        public IEnumerable<ActivityModels> acts { get; set; }
        public IEnumerable<OrganizationModels> orgs { get; set; }
    }
}