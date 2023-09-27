using Microsoft.AspNetCore.Mvc;
using TODOAPP.Models;
using TODOAPP.Repositoies;
using TODOAPP.ViewModels;

namespace TODOAPP.Controllers
{
    public class TODOController : Controller
    {
        private readonly IBaseRepository<TaskTable> taskRepository;
        private readonly IBaseRepository<Status> statusRepository;
        private readonly IBaseRepository<Category> categoryRepository;

        public TODOController(IBaseRepository<TaskTable> taskRepository,
                                IBaseRepository<Status> statusRepository,
                                IBaseRepository<Category> categoryRepository)
        {
            this.taskRepository = taskRepository;
            this.statusRepository = statusRepository;
            this.categoryRepository = categoryRepository;
        }

        public  IActionResult Index()
        {

            try
            {
               var tasks = taskRepository.GetAll();
                foreach (var item in tasks)
                {
                    item.Status =  statusRepository.GetById(item.StatusId);
                    item.Category = categoryRepository.GetById(item.CategoryId);
                }

                return View( taskRepository.GetAll());

            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var category = categoryRepository.GetAll();
            var status = statusRepository.GetAll();
            TaskTableViewModel task = new TaskTableViewModel()
            {
                Status = status.ToList(),
                Category = category.ToList()
            };

            return View(task);
        }

        [HttpPost]
        public IActionResult Create(TaskTableViewModel model)
        {

            try
            {
                if(ModelState.IsValid)
                {
                    var task = new TaskTable()
                    {
                        TaskId = model.TaskId,
                        TaskName = model.TaskName,
                        Priority = model.Priority,
                        CreatedAtdate = DateTime.Now,
                        CategoryId = model.Category_Id,
                        StatusId = model.Status_Id,
                    };
                    taskRepository.Add(task);

                    return RedirectToAction(nameof(Index));
                }
                model.Category = categoryRepository.GetAll().ToList();
                model.Status = statusRepository.GetAll().ToList();
                return View(model);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var task = taskRepository.GetById(id);
                var status = statusRepository.GetById(task.StatusId);
                var category = categoryRepository.GetById(task.CategoryId);
                task.Status = status;
                task.Category = category;

                return View(task);
            }
            catch (Exception e)
            {

                return Content(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id, IFormCollection form)
        {
            try
            {
                taskRepository.Delete(id);
                return RedirectToAction(nameof(Success));
                
            }
            catch (Exception e)
            {

                return Content(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Success()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                return Content("Error");
            }
        }

        [HttpGet]
        public IActionResult SuccessUpdate()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                return Content("Error");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {

                var task = taskRepository.GetById(id);
                TaskTableViewModel model = new TaskTableViewModel()
                {
                    TaskId = task.TaskId,
                    TaskName = task.TaskName,
                    Priority = task.Priority,
                    CreatedAtdate = task.CreatedAtdate,
                    Status = statusRepository.GetAll().ToList(),
                    Category = categoryRepository.GetAll().ToList()
                };
                
                return View(model);
                
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit(TaskTableViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var task = new TaskTable()
                    {
                        TaskId = model.TaskId,
                        TaskName = model.TaskName,
                        Priority = model.Priority,
                        CreatedAtdate = DateTime.Now,
                        StatusId = model.Status_Id,
                        CategoryId = model.Category_Id
                    };

                    taskRepository.Update(task);
                    return RedirectToAction(nameof(Index));
                    
                }
                model.Status = statusRepository.GetAll().ToList();
                model.Category = categoryRepository.GetAll().ToList();
                return View(model);
            }
            catch (Exception e)
            {

                return Content(e.Message);
            }
        }

        public IActionResult UpdateStatus(int id)
        {
            return View(taskRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, IFormCollection keys)
        {
            try
            {
                var task = taskRepository.GetById(id);
                var status = statusRepository.GetById(task.StatusId);
                var category = categoryRepository.GetById(task.CategoryId);
                task.Status = status;
                task.Category = category;

                if (task != null)
                {
                    if (task.Status.StatusName == "Pending")
                    {
                        task.StatusId = 2;
                    }
                    else if (task.Status.StatusName == "Open")
                    {
                        task.StatusId = 3;
                    }

                    taskRepository.Update(task);
                }
                return RedirectToAction(nameof(SuccessUpdate));
            }
            catch (Exception e)
            {

                return Content(e.Message);
            }
        }

    }
}
