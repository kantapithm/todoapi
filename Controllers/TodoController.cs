using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    public class MyUser
    {
      public string sessionID {get; set;}
      public string userID {get; set;}
    }

    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        public TodoController(ITodoRepository todoItems)
        {
            _todoRepository = todoItems;
        }
        public ITodoRepository _todoRepository { get; set; }

        private static HttpClient client = new HttpClient();

        protected string GetUserIdFromSession(string sessionid)
        {
            //FIXME
            HttpResponseMessage response = client.GetAsync("http://localhost:5000/api/session/" + sessionid).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                MyUser user = JsonConvert.DeserializeObject<MyUser>(result);
                return user.userID;
            }
            else
            {
                return null;
            }
        }


        [HttpGet]
        //public IEnumerable<TodoItem> GetAll(int sessionid)
        public IActionResult GetAll(string sessionid)
        {
            string userid;
            if ((userid = GetUserIdFromSession(sessionid)) == null)
            {
                return BadRequest();
            }
            return new ObjectResult(_todoRepository.GetAll(userid));
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
                Console.WriteLine("item is null");
                return BadRequest();
            }
                Console.WriteLine("add item");
            _todoRepository.Add(item);
            return CreatedAtRoute("GetTodo", new { key = item.Key }, item);
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
