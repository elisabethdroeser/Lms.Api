﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Entities
{
    public class Course
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<Module> Modules { get; set; } = new List<Module>();

        public Course()
        {
            Title = null!;
        }

        public Course(string title, DateTime startDate)
        {
            Title = title;
            StartDate = startDate;
        }
    }
}
