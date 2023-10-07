namespace Data.Entities;

public class UserExamAnswer : EntityBase
{
    public int UserExamQuestionId { get; set; }
    public int AnswerId { get; set; }

    public virtual UserExamQuestion UserExamQuestion { get; set; } = null!;
    public virtual Answer Answer { get; set; } = null!;
}