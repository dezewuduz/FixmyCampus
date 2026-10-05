using System.Security.Claims;

using FixmyCampus.Application.DTOs.Tickets;
using FixmyCampus.Application.Interfaces;
using FixmyCampus.Domain.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FixmyCampus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    // Get current logged-in user's ID
    private int GetCurrentUserId()
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!int.TryParse(userId, out var id))
        {
            throw new UnauthorizedAccessException(
                "Invalid user ID.");
        }

        return id;
    }

    // POST: api/tickets
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateTicketDto dto)
    {
        var userId = GetCurrentUserId();

        var ticket =
            await _ticketService.CreateAsync(
                dto,
                userId);

        return CreatedAtAction(
            nameof(GetById),
            new { id = ticket.Id },
            ticket);
    }

    // GET: api/tickets
    // GET: api/tickets?building=Library
    // GET: api/tickets?status=1
    // GET: api/tickets?building=Library&status=1
    [HttpGet]
    [Authorize(Roles = "Admin,Technician")]
    public async Task<IActionResult> GetAll(
        Building? building,
        TicketStatus? status)
    {
        var tickets =
            await _ticketService.GetFilteredAsync(
                building,
                status);

        return Ok(tickets);
    }

    // GET: api/tickets/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ticket =
            await _ticketService.GetByIdAsync(id);

        if (ticket == null)
        {
            return NotFound(new
            {
                message = "Ticket not found."
            });
        }

        return Ok(ticket);
    }

    // GET: api/tickets/my
    [HttpGet("my")]
    public async Task<IActionResult> GetMyTickets()
    {
        var userId = GetCurrentUserId();

        var tickets =
            await _ticketService.GetMyTicketsAsync(
                userId);

        return Ok(tickets);
    }

    // PUT: api/tickets/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateTicketDto dto)
    {
        var userId = GetCurrentUserId();

        var ticket =
            await _ticketService.UpdateAsync(
                id,
                dto,
                userId);

        if (ticket == null)
        {
            return NotFound(new
            {
                message =
                    "Ticket not found or you are not the owner."
            });
        }

        return Ok(ticket);
    }

    // DELETE: api/tickets/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetCurrentUserId();

        var deleted =
            await _ticketService.DeleteAsync(
                id,
                userId);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Ticket not found."
            });
        }

        return NoContent();
    }

    // PUT: api/tickets/5/assign
    [HttpPut("{id:int}/assign")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Assign(
        int id,
        AssignTicketDto dto)
    {
        try
        {
            var ticket =
                await _ticketService.AssignAsync(
                    id,
                    dto);

            if (ticket == null)
            {
                return NotFound(new
                {
                    message = "Ticket not found."
                });
            }

            return Ok(ticket);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    // PUT: api/tickets/5/status
    [HttpPut("{id:int}/status")]
    [Authorize(Roles = "Admin,Technician")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        TicketStatus status,
        string? comment)
    {
        var userId = GetCurrentUserId();

        var ticket =
            await _ticketService.UpdateStatusAsync(
                id,
                status,
                userId,
                comment);

        if (ticket == null)
        {
            return NotFound(new
            {
                message = "Ticket not found."
            });
        }

        return Ok(ticket);
    }
}