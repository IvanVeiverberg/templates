using Microsoft.AspNetCore.Mvc;
using Templates.Api.DTOs;
using Templates.Api.Entities;
using Templates.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers() => Ok(await _userService.GetUsersAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return user == null ? NotFound("User not found.") : Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] UserDto dto)
    {
        try
        {
            var user = await _userService.CreateUserAsync(dto);
            if (user == null) return BadRequest();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto dto)
    {
        if (dto.Id.HasValue && dto.Id.Value != id) return BadRequest();

        var updated = await _userService.UpdateUserAsync(id, dto);
        return updated ? NoContent() : NotFound("User not found.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await _userService.DeleteUserAsync(id);
        return deleted ? NoContent() : NotFound("User not found.");
    }
}