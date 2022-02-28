﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace Lms.Core.Dto
{
    public class ModuleDto
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate 
        {
            get { return StartDate.AddMonths(1); }
            set { EndDate = value; } 
            
        }
    }
}

