﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace DiplomaWebSite.Models
{
    public class Option
    {
        public int OptionId { get; set; }

        public string title { get; set; }

        public bool isActive { get; set; }

        public List<Choice> choice { get; set; }
    }
}