using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODO.CoreHosted.Shared;
using TODO.CoreHosted.Server.Data;

namespace TODO.CoreHosted.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly DataContext _context;

        public ToDoItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetToDoItem()
        {
          if (_context.TodoItem == null)
          {
              return NotFound();
          }
            return await _context.TodoItem.ToListAsync();
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetToDoItem(int id)
        {
          if (_context.TodoItem == null)
          {
              return NotFound();
          }
            var toDoItem = await _context.TodoItem.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return toDoItem;
        }

        // PUT: api/ToDoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem(int id, TodoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemExists(id))
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

        // POST: api/ToDoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostToDoItem(TodoItem todoItem)
        {
          if (_context.TodoItem == null)
          {
              return Problem("Entity set 'TODOCoreHostedServerContext.ToDoItem'  is null.");
          }
            _context.TodoItem.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoItem", new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/ToDoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            if (_context.TodoItem == null)
            {
                return NotFound();
            }
            var toDoItem = await _context.TodoItem.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            _context.TodoItem.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoItemExists(int id)
        {
            return (_context.TodoItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
