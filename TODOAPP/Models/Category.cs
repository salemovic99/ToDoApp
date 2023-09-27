using System;
using System.Collections.Generic;

namespace TODOAPP.Models
{
    public partial class Category
    {
        public Category()
        {
            TaskTables = new HashSet<TaskTable>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<TaskTable> TaskTables { get; set; }
    }
}
