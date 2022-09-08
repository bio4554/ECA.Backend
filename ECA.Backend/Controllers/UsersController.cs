using ECA.Backend.Common.Models;
using ECA.Backend.Common.RequestResponse;
using ECA.Backend.Data.Context;
using ECA.Backend.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECA.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly BackendContext _context;
    private readonly IUserService _userService;
    private readonly ILogger _log;

    public UsersController(BackendContext context, IUserService userService, ILoggerFactory loggerFactory)
    {
        _context = context;
        _userService = userService;
        _log = loggerFactory.CreateLogger<UsersController>();
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserAccount>>> GetUsers()
    {
        if (_context.Set<UserAccount>() == null) return NotFound();
        return await _context.Set<UserAccount>().ToListAsync();
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserAccount>> GetUser(long id)
    {
        if (_context.Set<UserAccount>() == null) return NotFound();
        var user = await _context.Set<UserAccount>().FindAsync(id);

        if (user == null) return NotFound();

        return user;
    }

    // PUT: api/Users/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(Guid id, UserAccount userAccount)
    {
        if (id != userAccount.Id) return BadRequest();

        _context.Entry(userAccount).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Users
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<UserAccount>> PostUser(UserAccount userAccount)
    {
        try
        {
            var response = await _userService.CreateUser(userAccount);
            if (response == null)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetUser", new { id = response.Id }, response);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message);
            _log.LogError(e.StackTrace);
            return Problem(statusCode: 500);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserAccount>> PostLogin(LoginRequest req)
    {
        try
        {
            var res = await _userService.Login(req);
            if (res.Authenticated)
            {
                return res.Account != null ? res.Account : Problem(statusCode: 500);
            }
            return BadRequest(res);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message);
            _log.LogError(e.StackTrace);
            return Problem(statusCode: 500);
        }
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        if (_context.Set<UserAccount>() == null) return NotFound();
        var user = await _context.Set<UserAccount>().FindAsync(id);
        if (user == null) return NotFound();

        _context.Set<UserAccount>().Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(Guid id)
    {
        return (_context.Set<UserAccount>()?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}