using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace TodoApi.Models
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;
        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems;
        }

        public void Add(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
        }

        public TodoItem Find(string key)
        {
            return _context.TodoItems.Find(key);
        }

        public TodoItem Remove(string key)
        {
            TodoItem item = _context.TodoItems.Find(key);
            _context.TodoItems.Remove(item);
            return item;
        }

        public void Update(TodoItem item)
        {
            _context.TodoItems.Update(item);
            _context.SaveChanges();
        }
    }
}
