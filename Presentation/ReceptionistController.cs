using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abstraction;

namespace Presentation
{
    [ApiController]
    [Route("receptionist")]
    public class ReceptionistController : ControllerBase
    {
        private readonly ReceptionistService receptionistService;

        public ReceptionistController(ReceptionistService receptionistService) => this.receptionistService = receptionistService;

        /// <summary>
        /// View receptionist
        /// </summary>
        /// <param name="token"></param>
        /// <returns>All Receptionists</returns>
        [HttpGet]
        public async Task<IActionResult> GetReceptionists(CancellationToken token)
        {
            var receptionistDTO = await receptionistService.GetAllAsync(token);

            return StatusCode(200, receptionistDTO);
        }

        /// <summary>
        /// View receptionist with identification
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="token"></param>
        /// <returns>Receptionist with identification</returns>
        [HttpGet("{receptionistId:guid}")]
        public async Task<IActionResult> GetReceptionist(Guid receptionistId, CancellationToken token)
        {
            var receptionistDTO = await receptionistService.GetByIdAsync(receptionistId, token);

            return StatusCode(200, receptionistDTO);
        }

        /// <summary>
        /// Update receptionist
        /// </summary>
        /// <param name="receptionistId"></param>
        /// <param name="receptionistDTO"></param>
        /// <param name="token"></param>
        /// <returns>Receptionist DTO</returns>
        [HttpPut("{receptionistId:guid}")]
        public async Task<IActionResult> PutDoctor(Guid receptionistId, ReceptionistDTO receptionistDTO, CancellationToken token)
        {
            var _receptionistDTO = await receptionistService.Update(receptionistId, receptionistDTO, token);

            return StatusCode(200, _receptionistDTO);
        }

        /// <summary>
        /// Create a receptionist
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Created and info</returns>
        [HttpPost]
        public async Task<IActionResult> CreateReceptionist(CancellationToken token)
        {
            var receptionistDTO = await receptionistService.Create(token);

            return Created($"{receptionistDTO.Id}", receptionistDTO);
        }

        /// <summary>
        /// Delete a receptionist
        /// </summary>
        /// <param name="receptionistId"></param>
        /// <param name="token"></param>
        /// <returns>No Content</returns>
        [HttpDelete("{receptionistId:guid}")]
        public async Task<IActionResult> DeleteReceptionist(Guid receptionistId, CancellationToken token)
        {
            await receptionistService.Delete(receptionistId, token);

            return NoContent();
        }
    }
}
