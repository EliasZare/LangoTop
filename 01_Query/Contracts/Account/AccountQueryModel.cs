namespace _01_Query.Contracts.Account
{
    public class AccountQueryModel
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Biography { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PaidDate { get; set; }
        public long RoleId { get; set; }
        public string ProfilePhoto { get; set; }
        public string TeacherUsername { get; set; }
    }
}
