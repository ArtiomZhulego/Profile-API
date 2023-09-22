using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abstraction;

namespace Presentation
{
    [ApiController]
    [Route("patients")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService patientService;

        public PatientController(PatientService patientService) => this.patientService = patientService;

        /// <summary>
        /// View patients
        /// </summary>
        /// <param name="token"></param>
        /// <returns>All Patients</returns>
        [HttpGet]
        public async Task<IActionResult> GetPatients(CancellationToken token)
        {
            var patientDTO = await patientService.GetAllAsync(token);

            return StatusCode(200, patientDTO);
        }

        /// <summary>
        /// View patient with identification
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Patient with identification</returns>
        [HttpGet("{patientId:guid}")]
        public async Task<IActionResult> GetPatient(Guid patientId, CancellationToken token)
        {
            var patientDTO = await patientService.GetByIdAsync(patientId, token);

            return StatusCode(200, patientDTO);
        }

        /// <summary>
        /// Update Patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="token"></param>
        /// <returns>Patients dto</returns>
        [HttpPut("{patientId:guid}")]
        public async Task<IActionResult> PutPatient(Guid patientId, PatientDTO patientDTO, CancellationToken token)
        {
            var _patientDTO = await patientService.Update(patientId, patientDTO, token);

            return StatusCode(200, _patientDTO);
        }

        /// <summary>
        /// Create a Patient
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Created and info</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePatient(CancellationToken token)
        {
            var patient = await patientService.Create(token);

            return Created($"{patient.Id}", patient);
        }

        /// <summary>
        /// Delete a Patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="token"></param>
        /// <returns>No Content</returns>
        [HttpDelete("{patientId:guid}")]
        public async Task<IActionResult> DeletePatient(Guid patientId, CancellationToken token)
        {
            await patientService.Delete(patientId, token);

            return NoContent();
        }

        /// <summary>
        /// Search patient by name
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="token"></param>
        /// <returns>Doctor with identification</returns>
        /*[HttpGet("{fullName:string}")]
        public async Task<IActionResult> SearchByName(string fullName, CancellationToken token)
        {
            var doctorDTO = await patientService.SearchByName(fullName, token);

            return StatusCode(200, doctorDTO);
        }*/
    }
}
