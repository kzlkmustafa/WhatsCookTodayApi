using System.ComponentModel.DataAnnotations;

namespace WhatsCookTodayApi.MyModels
{
    public class MealOfDay
    {
        [Key]
        public int MealOfDayId { get; set; }
        [StringLength(50)]
        public string MealOfDayName { get; set; }
        public string MealOfDayRecipe { get; set; }
        [StringLength(100)]
        public string MealOfDayPhoto { get; set; }
    }
}
