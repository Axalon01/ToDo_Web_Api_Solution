namespace ToDo_Web_Api.Models
{
	public class TaskModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public bool IsComplete { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
