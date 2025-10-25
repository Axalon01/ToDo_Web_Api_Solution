using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo_Web_Api.Models;
using ToDo_Web_Api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDo_Web_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TasksController : ControllerBase
	{
		private readonly TaskService _taskService;
		public TasksController(TaskService taskService)
		{
			_taskService = taskService;
		}

		// GET: api/<TasksController>
		[HttpGet]
		public IEnumerable<TaskModel> Get()
		{
			return _taskService.LoadTasks();
		}

		// GET api/<TasksController>/5
		[HttpGet("{id}")]
		public IActionResult Get(Guid id)
		{
			var tasks = _taskService.LoadTasks().FirstOrDefault(t => t.Id == id);

			if (tasks == null)
			{
				return NotFound();
			}

			return Ok(tasks);
		}

		// POST api/<TasksController>
		[HttpPost]
		public ActionResult<TaskModel> Post([FromBody] TaskModel value)
		{
			var createdTask = _taskService.AddTask(value);
			return CreatedAtAction(nameof(Get), new { id = createdTask.Id }, createdTask);
		}

		// PUT api/<TasksController>/5
		[HttpPut("{id}")]
		public IActionResult Put(Guid id, [FromBody] TaskModel value)
		{
			if (id != value.Id)
			{
				return BadRequest("ID in URL does not match ID in task body.");
			}

			var success = _taskService.UpdateTask(value);

			if (!success)
			{
				return NotFound();
			}

			return NoContent(); // 204 - Successfully updated, but nothing to return
		}

		// DELETE api/<TasksController>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id)
		{
			var success = _taskService.DeleteTask(id);

			if (!success)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
