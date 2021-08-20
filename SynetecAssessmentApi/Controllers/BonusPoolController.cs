using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.BusinessLogic.Interfaces;
using SynetecAssessmentApi.Model;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class BonusPoolController : ControllerBase
    {
        private readonly IBonusPoolService bonusPoolService;

        public BonusPoolController(IBonusPoolService bonusPoolService)
        {
            this.bonusPoolService = bonusPoolService;
        }

        // TODO: Extract separate model for aapi and provide it validation
        [HttpPost()]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            if (request.SelectedEmployeeId == 0)
            {
                return BadRequest("Employee id doesn't specified");
            }

            var result = await bonusPoolService.CalculateAsync(request.TotalBonusPoolAmount, request.SelectedEmployeeId);

            if (!result.IsSucceed)
            {
                return BadRequest("Problem with bonus calculation:" + result.ErrorMessage);
            }
            return Ok(result.Value);
        }
    }
}
