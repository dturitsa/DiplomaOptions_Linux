using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using DiplomaWebSite.Models;

namespace DiplomaWebSite.Migrations.Options
{
    [DbContext(typeof(OptionsContext))]
    partial class OptionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("DiplomaWebSite.Models.Choice", b =>
                {
                    b.Property<int>("ChoiceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FirstChoiceOptionId");

                    b.Property<int>("FourthChoiceOptionId");

                    b.Property<int>("SecondChoiceOptionId");

                    b.Property<DateTime>("SelectionDate");

                    b.Property<string>("StudentFirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 40);

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 9);

                    b.Property<string>("StudentLastname")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 40);

                    b.Property<int>("ThirdChoiceOptionId");

                    b.Property<int>("YearTermId");

                    b.Property<int?>("optionsOptionId");

                    b.HasKey("ChoiceId");
                });

            modelBuilder.Entity("DiplomaWebSite.Models.Option", b =>
                {
                    b.Property<int>("OptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("isActive");

                    b.Property<string>("title");

                    b.HasKey("OptionId");
                });

            modelBuilder.Entity("DiplomaWebSite.Models.YearTerm", b =>
                {
                    b.Property<int>("YearTermId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("isDefault");

                    b.Property<int>("term");

                    b.Property<int>("year");

                    b.HasKey("YearTermId");
                });

            modelBuilder.Entity("DiplomaWebSite.Models.Choice", b =>
                {
                    b.HasOne("DiplomaWebSite.Models.YearTerm")
                        .WithMany()
                        .HasForeignKey("YearTermId");

                    b.HasOne("DiplomaWebSite.Models.Option")
                        .WithMany()
                        .HasForeignKey("optionsOptionId");
                });
        }
    }
}
