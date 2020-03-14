using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Mdoels
{
    public class ToDoEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TaskDate { get; set; }
        public bool isFinished { get; set; }
    }
}
