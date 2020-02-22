using JwtDatabase;
using JwtModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtWebApi.Controllers
{
   [Route("api/todos")]
   [ApiController]
   [Authorize]
   public class TodosController : ControllerBase
   {
      private DatabaseContext Context { get; }

      public TodosController(DatabaseContext context)
      {
         Context = context;
      }

      /// <summary>
      /// List of Todos
      /// </summary>
      /// <returns></returns>
      [HttpGet()]
      public async Task<ActionResult<IEnumerable<Todo>>> GetTodo()
      {
         return await Context.Todo.AsNoTracking().ToListAsync();
      }

      /// <summary>
      /// List of Todos
      /// </summary>
      /// <returns></returns>
      [HttpGet("filter")]
      public async Task<ActionResult<IEnumerable<Todo>>> GetTodoKeyword([FromQuery] string keyword)
      {
         return await Context.Todo.AsNoTracking()
            .Where(x => x.Title.Contains(keyword))
            .ToListAsync();
      }

      /// <summary>
      /// Get Todo For Id
      /// </summary>
      /// <param name="id"></param>
      /// <returns>Todo</returns>
      [HttpGet("{id}")]
      public async Task<ActionResult<Todo>> GetTodo(int id)
      {
         var todo = await Context
            .Todo
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

         if (todo == null)
         {
            return NotFound();
         }

         return todo;
      }

      /// <summary>
      /// List of Todos Done (true/false)
      /// </summary>
      /// <param name="done">bool (true or false)</param>
      /// <returns>IEnumerable<Todo></returns>   
      [HttpGet("{done}/done")]
      public async Task<ActionResult<IEnumerable<Todo>>> GetTodoStatus(bool done = false)
      {
         return await Context
            .Todo
            .AsNoTracking()
            .Where(x => x.Done == done)
            .ToListAsync();
      }

      /// <summary>
      /// Update Todo
      /// </summary>
      /// <param name="id"></param>
      /// <param name="todo"></param>
      /// <returns></returns>
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


      /// <summary>
      /// Add Todo
      /// </summary>
      /// <param name="todo"></param>
      /// <returns></returns>
      [HttpPost]
      public async Task<ActionResult<Todo>> PostTodo(Todo todo)
      {
         Context.Todo.Add(todo);
         await Context.SaveChangesAsync();

         return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
      }


      /// <summary>
      /// Delete Todo
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
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
