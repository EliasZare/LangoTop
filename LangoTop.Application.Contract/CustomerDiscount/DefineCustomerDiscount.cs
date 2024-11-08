﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using LangoTop.Application.Contract.Course;

namespace LangoTop.Application.Contract.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        [Range(1, 100000, ErrorMessage = ValidationMessage.IsRequired)]
        public long CourseId { get; set; }

        [Range(1, 100000, ErrorMessage = ValidationMessage.IsRequired)]
        public int DiscountRate { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string StartDate { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string EndDate { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(500, ErrorMessage = "تعداد کاراکترها بیشتر از حد مجاز می باشد")]
        public string Reason { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}