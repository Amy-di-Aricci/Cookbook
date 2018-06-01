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
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [DisplayName("Opis przygotowania")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DisplayName("Poziom trudności")]
        [Range(0,2)]
        public DifficultyEnum Difficulty { get; set; }
        [DisplayName("Data dodania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
    }

    public class RecipeDBContext : DbContext, IDbContext
    {
        public IDbSet<Recipe> Recipes { get; set; }

        IDbSet<TEntity> IDbContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }
    }
}