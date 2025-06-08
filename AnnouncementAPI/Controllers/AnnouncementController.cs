using Announcement.Application.Announcement.Commands.Create;
using Announcement.Application.Announcement.Commands.Delete;
using Announcement.Application.Announcement.Commands.Edit;
using Announcement.Application.Announcement.DTOs;
using Announcement.Application.Announcement.Queries.GetSimilarAnnouncements;
using Announcement.Application.Announcement.Query.GetAll;
using Announcement.Application.Announcement.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementAPI.Controllers
{
    /// <summary>
    /// API controller for managing announcements.
    /// Provides endpoints to create, read, update, delete, and get similar announcements.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnouncementController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance used to send commands and queries.</param>
        public AnnouncementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new announcement.
        /// </summary>
        /// <param name="command">The command containing data for the new announcement.</param>
        /// <returns>The ID of the newly created announcement.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateAnnouncementCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        /// <summary>
        /// Retrieves a list of all announcements.
        /// </summary>
        /// <returns>A list of <see cref="AnnouncementDto"/> objects.</returns>
        [HttpGet]
        public async Task<ActionResult<List<AnnouncementDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAnnouncementsListQuery());
            return Ok(result);
        }

        /// <summary>
        /// Retrieves an announcement by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the announcement to retrieve.</param>
        /// <returns>The announcement data if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AnnouncementDto>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetAnnouncementByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Updates an existing announcement.
        /// </summary>
        /// <param name="command">The command containing updated announcement data.</param>
        /// <returns>No content if successful; otherwise, NotFound.</returns>
        [HttpPut]
        public async Task<IActionResult> Edit(EditAnnouncementCommand command)
        {
            var success = await _mediator.Send(command);
            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes an announcement by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the announcement to delete.</param>
        /// <returns>No content if successful; otherwise, NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeleteAnnouncementCommand { Id = id });
            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Retrieves a list of announcements similar to the specified announcement.
        /// </summary>
        /// <param name="id">The ID of the announcement to find similarities for.</param>
        /// <param name="count">The number of similar announcements to retrieve (default is 3).</param>
        /// <returns>A list of similar <see cref="AnnouncementDto"/> objects.</returns>
        [HttpGet("{id}/similar")]
        public async Task<ActionResult<List<AnnouncementDto>>> GetSimilar(Guid id, [FromQuery] int count = 3)
        {
            var query = new GetSimilarAnnouncementsQuery { Id = id, Count = count };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
