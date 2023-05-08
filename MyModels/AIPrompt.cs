using System.ComponentModel.DataAnnotations;

namespace WhatsCookTodayApi.MyModels
{
    public class AIPrompt
    {
        [Key]
        public int AIPromptId { get; set; }
        public string MyPromptsMaterials { get; set; }
        public string AIPromptRecipe { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int MyPromptId { get; set; }
        public MyPrompt MyPrompt { get; set; }
    }
}
