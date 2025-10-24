using ToDo_Web_Api.Models;

namespace ToDo_Web_Api.Services
{
	public class TaskService
	{

		private static List<TaskModel> _tasks = new List<TaskModel>();

		public bool SaveTasks(List<TaskModel> tasks)
		{
			_tasks = tasks;
			return true;
		}

		public List<TaskModel> LoadTasks()
		{
			return _tasks;
		}

		public bool UpdateTask(TaskModel updatedTask)
		{
			var tasks = LoadTasks();
			var taskToUpdate = tasks.FirstOrDefault(t => t.Id == updatedTask.Id);

			if (taskToUpdate != null)
			{
				taskToUpdate.Title = updatedTask.Title;
				taskToUpdate.Description = updatedTask.Description;
				taskToUpdate.IsComplete = updatedTask.IsComplete;
				taskToUpdate.DueDate = updatedTask.DueDate;

				return SaveTasks(tasks);
			}
			return false; // Not found
		}

		public bool DeleteTask(Guid id)
		{
			var tasks = LoadTasks();
			var taskToDelete = tasks.FirstOrDefault(t => t.Id == id);

			if (taskToDelete != null)
			{
				tasks.Remove(taskToDelete);
				return SaveTasks(tasks);
			}

			return false; // Nothing found to delete
		}

		public TaskModel AddTask(TaskModel newTask)
		{
			var tasks = LoadTasks();

			// Auto-generates field if they're missing
			if (newTask.Id == Guid.Empty)
				newTask.Id = Guid.NewGuid();

			if (newTask.CreatedAt == default)
				newTask.CreatedAt = DateTime.Now;

			tasks.Add(newTask);
			SaveTasks(tasks);
			return newTask; // Useful if the caller wants the generated Id
		}
	}
}
