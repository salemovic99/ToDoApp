using System.ComponentModel.DataAnnotations;
using TODOAPP.Models;

namespace TODOAPP.ViewModels
{
    public class TaskTableViewModel
    {

        public int TaskId { get; set; }

        [Required(ErrorMessage ="Task Name Can not be empty!")]
        public string TaskName { get; set; } = null!;

        [Required(ErrorMessage = "Priority Can not be empty!")]
        public int Priority { get; set; }

        public DateTime CreatedAtdate { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category Can not Be Empty!")]
        public int Category_Id { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status Can not Be Empty!")]
        public int Status_Id { get; set; }

        public  List<Category>? Category { get; set; }
        public  List<Status>? Status { get; set; }
    }
}
