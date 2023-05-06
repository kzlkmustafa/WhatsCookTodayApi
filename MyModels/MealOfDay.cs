using System.ComponentModel.DataAnnotations;

namespace WhatsCookTodayApi.MyModels
{
    public class MealOfDay
    {
        [Key]
        public int MealOfDayId { get; set; }
        public string MealOfDayName { get; set; }
        public string MealOfDayRecipe { get; set; }
        public string MealOfDayPhoto { get; set; }
    }
}
