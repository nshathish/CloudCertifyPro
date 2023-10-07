namespace Data.Entities;

public abstract class EntityBase
{
    public int Id { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    
}