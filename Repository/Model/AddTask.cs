using Newtonsoft.Json;
using System;

namespace Repository.Model
{
    public class AddTask
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "creator")]
        public string Creator { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "status")]
        public TaskProgress Status { get; set; }
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "pk")]
        public string PartitionKey { get; set; } = "todoapp";
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public enum TaskProgress
        {
            Backlog = 1,
            InProgress = 2,
            Completed = 3
        }
    }
}
