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
    public class CityModels
    {
        [Key]
        public int id { get; set; }

        [Required]
        [DisplayName("City name")]
        public string name { get; set; }
    }
}