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

        public IEnumerable<TodoItem> GetAll(string userid)
        {
            List<TodoItem> items = new List<TodoItem>();
            foreach(TodoItem item in _context.TodoItems) {
                if(item.Owner.ToString() == userid) {
                    items.Add(item);
                }
            }
            return items;
        }

        public void Add(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
        }

        public TodoItem Find(int key)
        {
            return _context.TodoItems.Find(key);
        }

        public TodoItem Remove(int key)
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
