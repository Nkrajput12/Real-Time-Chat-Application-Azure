using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ConnectHub.Authentication.Service.Interface;
using ConnectHub.Authentication.Models.DTOs;

namespace ConnectHub.Auth.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserByAdminAsync(id);
            return success ? Ok(new { message = "User deleted successfully." }) : NotFound();
        }

        [HttpPut("users/{id}/suspend")]
        public async Task<IActionResult> SuspendUser(int id)
        {
            var success = await _userService.ToggleUserActiveStatusAsync(id);
            return success ? Ok(new { message = "User status toggled successfully.", userId = id }) : NotFound();
        }

        [HttpGet("analytics")]
        public async Task<IActionResult> GetAnalytics()
        {
            var users = await _userService.GetAllUsersAsync();
            var activeUsers = users.Count(u => u.IsActive);
            var onlineUsers = users.Count(u => u.IsOnline);
            
            return Ok(new {
                totalUsers = users.Count(),
                activeUsers = activeUsers,
                onlineUsers = onlineUsers,
                deactivatedUsers = users.Count() - activeUsers,
                liveConnections = onlineUsers
            });
        }
    }
}