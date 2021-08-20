using SynetecAssessmentApi.BusinessLogic.Interfaces;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Model;
using SynetecAssessmentApi.Persistence.Interfaces;
using System;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.BusinessLogic.Services
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly IEmployeeRepository employeeRepository;
        public BonusPoolService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<Result<BonusPoolCalculatorResultDto>> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId)
        {
            try
            {
                //load the details of the selected employee using the Id
                Employee employee = await employeeRepository.GetEmployeeById(selectedEmployeeId);

                if (employee == null)
                {
                    return Result<BonusPoolCalculatorResultDto>.Fail($"Employee with id={selectedEmployeeId} doesn't exist");
                }

                //get the total salary budget for the company
                int totalSalary = employeeRepository.GetEmployeesTotalSalary();

                //calculate the bonus allocation for the employee
                decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
                int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);

                var bonus = new BonusPoolCalculatorResultDto
                {
                    Employee = new EmployeeDto
                    {
                        Fullname = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        Department = new DepartmentDto
                        {
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    },

                    Amount = bonusAllocation
                };

                return Result<BonusPoolCalculatorResultDto>.Ok(bonus);
            }
            catch (Exception e)
            {
                return Result<BonusPoolCalculatorResultDto>.Fail(e.Message);
            }
        }
    }
}
