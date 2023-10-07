using Data.Account;

namespace Data.Entities;

public class UserExam : EntityBase
{
    public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;
    public TimeSpan Duration { get; set; }
    public int Score { get; set; }

    public int UserId { get; set; }
    public int ExamId { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;
    public virtual Exam Exam { get; set; } = null!;
}