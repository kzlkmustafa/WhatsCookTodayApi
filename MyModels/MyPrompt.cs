using System.ComponentModel.DataAnnotations;

namespace WhatsCookTodayApi.MyModels
{
    public class MyPrompt
    {
        [Key]
        public int MyPromptId { get; set; }
        public string Materials { get; set; }
        public string Id { get; set; }
        public MyUsers User { get; set; }
        
        public List<AIPrompt> AIPrompts { get; set; } 
    }
}
