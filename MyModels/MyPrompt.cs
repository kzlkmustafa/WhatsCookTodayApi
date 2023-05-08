using System.ComponentModel.DataAnnotations;

namespace WhatsCookTodayApi.MyModels
{
    public class MyPrompt
    {
        [Key]
        public int MyPromptId { get; set; }
        public string Materials { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        
        public List<AIPrompt> AIPrompts { get; set; } 
    }
}
