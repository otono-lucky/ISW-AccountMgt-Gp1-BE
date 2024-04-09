using AccountMgt.Core.IServices;
using AccountMgt.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountMgt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileSservices _profileServices;
        public ProfileController(IProfileSservices profileSservices)
        {
            _profileServices = profileSservices;
        }

        [HttpGet("get-all-profiles-by-userId")]
        public async Task<ActionResult<IList<GetAllProfileDto>>> GetAllProfileByUserId(Guid userId)
        {
            return Ok(await _profileServices.GetAllProfileByUserId(userId));
        }
    }
}
