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
      public IActionResult GetById(string key)
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
        return CreatedAtRoute("GetTodo", new {id = item.Key}, item);
      }

      [HttpPut("{key}")]
      public IActionResult Update(string key, [FromBody] TodoItem item)
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
      public IActionResult Delete(string key)
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
