﻿﻿using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaWebSite.Models
{
    public class OptionsContext : DbContext
    {
        //public OptionsContext() : base("DefaultConnection") { }

        public DbSet<Choice> Choices { get; set; }

        public DbSet<YearTerm> YearTerms { get; set; }

        public DbSet<Option> Options { get; set; }
    }
}