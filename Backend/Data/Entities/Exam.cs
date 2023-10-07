using Data.Enums;

namespace Data.Entities;

public class Exam : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ExamVendorEnum ExamVendor { get; set; }
    public bool IsActive { get; set; }
    public bool IsRetired { get; set; }
    public DateOnly? RetirementDate { get; set; }
}