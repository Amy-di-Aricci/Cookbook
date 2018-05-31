using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Cookbook.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public DateTime PublishDate { get; set; }
    }
}