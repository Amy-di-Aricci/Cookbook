using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Cookbook.Models
{
    public enum DifficultyEnum{
        [Display(Name = "Łatwy")]
        easy,
        [Display(Name = "Średni")]
        medium,
        [Display(Name = "Trudny")]
        difficult
    }

    public class Recipe
    {
        public int RecipeId { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Opis przygotowania")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DisplayName("Poziom trudności")]
        public DifficultyEnum Difficulty { get; set; }
        [DisplayName("Data dodania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
    }

    public class RecipeDBContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
    }
}