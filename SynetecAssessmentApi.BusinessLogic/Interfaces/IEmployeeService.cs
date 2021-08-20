using SynetecAssessmentApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        public Task<Result<List<EmployeeDto>>> GetEmployeesAsync();
    }
}
