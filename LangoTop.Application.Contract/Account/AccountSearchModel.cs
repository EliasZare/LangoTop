﻿namespace LangoTop.Application.Contract.Account
{
    public class AccountSearchModel
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public long RoleId { get; set; }
    }
}