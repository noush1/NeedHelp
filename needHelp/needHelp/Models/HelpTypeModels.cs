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
    public class HelpTypeModels
    {
        [Key]
        public int id { get; set; }

        [Required]
        [DisplayName("Type name")]
        public string typeName { get; set; }

        [Required]
        [DisplayName("Image path")]
        public string imagePath { get; set; }
    }
}