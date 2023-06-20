using System.ComponentModel.DataAnnotations;

namespace WhatsCookTodayApi.MyModels.ViewModels
{
    public class MyPromptViewModel
    {
        [Required(ErrorMessage = "materyalleri giriniz")]
        public string Materials { get; set; }
        public string Id { get; set; }
    }
}
