namespace Data.Entities;

public class Answer: EntityBase
{
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public bool IsCorrect { get; set; }

    public int QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;
    
}