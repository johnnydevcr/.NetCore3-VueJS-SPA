using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.DTOs;
using Model.Identity;
using Service;
using Service.Commons;

namespace Core.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create(ApplicationUserRegisterDTO model) {
            var result = await _userManager.CreateAsync(new ApplicationUser
            {
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            }, model.Password);

            if (!result.Succeeded) {
                throw new Exception("Error");
            }
            return Ok();
        }
    }
}
