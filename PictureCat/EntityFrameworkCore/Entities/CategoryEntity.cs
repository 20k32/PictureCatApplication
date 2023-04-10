using System.Collections.Generic;

namespace PictureCat
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public List<ImageToCategory> ImageCategories { get; set; } = null!;
    }
}
