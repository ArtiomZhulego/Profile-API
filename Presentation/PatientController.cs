using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Presentation
{
    [ApiController]
    [Route("patients")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;

        public PatientController(IPatientService patientService) => this.patientService = patientService;

        /// <summary>
        /// View patients
        /// </summary>
        /// <param name="token"></param>
        /// <returns>All Patients</returns>
        [HttpGet]
        public async Task<IActionResult> GetPatientsAsync(CancellationToken token)
        {
            var patientDTO = await patientService.GetAllAsync(token);

            return Ok(patientDTO);
        }

        /// <summary>
        /// View patient with identification
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Patient with identification</returns>
        [HttpGet("{patientId:guid}")]
        public async Task<IActionResult> GetPatientAsync(Guid patientId, CancellationToken token)
        {
            var patientDTO = await patientService.GetByIdAsync(patientId, token);

            return Ok(patientDTO);
        }

        /// <summary>
        /// Update Patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="token"></param>
        /// <returns>Patients dto</returns>
        [HttpPut("{patientId:guid}")]
        public async Task<IActionResult> PutPatientAsync(Guid patientId, PatientDTO patientDTO, CancellationToken token)
        {
            var _patientDTO = await patientService.UpdateAsync(patientId, patientDTO, token);

            return Ok(_patientDTO);
        }

        /// <summary>
        /// Create a Patient
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Created and info</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePatientAsync(PatientDTO patientDTO, CancellationToken token)
        {
            var patient = await patientService.CreateAsync(patientDTO,token);

            return Created($"{patient.Id}", patient);
        }

        /// <summary>
        /// Delete a Patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="token"></param>
        /// <returns>No Content</returns>
        [HttpDelete("{patientId:guid}")]
        public async Task<IActionResult> DeletePatientAsync(Guid patientId, CancellationToken token)
        {
            await patientService.DeleteAsync(patientId, token);

            return NoContent();
        }
    }
}
