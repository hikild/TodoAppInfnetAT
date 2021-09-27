using Microsoft.Azure.Cosmos;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Database
{
    public class TodoAppDatabase
    {
        private string ConnectionString = "AccountEndpoint=https://todoappinfnet.documents.azure.com:443/;AccountKey=aQSZnuls5fIhFrwvEjScKFFxyAi6dYh0pks6vpAd2tqrdvMBKsXjjx7j2CBZVxyVPGW8i6yLzOm1wDtniDkmuQ==;";
        private string Database = "todoapp";
        private string Container = "todo-add";

        private CosmosClient CosmosClient { get; set; }

        public TodoAppDatabase()
        {
            this.CosmosClient = new CosmosClient(this.ConnectionString);
        }


        public List<AddTask> GetAll()
        {
            var container = this.CosmosClient.GetContainer(Database, Container);

            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c");

            var result = new List<AddTask>();

            var queryResult = container.GetItemQueryIterator<AddTask>(queryDefinition);

            while (queryResult.HasMoreResults)
            {
                FeedResponse<AddTask> currentResultSet = queryResult.ReadNextAsync().Result;
                result.AddRange(currentResultSet.Resource);
            }

            return result;

        }

        public AddTask GetTaskById(Guid id)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);

            QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c where c.id = '{id}'");

            var queryResult = container.GetItemQueryIterator<AddTask>(queryDefinition);

            AddTask task = null;

            while (queryResult.HasMoreResults)
            {
                FeedResponse<AddTask> currentResultSet = queryResult.ReadNextAsync().Result;
                task = currentResultSet.Resource.FirstOrDefault();
            }

            return task;
        }

        public async Task SaveTask(AddTask task)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);

            await container.CreateItemAsync<AddTask>(task, new PartitionKey(task.PartitionKey));
        }

        public async Task DeleteTask(AddTask task)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.DeleteItemAsync<AddTask>(task.Id.ToString(), new PartitionKey(task.PartitionKey));
        }

        public async Task UpdateTask(AddTask task)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.ReplaceItemAsync<AddTask>(task, task.Id.ToString(), new PartitionKey(task.PartitionKey));
        }

    }
}
