using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Entities;

namespace WebApi.Controllers
{
    // [Microsoft.AspNetCore.Mvc.Route("api/[controller]/[action]")]
    // [ApiController]
    // public class UserController : ControllerBase
    // {
    //     
    //     private readonly ApplicationDbContext _applicationDb;
    //
    //     public UserController(ApplicationDbContext applicationDbContext)
    //     {
    //         _applicationDb = applicationDbContext;
    //     }
    //
    //     
    //     [HttpPost]
    //     [Microsoft.AspNetCore.Mvc.Route("api/user/[action]")]
    //     [Authorize]
    //     public async Task<ActionResult<Guid>>  GetUserId(IncommingUserViewModel incomingUser)
    //     {
    //         var user = await _applicationDb.Users.FindAsync(incomingUser.UserId);
    //
    //         if (user == null)
    //         {
    //F
    //             user = new Entities.User()
    //             {
    //                 Id = incomingUser.UserId,
    //                 UserName = incomingUser.UserName,
    //                 UserImage = incomingUser.UserImagePath
    //             };
    //
    //            await _applicationDb.Users.AddAsync(user);
    //         }
    //
    //         return user.Id;
    //     }
    // }
}