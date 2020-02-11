using JwtModels;
using JwtWebApi.Repositories.Login;
using JwtWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JwtWebApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class LoginController : ControllerBase
   {
      public IRepositoryLogin RepositoryLogin { get; }
      public ITokenService TokenService { get; }

      public LoginController(IRepositoryLogin repositoryLogin, ITokenService tokenService)
      {
         RepositoryLogin = repositoryLogin ?? throw new ArgumentNullException(nameof(repositoryLogin));
         TokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
      }

      [HttpPost]
      [Route("auth")]
      public IActionResult Authenticate(Login login)
      {
         if (ModelState.IsValid)
         {
            User user = RepositoryLogin.CheckLogin(login);
            if (user == null)
            {
               return NotFound(new { message = "User no found" });
            }
            TokenResult tokenResult = TokenService.GenerateTokenResult(user);
            return new JsonResult(tokenResult);
         }
         return BadRequest(ModelState);
      }
   }
}