using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JwtDatabase;
using JwtModels;
using Microsoft.AspNetCore.Authorization;

namespace JwtWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {
        private DatabaseContext Context { get; }

        public TodosController(DatabaseContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodo()
        {
            return await Context.Todo.ToListAsync();
        }
      
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await Context.Todo.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            Context.Entry(todo).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
             
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            Context.Todo.Add(todo);
            await Context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }
              
        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> DeleteTodo(int id)
        {
            var todo = await Context.Todo.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            Context.Todo.Remove(todo);
            await Context.SaveChangesAsync();

            return todo;
        }

        private bool TodoExists(int id)
        {
            return Context.Todo.Any(e => e.Id == id);
        }
    }
}
