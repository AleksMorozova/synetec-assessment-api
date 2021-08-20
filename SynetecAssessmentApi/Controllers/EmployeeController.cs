using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.BusinessLogic.Interfaces;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        /// <summary>
        /// Get employees
        /// </summary>  
        /// <remarks>
        /// </remarks>
        [HttpGet]
        [Route("getAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await employeeService.GetEmployeesAsync();

            if (!result.IsSucceed)
            {
                return BadRequest("Error during employee reading:" + result.ErrorMessage);
            }
            return Ok(result.Value);
        }
    }
}
