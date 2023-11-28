using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Presentation
{
    [ApiController]
    [Route("receptionist")]
    public class ReceptionistController : ControllerBase
    {
        private readonly IReceptionistService _service;

        public ReceptionistController(IReceptionistService receptionistService)
        {
            _service = receptionistService;
        }

        /// <summary>
        /// View receptionist
        /// </summary>
        /// <param name="token"></param>
        /// <returns>All Receptionists</returns>
        [HttpGet]
        public async Task<IActionResult> GetReceptionistsAsync(CancellationToken token)
        {
            var receptionistDTO = await _service.GetAllAsync(token);

            return Ok(receptionistDTO);
        }

        /// <summary>
        /// View receptionist with identification
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="token"></param>
        /// <returns>Receptionist with identification</returns>
        [HttpGet("{receptionistId:guid}")]
        public async Task<IActionResult> GetReceptionistAsync(Guid receptionistId, CancellationToken token)
        {
            var receptionistDTO = await _service.GetByIdAsync(receptionistId, token);

            return Ok(receptionistDTO);
        }

        /// <summary>
        /// Update receptionist
        /// </summary>
        /// <param name="receptionistId"></param>
        /// <param name="receptionistDTO"></param>
        /// <param name="token"></param>
        /// <returns>Receptionist DTO</returns>
        [HttpPut("{receptionistId:guid}")]
        public async Task<IActionResult> UpdateDoctorAsync(Guid receptionistId, ReceptionistDTO receptionistDTO, CancellationToken token)
        {
            var receptionist = await _service.UpdateAsync(receptionistId, receptionistDTO, token);

            return Ok(receptionist);
        }

        /// <summary>
        /// Create a receptionist
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Created and info</returns>
        [HttpPost]
        public async Task<IActionResult> CreateReceptionistAsync(ReceptionistDTO _receptionistDTO, CancellationToken token)
        {
            var receptionistDTO = await _service.CreateAsync(_receptionistDTO,token);

            return Created($"{receptionistDTO.Id}", receptionistDTO);
        }

        /// <summary>
        /// Delete a receptionist
        /// </summary>
        /// <param name="receptionistId"></param>
        /// <param name="token"></param>
        /// <returns>No Content</returns>
        [HttpDelete("{receptionistId:guid}")]
        public async Task<IActionResult> DeleteReceptionistAsync(Guid receptionistId, CancellationToken token)
        {
            await _service.DeleteAsync(receptionistId, token);

            return NoContent();
        }
    }
}
