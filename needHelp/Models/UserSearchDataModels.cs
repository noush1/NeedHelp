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
    public class UserSearchDataModels
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int VolunteerId { get; set; }

        [DisplayName("עיר")]
        public int? cityId { get; set; }

        [DisplayName("זמן החיפוש")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? searchDate { get; set; }

        [DisplayName("מתאריך ושעה")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? startDate { get; set; }

        [DisplayName("עד תאריך ושעה")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? endDate { get; set; }

        [DisplayName("ארגון")]
        public int? organizationId { get; set; }

        [DisplayName("סוג פעילות")]
        public int? typeId { get; set; }

        public virtual VolunteerModels volunteer { get; set; }
        public virtual OrganizationModels org { get; set; }
        public virtual CityModels city { get; set; }
        public virtual HelpTypeModels type { get; set; }
    }
}