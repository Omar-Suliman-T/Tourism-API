using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourist.APPLICATION.DTO.User;
using Tourist.APPLICATION.Interface;
using Tourist.PERSISTENCE.Repository;

namespace Tourist.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        //public UsersController(IUser  userRepository)
        //{
        //    _userRepository = userRepository;
        //}


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _unitOfWork.User.GetAllAsync(null);
            var userDTOs = users.Select(u => new UserDTOs
            {
                Id = u.Id,
                //FirstName = u.FirstName,
                //LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).ToList();

            return Ok(userDTOs);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _unitOfWork.User.GetAsync(u => u.Id == id);
            if (user == null)
                return NotFound();
            var userDTOs =  new UserDTOs
            {
                Id = user.Id,
                //FirstName = user.FirstName,
                //LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(userDTOs);

        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _unitOfWork.User.GetByEmailAsync(email);
            if (user == null)
                return NotFound("User not found");

            var userDTOs = new UserDTOs
            {
                Id = user.Id,
                //FirstName = user.FirstName,
                //LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(userDTOs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDTOs dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _unitOfWork.User.GetAsync(u => u.Id == id);
                if (user == null) return NotFound("User not found.");

                //user.FirstName = dto.FirstName;
                //user.LastName = dto.LastName;
                user.Email = dto.Email;
                user.PhoneNumber = dto.PhoneNumber;

                await _unitOfWork.User.UpdateAsync(user);

                var userDTO = new UserDTOs
                {
                    Id = user.Id,
                    //FirstName = user.FirstName,
                    //LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };

                return Ok(userDTO);
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }


        // ================================
        // DELETE / DEACTIVATE USER
        // ================================
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteUser(string id)
        {
            var result = await _unitOfWork.User.SoftDeleteAsync(id);


            if (!result)
                return NotFound();


            return Ok(new { Message = "User deactivated successfully" });
        }
    }
}
