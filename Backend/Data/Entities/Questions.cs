namespace Data.Entities;

public class Question : EntityBase
{
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }

    public int ExamId { get; set; }

    public virtual Exam Exam { get; set; } = null!;
    public virtual ICollection<Answer> Answers { get; set; } = null!;
}