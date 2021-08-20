using SynetecAssessmentApi.BusinessLogic.Interfaces;
using SynetecAssessmentApi.Model;
using SynetecAssessmentApi.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.BusinessLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public async Task<Result<List<EmployeeDto>>> GetEmployeesAsync()
        {
            try
            {
                var employees = await employeeRepository.GetAll();

                List<EmployeeDto> result = new List<EmployeeDto>();

                foreach (var employee in employees)
                {
                    result.Add(
                        new EmployeeDto
                        {
                            Fullname = employee.Fullname,
                            JobTitle = employee.JobTitle,
                            Salary = employee.Salary,
                            Department = new DepartmentDto
                            {
                                Title = employee.Department.Title,
                                Description = employee.Department.Description
                            }
                        });
                }

                return Result<List<EmployeeDto>>.Ok(result);
            }
            catch (Exception e)
            {
                return Result<List<EmployeeDto>>.Fail(e.Message);
            }
        }
    }
}
