namespace ApiGateways.Common.Models.User
{
    public class Users
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool Active { get; set; }
    }
}
