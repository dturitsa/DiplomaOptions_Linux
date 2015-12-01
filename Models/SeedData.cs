using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaWebSite.Models
{
    public static class SeedData
    {
        public static void Initialize(OptionsContext context)
        {
            
            if (!context.YearTerms.Any())
            {
                context.YearTerms.Add(new YearTerm { year = 2015, term = 10, isDefault = false });
                context.YearTerms.Add(new YearTerm { year = 2015, term = 20, isDefault = false });
                context.YearTerms.Add(new YearTerm { year = 2015, term = 30, isDefault = false });
                context.YearTerms.Add(new YearTerm { year = 2016, term = 10, isDefault = true });
                context.YearTerms.Add(new YearTerm {  });

                context.SaveChanges();
            }
            
            if (!context.Options.Any())
            {
                context.Options.Add(new Option { title = "Data Communications", isActive = true });
                context.Options.Add(new Option { title = "Client Server", isActive = true });
                context.Options.Add(new Option { title = "Digital Processing", isActive = true });
                context.Options.Add(new Option { title = "Information Systems", isActive = true });
                context.Options.Add(new Option { title = "Database", isActive = false });
                context.Options.Add(new Option { title = "Web & Mobile", isActive = true });
                context.Options.Add(new Option {  title = "Tech Pro", isActive = false });

                context.SaveChanges();
            }
            
            if (!context.Choices.Any())
            {
                context.Choices.Add(new Choice { 
                  StudentId = "A00100001",
                  StudentFirstName = "SampleFirstName",
                  StudentLastname = "SampleLastName",
                  FirstChoiceOptionId = 5,
                  SecondChoiceOptionId = 2,
                  ThirdChoiceOptionId = 6,
                  FourthChoiceOptionId = 4,
                  SelectionDate = DateTime.Now,
                  YearTermId = 3
                 });
                 
                context.Choices.Add(new Choice {  StudentId = "A00100000",
                  StudentFirstName = "SampleFirstName",
                  StudentLastname = "SampleLastName",
                  FirstChoiceOptionId = 2,
                  SecondChoiceOptionId = 5,
                  ThirdChoiceOptionId = 3,
                  FourthChoiceOptionId = 4,
                  SelectionDate = DateTime.Now,
                  YearTermId = 3
                   });
                   
                context.Choices.Add(new Choice { StudentId = "A00100002",
                  StudentFirstName = "SampleFirstName",
                  StudentLastname = "SampleLastName",
                  FirstChoiceOptionId = 4,
                  SecondChoiceOptionId = 6,
                  ThirdChoiceOptionId = 3,
                  FourthChoiceOptionId = 5,
                  SelectionDate = DateTime.Now,
                  YearTermId = 3           
                   });
                   
                   context.Choices.Add(new Choice { StudentId = "A00100003",
                  StudentFirstName = "SampleFirstName",
                  StudentLastname = "SampleLastName",
                  FirstChoiceOptionId = 4,
                  SecondChoiceOptionId = 6,
                  ThirdChoiceOptionId = 3,
                  FourthChoiceOptionId = 5,
                  SelectionDate = DateTime.Now,
                  YearTermId = 3           
                   });
                   
                   context.Choices.Add(new Choice { StudentId = "A00100004",
                  StudentFirstName = "SampleFirstName",
                  StudentLastname = "SampleLastName",
                  FirstChoiceOptionId = 4,
                  SecondChoiceOptionId = 6,
                  ThirdChoiceOptionId = 3,
                  FourthChoiceOptionId = 5,
                  SelectionDate = DateTime.Now,
                  YearTermId = 3           
                   });
                   
                  context.Choices.Add(new Choice { StudentId = "A00100005",
                  StudentFirstName = "SampleFirstName",
                  StudentLastname = "SampleLastName",
                  FirstChoiceOptionId = 4,
                  SecondChoiceOptionId = 6,
                  ThirdChoiceOptionId = 3,
                  FourthChoiceOptionId = 5,
                  SelectionDate = DateTime.Now,
                  YearTermId = 3           
                   });
                   
                  context.Choices.Add(new Choice { StudentId = "A00100006",
                  StudentFirstName = "SampleFirstName",
                  StudentLastname = "SampleLastName",
                  FirstChoiceOptionId = 4,
                  SecondChoiceOptionId = 6,
                  ThirdChoiceOptionId = 3,
                  FourthChoiceOptionId = 5,
                  SelectionDate = DateTime.Now,
                  YearTermId = 4           
                   });
                   
                  context.Choices.Add(new Choice { StudentId = "A00100007",
                  StudentFirstName = "SampleFirstName",
                  StudentLastname = "SampleLastName",
                  FirstChoiceOptionId = 4,
                  SecondChoiceOptionId = 6,
                  ThirdChoiceOptionId = 3,
                  FourthChoiceOptionId = 5,
                  SelectionDate = DateTime.Now,
                  YearTermId = 4           
                   });
                   
                  context.Choices.Add(new Choice { StudentId = "A00100008",
                  StudentFirstName = "SampleFirstName",
                  StudentLastname = "SampleLastName",
                  FirstChoiceOptionId = 4,
                  SecondChoiceOptionId = 6,
                  ThirdChoiceOptionId = 3,
                  FourthChoiceOptionId = 5,
                  SelectionDate = DateTime.Now,
                  YearTermId = 4           
                   });

                context.SaveChanges();
            }
        }
    }
}
