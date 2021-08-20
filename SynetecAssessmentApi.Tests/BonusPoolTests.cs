using FluentAssertions;
using Moq;
using SynetecAssessmentApi.BusinessLogic.Services;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace SynetecAssessmentApi.Tests
{
    public class BonusPoolTests
    {
        private readonly Mock<IEmployeeRepository> employeeRepository = new Mock<IEmployeeRepository>();
        private BonusPoolService bonusPoolService;

        public BonusPoolTests()
        {
            bonusPoolService = new BonusPoolService(employeeRepository.Object);
        }


        [Fact]
        public async Task VerifyTest()
        {
            // Arrange
            int bonusPoolAmount = 1000;
            int employeeId = 1;
            Employee employee = new Employee(1, "", "", 525, 1);
            employee.Department = new Department(1, "", "");

            employeeRepository.Setup(x => x.GetEmployeeById(employeeId)).Returns(Task.FromResult(employee));
            employeeRepository.Setup(x => x.GetEmployeesTotalSalary()).Returns(1500);

            // Act
            var result = await bonusPoolService.CalculateAsync(bonusPoolAmount, employeeId);

            // Assert
            result.Value.Amount.Equals(350);
        }

        [Fact]
        public async Task NullBonusPoolTest()
        {
            // Arrange
            int bonusPoolAmount = 0;
            int employeeId = 1;
            Employee employee = new Employee(1, "", "", 525, 1);
            employee.Department = new Department(1, "", "");

            employeeRepository.Setup(x => x.GetEmployeeById(employeeId)).Returns(Task.FromResult(employee));
            employeeRepository.Setup(x => x.GetEmployeesTotalSalary()).Returns(1500);

            // Act
            var result = await bonusPoolService.CalculateAsync(bonusPoolAmount, employeeId);

            // Assert
            result.Value.Amount.Equals(0);
        }

        [Fact]
        public async Task NullEmployeeSalaryTest()
        {
            // Arrange
            int bonusPoolAmount = 0;
            int employeeId = 1;
            Employee employee = new Employee(1, "", "", 0, 1);
            employee.Department = new Department(1, "", "");

            employeeRepository.Setup(x => x.GetEmployeeById(employeeId)).Returns(Task.FromResult(employee));
            employeeRepository.Setup(x => x.GetEmployeesTotalSalary()).Returns(1500);

            // Act
            var result = await bonusPoolService.CalculateAsync(bonusPoolAmount, employeeId);

            // Assert
            result.Value.Amount.Equals(0);
        }


        [Fact]
        public async Task NullEmployeeTest()
        {
            // Arrange
            int bonusPoolAmount = 1000;
            int employeeId = 1;
            Employee employee = null;

            employeeRepository.Setup(x => x.GetEmployeeById(employeeId)).Returns(Task.FromResult(employee));
            employeeRepository.Setup(x => x.GetEmployeesTotalSalary()).Returns(1500);

            // Act
            var result = await bonusPoolService.CalculateAsync(bonusPoolAmount, employeeId);

            // Assert
            result.IsSucceed.Should().BeFalse();
            result.ErrorMessage.Should().Be($"Employee with id={employeeId} doesn't exist");

        }
    }
}
