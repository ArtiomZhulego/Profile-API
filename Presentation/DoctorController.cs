﻿using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation
{
    [ApiController]
    [Route("doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService doctorService;

        public DoctorController(DoctorService doctorService) => this.doctorService = doctorService;

        /// <summary>
        /// View doctors
        /// </summary>
        /// <param name="token"></param>
        /// <returns>All Doctors</returns>
        [HttpGet]
        public async Task<IActionResult> GetDoctors(CancellationToken token)
        {
            var doctorsDTO = await doctorService.GetAllAsync(token);

            return StatusCode(200, doctorsDTO);
        }

        /// <summary>
        /// View doctor with identification
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="token"></param>
        /// <returns>Doctor with identification</returns>
        [HttpGet("{doctorId:guid}")]
        public async Task<IActionResult> GetDoctor(Guid doctorId, CancellationToken token)
        {
            var doctorDTO = await doctorService.GetByIdAsync(doctorId, token);

            return StatusCode(200, doctorDTO);
        }

        /// <summary>
        /// Patch Status
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="statusId"></param>
        /// <param name="token"></param>
        /// <returns>Doctor's dto</returns>
        [HttpPatch("{doctorId:guid}")]
        public async Task<IActionResult> PatchStatus(Guid doctorId, int statusId, CancellationToken token)
        {
            var doctorDTO = await doctorService.PatchStatus(doctorId, statusId, token);

            return StatusCode(200, doctorDTO);
        }

        /// <summary>
        /// Update Doctor
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="doctorDTO"></param>
        /// <param name="token"></param>
        /// <returns>Doctor's dto</returns>
        [HttpPut("{doctorId:guid}")]
        public async Task<IActionResult> PutDoctor(Guid doctorId, DoctorDTO doctorDTO, CancellationToken token)
        {
            var _doctorDTO = await doctorService.Update(doctorId, doctorDTO, token);

            return StatusCode(200, _doctorDTO);
        }

        /// <summary>
        /// Create a Doctor
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Created and info</returns>
        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CancellationToken token)
        {
            var doctor = await doctorService.Create(token);

            return Created($"{doctor.Id}", doctor);
        }

        /// <summary>
        /// Delete a Doctor
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="token"></param>
        /// <returns>No Content</returns>
        [HttpDelete("{doctorId:guid}")]
        public async Task<IActionResult> DeleteDoctor(Guid doctorId, CancellationToken token)
        {
            await doctorService.Delete(doctorId, token);

            return NoContent();
        }

        /// <summary>
        /// Filter doctors
        /// </summary>
        /// <param name="officeId"></param>
        /// <param name="specialityId"></param>
        /// <param name="token"></param>
        /// <returns>No Content</returns>
        [HttpGet("specialty={specialty}&office={office}")]
        public async Task<IActionResult> FilterDoctor(Guid officeId, Guid specialityId, CancellationToken token)
        {
            var doctorsDTO = await doctorService.FilterDoctor(officeId, specialityId, token);

            return Ok(doctorsDTO);
        }

        /// <summary>
        /// Search doctor by name
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="token"></param>
        /// <returns>Doctor with identification</returns>
        [HttpGet("fullName={fullName}")]
        public async Task<IActionResult> SearchByName(string fullName, CancellationToken token)
        {
            var doctorDTO = await doctorService.SearchByName(fullName, token);

            return StatusCode(200, doctorDTO);
        }
    }
}
