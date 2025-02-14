﻿namespace WeddingShare.Models.Database
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; }
        public int FailedLogins { get; set; }
        public DateTime? LockoutUntil { get; set; }
        public string? MultiFactorToken { get; set; }

        public bool IsLockedOut
        {
            get 
            {
                return this.LockoutUntil != null && this.LockoutUntil >= DateTime.UtcNow;
            }
        }
    }
}