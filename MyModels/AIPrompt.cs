using System.ComponentModel.DataAnnotations;

namespace WhatsCookTodayApi.MyModels
{
    public class AIPrompt
    {
        [Key]
        public int AIPromptId { get; set; }
        [StringLength(100)]
        public string MyPromptsMaterials { get; set; }
        public string AIPromptRecipe { get; set; }
        public string? Id { get; set; }
        public MyUsers User { get; set; }
        public int? MyPromptId { get; set; }
        public MyPrompt MyPrompt { get; set; }
    }
}
