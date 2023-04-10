using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace PictureCat
{
    public class ImageEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Path { get; set; } = null!;
        public byte[] ImageBytes { get; set; } = null!;
        public DateTime? ReleaseDate { get; set; }
        public bool Liked { get; set; }
        public List<ImageToCategory> ImageCategories { get; set; } = null!;
        public List<ImageToTag> ImageTags { get; set; } = null!;
    }
}
