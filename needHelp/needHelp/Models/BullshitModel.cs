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
    public class BullshitModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [DisplayName("Bullshit Name")]
        public string name { get; set; }
    }
}