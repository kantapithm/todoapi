using System.Collections.Generic;

namespace TodoApi.Models
{
    public interface ITodoRepository
    {
        void Add(TodoItem item);
        IEnumerable<TodoItem> GetAll(string userid);
        TodoItem Find(int key);
        TodoItem Remove(int key);
        void Update(TodoItem item);
    }
}
