using System;
using System.Collections.Generic;

namespace TODOAPP.Models
{
    public partial class TaskTable
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; } = null!;
        public int Priority { get; set; }
        public DateTime CreatedAtdate { get; set; }
        public int CategoryId { get; set; }
        public int StatusId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Status? Status { get; set; }
    }
}
