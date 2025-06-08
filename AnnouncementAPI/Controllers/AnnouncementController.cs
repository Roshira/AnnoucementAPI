using Announcement.Application.Announcement.Commands;
using Announcement.Application.Announcement.DTOs;
using Announcement.Application.Announcement.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AnnouncementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IMediator _mediator;

   public AnnouncementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnnouncementCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<AnnouncementDto>>> Get()
        {
            var result = await _mediator.Send(new GetAnnouncementsListQuery());
            return Ok(result);
        }
    }
}
