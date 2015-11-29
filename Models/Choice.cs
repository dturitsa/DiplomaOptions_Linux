﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace DiplomaWebSite.Models
{
    public class Choice
    {
        public int ChoiceId { get; set; }

        public int YearTermId { get; set; }

        [Required]
        [RegularExpression("^A00[0-9]{6}$")]
        [MaxLength(9)]
        public String StudentId { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "First Name")]
        public String StudentFirstName { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "Last Name")]
        public String StudentLastname { get; set; }


        [Required]
        [Display(Name = "First Choice")]
        public int FirstChoiceOptionId { get; set; }

        [Required]
        [Display(Name = "Second Choice")]
        public int SecondChoiceOptionId { get; set; }

        [Required]
        [Display(Name = "Third Choice")]
        public int ThirdChoiceOptionId { get; set; }

        [Required]
        [Display(Name = "Fourth Choice")]
        public int FourthChoiceOptionId { get; set; }

        [Required]
        public DateTime SelectionDate { get; set; }

        public Option options { get; set; }

        public YearTerm yearTerm { get; set; }

        // public List<Option> options { get; set; }

        // public List<YearTerm> yearTerms { get; set; }



    }
}