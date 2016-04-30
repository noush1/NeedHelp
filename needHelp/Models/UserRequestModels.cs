﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace needHelp.Models
{
    public class UserRequestModels
    {
        [Key]
        [Column(Order = 1)]
        public int volunteerId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int activityId { get; set; }

        public bool isAccepted { get; set; }
    }
}