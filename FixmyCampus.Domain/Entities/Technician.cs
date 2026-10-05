using FixmyCampus.Domain.Enums;

namespace FixmyCampus.Domain.Entities;

public class Technician : User
{
   public int TechnicianId { get; set; }
   public TicketCategory Specialization { get; set; }
    public bool IsAvailable { get; set; } = true;

    // Tickets assigned to this technician
    public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
}