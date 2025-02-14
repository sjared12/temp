namespace WeddingShare.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int GalleryId { get; set; }
    }
}