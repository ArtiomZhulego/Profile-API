using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Presentation
{
    [ApiController]
    [Route("doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService doctorService;

        public DoctorController(IDoctorService doctorService) => this.doctorService = doctorService;

        /// <summary>
        /// View doctors
        /// </summary>
        /// <param name="token"></param>
        /// <returns>All Doctors</returns>
        [HttpGet]
        public async Task<IActionResult> GetDoctorsAsync(CancellationToken token)
        {
            var doctorsDTO = await doctorService.GetAllAsync(token);

            return Ok(doctorsDTO);
        }

        /// <summary>
        /// View doctor with identification
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="token"></param>
        /// <returns>Doctor with identification</returns>
        [HttpGet("{doctorId:guid}")]
        public async Task<IActionResult> GetDoctorAsync(Guid doctorId, CancellationToken token)
        {
            var doctorDTO = await doctorService.GetByIdAsync(doctorId, token);

            return Ok(doctorDTO);
        }

        /// <summary>
        /// Patch Status
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="statusId"></param>
        /// <param name="token"></param>
        /// <returns>Doctor's dto</returns>
        [HttpPatch("{doctorId:guid}")]
        public async Task<IActionResult> UpdateStatusAsync(Guid doctorId, int statusId, CancellationToken token)
        {
            var doctorDTO = await doctorService.UpdateStatusAsync(doctorId, statusId, token);

            return Ok(doctorDTO);
        }

        /// <summary>
        /// Update Doctor
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="doctorDTO"></param>
        /// <param name="token"></param>
        /// <returns>Doctor's dto</returns>
        [HttpPut("{doctorId:guid}")]
        public async Task<IActionResult> UpdateDoctorAsync(Guid doctorId, DoctorDTO doctorDTO, CancellationToken token)
        {
            var _doctorDTO = await doctorService.UpdateAsync(doctorId, doctorDTO, token);

            return Ok(_doctorDTO);
        }

        /// <summary>
        /// Create a Doctor
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Created and info</returns>
        [HttpPost]
        public async Task<IActionResult> CreateDoctorAsync(DoctorDTO doctorDTO, CancellationToken token)
        {
            var doctor = await doctorService.CreateAsync(doctorDTO,token);

            return Created($"{doctor.Id}", doctor);
        }

        /// <summary>
        /// Delete a Doctor
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="token"></param>
        /// <returns>No Content</returns>
        [HttpDelete("{doctorId:guid}")]
        public async Task<IActionResult> DeleteDoctorAsync(Guid doctorId, CancellationToken token)
        {
            await doctorService.DeleteAsync(doctorId, token);

            return NoContent();
        }

        /// <summary>
        /// Filter doctors
        /// </summary>
        /// <param name="officeId"></param>
        /// <param name="specialityId"></param>
        /// <param name="token"></param>
        /// <returns>No Content</returns>
        [HttpGet("{officeId:guid}/{specialityId:guid}")]
        public async Task<IActionResult> FilterDoctorAsyc(Guid officeId, Guid specialityId, CancellationToken token)
        {
            var doctorsDTO = await doctorService.FilterDoctorAsync(officeId, specialityId, token);

            return Ok(doctorsDTO);
        }

        /// <summary>
        /// Search doctor by name
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="token"></param>
        /// <returns>Doctor with identification</returns>
        [HttpGet("fullName={fullName}")]
        public async Task<IActionResult> SearchByNameAsync(string fullName, CancellationToken token)
        {
            var doctorDTO = await doctorService.SearchByNameAsync(fullName, token);

            return Ok(doctorDTO);
        }
    }
}
