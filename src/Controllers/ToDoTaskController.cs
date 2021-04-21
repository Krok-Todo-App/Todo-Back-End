using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using taskAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace taskAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private readonly ToDoContext _context;
        public TasksController(ToDoContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //GET api/tasks/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoTask>>> Get()
        {

            var user = await _userManager.GetUserAsync(User);
            var allTasks = await _context.Tasks
                                         .Where(t => t.User == user)
                                         .Include(t => t.User)
                                         .OrderBy(t => t.Id)
                                         .ToListAsync();

            if (allTasks == null || allTasks.Count == 0)
                return NotFound();

            return Ok(allTasks);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<ToDoTask>>> Get(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var task = await _context.Tasks
                                     .Where(t => t.User == user)
                                     .Include(t => t.User)
                                     .FirstAsync(t => t.Id == id);

            if (task == null)
                return NotFound(id);

            return Ok(task);
        }


        [HttpPut]
        public async Task<ActionResult<ToDoTask>> Put([FromBody] ToDoTask task)
        {
            if(task.User == null)
                return BadRequest("User not supplied");

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<ToDoTask>> Post([FromBody] ToDoTask task)
        {
            var user = await _userManager.GetUserAsync(User);
            task.User = user;
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var task = await _context.Tasks
                                     .Where(t => t.User == user)
                                     .FirstAsync(t => t.Id == id);

            if (task == null)
                return NotFound(id);

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //GET api/tasks/test
        [HttpGet("test")]
        public ActionResult Test()
        {
            return Ok("works");
        }
    }
}
