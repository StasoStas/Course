using System;
using System.Collections.Generic;
using System.Text;

namespace less_6
{
    [Serializable]
    class Todo
    {
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public Todo() { }
        public Todo(string title)
        {
            Title = title;
            IsDone = false;
        }
    }
}
