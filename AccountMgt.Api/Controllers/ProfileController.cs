﻿using AccountMgt.Core.IServices;
using AccountMgt.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AccountMgt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileSservices _profileServices;

        public ProfileController(IProfileSservices profileServices)
        {
            _profileServices = profileServices;
        }

        [HttpPost("create-profile")]
        public async Task<IActionResult> CreateUserProfile([FromBody] ProfileDto profile)
        {
            return Ok(await _profileServices.CreateProfile(profile));
        }

        [HttpPut("updateprofile-balance")]
        public async Task<IActionResult> ProfileBalance([FromBody] UpdateProfileBalanceDto profileBalance)
        {
            return Ok(await _profileServices.UpdateProfileBalance(profileBalance));
        }

        [HttpGet("get-all-profiles-by-userId")]
        public async Task<ActionResult<IList<GetAllProfileDto>>> GetAllProfileByUserId(string userId)
        {
            return Ok(await _profileServices.GetAllProfileByUserId(userId));
        }

        [HttpGet("get-profile-by-id")]
        public async Task<IActionResult> GetProfileById(string Id)
        {
            return Ok(await _profileServices.GetProfileById(Id));
        }

        [HttpPut]
        public async Task DeleteUserProfileById(string id)
        {
            await _profileServices.DeleteProfileById(id);
        }

    }
}
