using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

[ApiController]
[Route("api/[controller]")]

public class StudyController : ControllerBase
{
    private static int AutoIncrement = 1;
    private readonly StudyDbContext _context;

    public StudyController (StudyDbContext context)
    {
        _context = context;
    }

    [HttpGet]

    public async Task<IActionResult> GetUsers()
    {
        await _context.Users.ToListAsync();
        List<Task<UserDTO>> ConvertedUsers = new List<Task<UserDTO>>();

        await foreach (var allusers in _context.Users)
        {
            ConvertedUsers.Add(Utilities.ConvertUserDto(allusers));
        }

        

        return Ok (ConvertedUsers);
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetUserById(int id)
    {
        var FindUser = await _context.Users.FindAsync(id);

        if (FindUser == null)
        {
            return NotFound();
        }

        return Ok(Utilities.ConvertUserDto(FindUser));
    }

        [HttpPost]

    public async Task<IActionResult> CreateUser ([FromBody] User user)
    {
        if (user.Username == null || user.Email == null || user.Password == null )
        {
            return BadRequest();
        }

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        AutoIncrement = AutoIncrement + user.UserId;

        var userDTO = Utilities.ConvertUserDto(user);

        return CreatedAtAction(nameof(GetUserById), new {id = user.UserId}, userDTO);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateUser (int id,[FromBody] User user)
    {
        var findUserinfo = await _context.Users.FindAsync(id);

        if (findUserinfo == null)
        {
            return NotFound();
        }

        if (findUserinfo.Username == null || findUserinfo.Email == null || findUserinfo.Password == null)
        {
            return BadRequest();
        }

        findUserinfo.Username = user.Username;
        findUserinfo.Email = user.Email;
        findUserinfo.Password = user.Password;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteUser (int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
           return NotFound(); 
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
        
    }
    
}