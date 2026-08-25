using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
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

        return Ok (_context.Users);
    }

    [HttpGet("{id}")]

    public IActionResult GetUserById(User user, int id)
    {
        var FindUser = _context.Users.FirstOrDefault(ExpectedId => id == user.UserId);

        return Ok(FindUser);
    }

        [HttpPost]

    public IActionResult CreateUser ([FromBody] User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        AutoIncrement = AutoIncrement + user.UserId;

        return CreatedAtAction(nameof(GetUserById), new {id = user.UserId}, user);
    }


    // [HttpPost]

    // public IActionResult CreateUser ([FromBody] User user)
    // {
    //     _context.Users.Add(user);
    //     _context.SaveChanges();

    //     return CreatedAtAction(nameof(GetUserById), new { Id = user.UserId }, user);
    // }
    
}