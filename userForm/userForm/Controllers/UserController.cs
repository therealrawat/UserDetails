using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using userForm.Repository;
using userForm.ViewModel;

namespace userForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepo _userRepo;
        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }


        [HttpGet(nameof(GetUserDetails))]
        public IActionResult GetUserDetails()
        {
            return Ok(_userRepo.GetUsers());
        }


        [HttpGet(nameof(GetUserID))]
        public IActionResult GetUserID(int id)
        {
            return Ok(_userRepo.GetUsersByID(id));
        }


        [HttpPost(nameof(AddUpdateUserDetails))]
        public IActionResult AddUpdateUserDetails(AddUpdateModel info)
        {
            return Ok(_userRepo.AddEditUsers(info));
        }

        [HttpDelete(nameof(DeleteUserDetails))]
        public IActionResult DeleteUserDetails(int id)
        {
            return Ok(_userRepo.DeleteUser(id));
        }
        
        [HttpGet(nameof(GetCountry))]
        public IActionResult GetCountry()
        {
            return Ok(_userRepo.GetCity());
        }
    }
}
