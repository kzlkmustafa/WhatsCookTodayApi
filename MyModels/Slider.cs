using System.ComponentModel.DataAnnotations;

namespace WhatsCookTodayApi.MyModels
{
    public class Slider
    {
        [Key]
        public int SilderId { get; set; }
        public string SliderContent { get; set; }
        public string SliderPhoto { get; set; }

    }
}
