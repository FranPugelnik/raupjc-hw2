using GenericList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace _2.zad
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;
        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryTodoDatabase = initialDbState;
            }
            else
            {
                _inMemoryTodoDatabase = new GenericList<TodoItem>();
            }
}

        public TodoItem Add(TodoItem todoItem)
        {
            if (_inMemoryTodoDatabase.Contains(todoItem))
                throw new DuplicateTodoItemException($" duplicate id: {todoItem.Id }");
            _inMemoryTodoDatabase.Add(todoItem);
            return todoItem;
        }

        public TodoItem Get(Guid todoId)
        {

            if (_inMemoryTodoDatabase.Count() == 0) return null;
            TodoItem temp = _inMemoryTodoDatabase.FirstOrDefault(i =>
            {
                if (i == null)
                    return false;
                return i.Id == todoId;
            });
            return temp;


        }

        public List<TodoItem> GetActive()
        {
            List<TodoItem> L;
            L = _inMemoryTodoDatabase.Where(x => x.IsCompleted.Equals(false)).ToList();
            return L;
        }

        public List<TodoItem> GetAll()
        {
            return _inMemoryTodoDatabase.OrderByDescending(x => x.DateCreated).ToList();
        }

        public List<TodoItem> GetCompleted()
        {
            return _inMemoryTodoDatabase.Where(x => x.IsCompleted.Equals(true)).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            return _inMemoryTodoDatabase.Where(filterFunction).ToList();
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            if (_inMemoryTodoDatabase.Any(x => x.Id.Equals(todoId)))
            {
                return _inMemoryTodoDatabase.First(x => x.Id.Equals(todoId)).MarkAsCompleted();
            }
            return false;
        }

        public bool Remove(Guid todoId)
        {
            if (_inMemoryTodoDatabase.Count() == 0)
                return false;
            TodoItem temp = _inMemoryTodoDatabase.FirstOrDefault(x =>
            {
                if (x == null) return false;
                return x.Id == todoId;
            });
            if (temp == null) return false;
            _inMemoryTodoDatabase.Remove(temp);
            return true;
        }

        public TodoItem Update(TodoItem todoItem)
        {
            if (_inMemoryTodoDatabase.Any(x => x.Id.Equals(todoItem)))
            {
                _inMemoryTodoDatabase.FirstOrDefault(x => x.Equals(todoItem)).DateCompleted = todoItem.DateCompleted; //mozda moze
                 _inMemoryTodoDatabase.FirstOrDefault(x => x.Equals(todoItem)).DateCreated = todoItem.DateCreated;    //_inMemoryTodoDatabase.FirstOrDefault(x => x.Equals(todoItem))= todoItem
                _inMemoryTodoDatabase.FirstOrDefault(x => x.Equals(todoItem)).Text = todoItem.Text;                   //
            }
            else
                _inMemoryTodoDatabase.Add(todoItem);
            return todoItem;
        }
        
    }

    [Serializable]
    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException()
        {
        }

        public DuplicateTodoItemException(string message) : base(message)
        {
        }

        public DuplicateTodoItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateTodoItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
