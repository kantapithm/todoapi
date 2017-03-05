using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
      public TodoController(ITodoRepository todoItems)
      {
        _todoRepository = todoItems;
      }
      public ITodoRepository _todoRepository { get; set; }

      [HttpGet]
      public IEnumerable<TodoItem> GetAll()
      {
        return _todoRepository.GetAll();
      }

      [HttpGet("{key}", Name = "GetTodo")]
      public IActionResult GetById(int key)
      {
        var item = _todoRepository.Find(key);
        if (item == null)
        {
          return NotFound();
        }
        return new ObjectResult(item);
      }
      [HttpPost]
      public IActionResult Create([FromBody] TodoItem item)
      {
        if (item == null)
        {
          return BadRequest();
        }
        _todoRepository.Add(item);
        return CreatedAtRoute("GetTodo", new {key = item.Key}, item);
      }

      [HttpPut("{key}")]
      public IActionResult Update(int key, [FromBody] TodoItem item)
      {
        if (item == null || item.Key != key)
        {
          return BadRequest();
        }
        var todo = _todoRepository.Find(key);
        if (todo == null)
        {
          return NotFound();
        }

        todo.IsComplete = item.IsComplete;
        todo.Name = item.Name;
        todo.Owner = item.Owner;
        _todoRepository.Update(todo);
        return new NoContentResult();
      }

      [HttpDelete("{key}")]
      public IActionResult Delete(int key)
      {
        var todo = _todoRepository.Find(key);
        if (todo == null)
        {
          return NotFound();
        }
        _todoRepository.Remove(key);
        return new NoContentResult();
      }
    }
}
