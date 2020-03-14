using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Mdoels;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoEntitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ToDoEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoEntity>>> GetToDos()
        {
            return await _context.ToDos.Where(t => !t.isFinished).ToListAsync();
        }

        // GET: api/ToDoEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoEntity>> GetToDoEntity(int id)
        {
            var toDoEntity = await _context.ToDos.FindAsync(id);

            if (toDoEntity == null)
            {
                return NotFound();
            }

            return toDoEntity;
        }

        // PUT: api/ToDoEntities/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoEntity(int id, ToDoEntity toDoEntity)
        {
            if (id != toDoEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDoEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoEntityExists(id))
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

        // POST: api/ToDoEntities
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ToDoEntity>> PostToDoEntity(ToDoEntity toDoEntity)
        {
            _context.ToDos.Add(toDoEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoEntity", new { id = toDoEntity.Id }, toDoEntity);
        }

        // DELETE: api/ToDoEntities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoEntity>> DeleteToDoEntity(int id)
        {
            var toDoEntity = await _context.ToDos.FindAsync(id);
            if (toDoEntity == null)
            {
                return NotFound();
            }

            _context.ToDos.Remove(toDoEntity);
            await _context.SaveChangesAsync();

            return toDoEntity;
        }

        private bool ToDoEntityExists(int id)
        {
            return _context.ToDos.Any(e => e.Id == id);
        }
    }
}
