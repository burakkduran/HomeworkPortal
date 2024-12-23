﻿using System.ComponentModel.DataAnnotations;

namespace HomeworkPortal.ViewModels
{
    public class LessonModel : BaseModel
    {

        [Display(Name = "Ders Adı")]
        [Required(ErrorMessage = "Ders Adı Giriniz!")]
        public string Name { get; set; }


        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Kategori Giriniz!")]
        public int CategoryId { get; set; }
    }
}
