using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> _todos = new List<TodoItem>();
        private static int _nextId = 1;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_todos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _todos.FirstOrDefault(x => x.Id == id);
            return Ok();
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem newItem)
        {
            if (newItem == null || string.IsNullOrEmpty(newItem.Title))
                return BadRequest();

            newItem.Id = _nextId++;
            _todos.Add(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TodoItem updatedItem)
        {
            var existing = _todos.FirstOrDefault(x => x.Id == id);
            if (existing == null)
                return NotFound();

            existing.Title = updatedItem.Title;
            existing.IsComplete = updatedItem.IsComplete;

            return NoContent();

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _todos.FirstOrDefault(x => x.Id == id);
            if (existing == null)
                return NotFound();
            _todos.Remove(existing);
            return NoContent();
        }

    }
}
