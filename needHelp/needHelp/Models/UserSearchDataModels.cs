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

        [DisplayName("City")]
        public int cityId { get; set; }

        [DisplayName("Date and time")]
        public DateTime date { get; set; }


    }
}