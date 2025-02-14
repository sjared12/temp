// File: Models/Gallery.cs
namespace WeddingShare.Models
{
    public class Gallery
    {
        public int GalleryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CustomHomepageContent { get; set; } // HTML content
        public int GroupId { get; set; } // Assign gallery to a group
    }
}
// Compare this snippet from WeddingShare/Controllers/GalleryController.cs: 