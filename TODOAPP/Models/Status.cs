using System;
using System.Collections.Generic;

namespace TODOAPP.Models
{
    public partial class Status
    {
        public Status()
        {
            TaskTables = new HashSet<TaskTable>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; } = null!;

        public virtual ICollection<TaskTable> TaskTables { get; set; }
    }
}
