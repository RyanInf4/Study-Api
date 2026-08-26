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

    public IActionResult GetUsers()
    {
        _context.Users.ToList();
        List<UserDTO> ConvertedUsers = new List<UserDTO>();

        foreach (var allusers in _context.Users)
        {
            ConvertedUsers.Add(Utilities.ConvertUserDto(allusers));
        }

        

        return Ok (ConvertedUsers);
    }

    [HttpGet("{id}")]

    public IActionResult GetUserById(int id)
    {
        var FindUser = _context.Users.FirstOrDefault(ExpectedId => ExpectedId.UserId == id);

        if (FindUser == null)
        {
            return NotFound();
        }

        return Ok( Utilities.ConvertUserDto(FindUser));
    }

        [HttpPost]

    public IActionResult CreateUser ([FromBody] User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        AutoIncrement = AutoIncrement + user.UserId;

        var userDTO = Utilities.ConvertUserDto(user);

        return CreatedAtAction(nameof(GetUserById), new {id = user.UserId}, userDTO);
    }

    [HttpPut("{id}")]

    public IActionResult UpdateUser (int id,[FromBody] User user)
    {
        var findUserinfo = _context.Users.FirstOrDefault(Auser=> Auser.UserId == id);

        findUserinfo.Username = user.Username;
        findUserinfo.Email = user.Email;
        findUserinfo.Password = user.Password;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]

    public IActionResult DeleteUser (int id)
    {
        var user = _context.Users.Find(id);

        if (user == null)
        {
           return NotFound(); 
        }

        _context.Users.Remove(user);
        _context.SaveChanges();

        return NoContent();
        
    }
    
}