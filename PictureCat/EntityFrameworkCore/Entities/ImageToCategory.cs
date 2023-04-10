namespace PictureCat
{
    /// <summary>
    /// ManyToMany representation using explicitly created third table
    /// </summary>
    public class ImageToCategory
    {
        public int ImageId { get; set; }
        public ImageEntity ImageEntity { get; set; } = null!;
        public int CategoryId { get; set; }
        public CategoryEntity CategoryEntity { get; set; } = null!;
    }
}
