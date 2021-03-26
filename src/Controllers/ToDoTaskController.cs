using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using taskAPI.Model;
using Microsoft.EntityFrameworkCore;
using taskAPI.Helpers;

namespace taskAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoTaskController : ControllerBase
    {
        private readonly ToDoContext _context;
        public ToDoTaskController(ToDoContext context)
        {
            _context = context;
        }

        //GET api/todotask/get
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<ToDoTask>>> Get()
        {

            var allTasks = await _context.Tasks.ToListAsync();

            if (allTasks == null || allTasks.Count == 0)
                return NotFound();

            allTasks = StatusHelper.ValidateStatus(allTasks);

            return Ok(allTasks);
        }

        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<IEnumerable<ToDoTask>>> Get(int id)
        {

            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound(id);

            task = StatusHelper.ValidateStatus(task);

            return Ok(task);
        }

        [HttpPost("post")]
        public async Task<ActionResult<ToDoTask>> Post([FromBody] ToDoTask task)
        {
            if (ModelState.IsValid)
            {
                task.Status = "active";
                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
                return Ok(task);
            }
            return ValidationProblem();
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromBody] ToDoTask task)
        {
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return ValidationProblem();
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound(id);

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //GET api/todotask/test
        [HttpGet("test")]
        public ActionResult Test()
        {
            return Ok("shit works");
        }
    }
}
