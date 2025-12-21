using Microsoft.AspNetCore.Mvc;
using Tourist.APPLICATION.DTO.Notification;
using Tourist.APPLICATION.UseCase.Notification;

namespace Tourist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationUseCase _notificationUseCase;

        public NotificationController(NotificationUseCase notificationUseCase)
        {
            _notificationUseCase = notificationUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationUseCase.GetAllAsync();
            return Ok(notifications);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var notification = await _notificationUseCase.GetByIdAsync(id);
            if (notification == null)
                return NotFound();
            return Ok(notification);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotificationDTOs dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdId = await _notificationUseCase.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdId }, createdId);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateNotificationDTOs dto)
        {
            if (dto.NotificationId == null)
                return BadRequest("NotificationId is required");

            var updated = await _notificationUseCase.UpdateAsync(dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _notificationUseCase.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("multiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] IEnumerable<int> ids)
        {
            var deleted = await _notificationUseCase.DeleteMultipleAsync(ids);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
