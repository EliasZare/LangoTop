﻿namespace LangoTop.Application.Contract.Order
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public string Mobile { get; set; }
        public long AccountId { get; set; }
        public string AccountFullName { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }
        public string IssueTrackingNo { get; set; }
        public long RefId { get; set; }
        public string CreationDate { get; set; }
    }
}