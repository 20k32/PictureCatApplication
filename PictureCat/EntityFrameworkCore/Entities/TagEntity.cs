using System.Collections.Generic;

namespace PictureCat
{
    public class TagEntity
    {
        public int Id { get; set; }
        public string TagName { get; set; } = null!;
        public List<ImageToTag> ImageTags { get; set; } = null!;
    }
}
