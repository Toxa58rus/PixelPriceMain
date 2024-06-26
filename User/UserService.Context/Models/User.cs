﻿namespace UserService.Context.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool Active { get; set; }
        public virtual ImageUser ImageUser { get; set; }
    }
}
