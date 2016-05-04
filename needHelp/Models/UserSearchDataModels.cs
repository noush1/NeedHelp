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
        public int cityId { get; set; }

        [DisplayName("תאריך ושעה")]
        public DateTime date { get; set; }

        // TODO: add all the fileds of the search - organization name, type...
        //[DisplayName("ארגון")]
        //public int organizationId { get; set; }

        //[DisplayName("סוג פעילות")]
        //public int typeId { get; set; }
    }
}