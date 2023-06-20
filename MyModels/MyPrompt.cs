using System.ComponentModel.DataAnnotations;

namespace WhatsCookTodayApi.MyModels
{
    public class MyPrompt
    {
        [Key]
        public int MyPromptId { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage ="materyalleri giriniz")]
        public string Materials { get; set; }
        public string Id { get; set; }
        public MyUsers User { get; set; }
        
        public List<AIPrompt> AIPrompts { get; set; } 
    }
}
