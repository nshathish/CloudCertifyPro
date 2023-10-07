namespace Data.Entities;

public class UserExamQuestion: EntityBase
{
    public int UserExamId { get; set; }
    public int QuestionId { get; set; }

    public virtual UserExam UserExam { get; set; } = null!;
    public virtual Question Question { get; set; } = null!;
    
}