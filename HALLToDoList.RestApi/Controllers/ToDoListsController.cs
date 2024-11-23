using HALLToDoList.Database.Models;
using HALLToDoList.RestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HALLToDoList.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetLists()
        {
            var lst = _db.TblLists.AsNoTracking().ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetList(int id)
        {
            var item = _db.TblLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            return Ok(item is null ? $"No data found by provided id {id}" : item);
        }
        [HttpGet("complete")]
        public IActionResult GetCompletedLists()
        {
            var lst = _db.TblLists.AsNoTracking().Where(x => x.Status == "complete").ToList();
            return Ok(lst);
        }
        [HttpGet("idle")]
        public IActionResult GetIdleLists()
        {
            var lst = _db.TblLists.AsNoTracking().Where(x => x.Status == "idle").ToList();
            return Ok(lst);
        }
        [HttpGet("pending")]
        public IActionResult GetPendingLists()
        {
            var lst = _db.TblLists.AsNoTracking().Where(x => x.Status == "pending").ToList();
            return Ok(lst);
        }
        [HttpGet("due")]
        public IActionResult GetDueLists()
        {
            var lst = _db.TblLists.AsNoTracking().Where(x => x.Status == "due").ToList();
            return Ok(lst);
        }
        [HttpPost]
        public IActionResult PostList(ToDoListViewModel model)
        {
            if (!String.IsNullOrEmpty(model.Title)) return BadRequest("title field cannot be empty string.");
            TblList list = new TblList { TaskTitle = model.Title, Status = "idle" };
            _db.TblLists.Add(list);
            int result = _db.SaveChanges();
            if (result > 0)
                return Ok("Successfully Created");
            else
                return StatusCode(500, "Creation Failed.");
        }
        [HttpPut("{id}")]
        public IActionResult PutTitle(int id, ToDoListViewModel model)
        {
            var item = _db.TblLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                return NotFound($"No data is found by provided id {id}");
            }
            if (!String.IsNullOrEmpty(model.Title)) return BadRequest("title field cannot be empty string.");
            item.TaskTitle = model.Title;
            _db.Entry(item).State = EntityState.Modified;
            int result = _db.SaveChanges();
            if (result > 0)
                return Ok("Successfully Updated the title.");
            else
                return StatusCode(500, "Updated Failed.");
        }
        [HttpPatch("setToIdel/{id}")]
        public IActionResult PatchListToIdle(int id)
        {
            var item = _db.TblLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                return NotFound($"No data is found by provided id {id}");
            }
            item.Status = "idle";
            _db.Entry(item).State = EntityState.Modified;
            int result = _db.SaveChanges();
            if (result > 0)
                return Ok("Successfully set the list to 'complete'");
            else
                return StatusCode(500, $"Set the list to 'idle' failed id = {id}");
        }
        [HttpPatch("setToPending/{id}")]
        public IActionResult PatchListToPending(int id)
        {
            var item = _db.TblLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                return NotFound($"No data is found by provided id {id}");
            }
            item.Status = "pending";
            _db.Entry(item).State = EntityState.Modified;
            int result = _db.SaveChanges();
            if (result > 0)
                return Ok("Successfully set the list to 'pending'");
            else
                return StatusCode(500, $"Set the list to 'pending' failed id = {id}");
        }
        [HttpPatch("setToComplete/{id}")]
        public IActionResult PatchListToComplete(int id)
        {
            var item = _db.TblLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                return NotFound($"No data is found by provided id {id}");
            }
            item.Status = "complete";
            _db.Entry(item).State = EntityState.Modified;
            int result = _db.SaveChanges();
            if (result > 0)
                return Ok("Successfully set the list to 'complete'");
            else
                return StatusCode(500, $"Set the list to 'complete' failed id = {id}");
        }
        [HttpPatch("setToDue/{id}")]
        public IActionResult PatchListToDue(int id)
        {
            var item = _db.TblLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                return NotFound($"No data is found by provided id {id}");
            }
            item.Status = "due";
            _db.Entry(item).State = EntityState.Modified;
            int result = _db.SaveChanges();
            if (result > 0)
                return Ok("Successfully set the list to 'due'");
            else
                return StatusCode(500, $"Set the list to 'due' failed id = {id}");
        }
    }
}
