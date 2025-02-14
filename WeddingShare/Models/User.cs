// File: Models/User.cs
using System.Collections.Generic;

namespace WeddingShare.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public List<int> GroupIds { get; set; } // For GroupAdmin
        public List<int> GalleryIds { get; set; } // For GalleryAdmin
    }
}
// Compare this snippet from WeddingShare/Models/User.cs:
