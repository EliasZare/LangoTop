﻿using System;

namespace LangoTop.Application.Contract.DiscountCode
{
    public class DiscountCodeViewModel
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public string Code { get; set; }
        public string Course { get; set; }
        public int DiscountRate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime StartDateGr { get; set; }
        public DateTime EndDateGr { get; set; }
        public string Reason { get; set; }
        public string CreationDate { get; set; }
    }
}