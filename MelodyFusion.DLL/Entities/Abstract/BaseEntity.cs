namespace MelodyFusion.DLL.Entities.Abstract;

public class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastModifiedDate { get; set; } = DateTime.UtcNow;

}