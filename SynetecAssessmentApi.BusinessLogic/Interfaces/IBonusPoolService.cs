
using SynetecAssessmentApi.Model;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.BusinessLogic.Interfaces
{
    public interface IBonusPoolService
    {
        public Task<Result<BonusPoolCalculatorResultDto>> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId);
    }
}
