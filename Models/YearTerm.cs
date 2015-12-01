﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaWebSite.Models
{
    public class YearTerm
    {
        
        public int YearTermId { get; set; }

        public int year { get; set; }

        public int term { get; set; }

        public bool isDefault { get; set; }
        
    }
}