using PictureCat;
/// <summary>
/// ManyToMany representation using explicitly created third table
/// </summary>
public class ImageToTag
{
    public int ImageId { get; set; }
    public ImageEntity ImageEntity { get; set; } = null!;
    public int TagId { get; set; }
    public TagEntity TagEntity { get; set; } = null!;
}
