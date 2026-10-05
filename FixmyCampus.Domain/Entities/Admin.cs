using FixmyCampus.Domain.Enums;

namespace FixmyCampus.Domain.Entities;

public class Admin : User
{
    
    public int AdminId { get; set; }
    
    public string? Specialization { get; set; }
}