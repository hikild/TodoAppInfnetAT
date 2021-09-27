using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppFront.Models;

namespace TodoAppFront.Infra
{
    public class TodoRestClient
    {
        private string URL_TODO_REST = "https://todoappinfnet.azurewebsites.net/api/";
        public IList<TodoModel> GetAll()
        {
            var client = new RestClient(URL_TODO_REST);

            var request = new RestRequest("GetAll", DataFormat.Json);

            var response = client.Get<IList<TodoModel>>(request);

            return response.Data;
        }
        public TodoModel GetTaskById(Guid id)
        {
            var client = new RestClient(URL_TODO_REST);

            var request = new RestRequest($"GetTaskById?id={id}", DataFormat.Json);      

            var response = client.Get<TodoModel>(request);

            return response.Data;

        }

        public void SaveTask(TodoModel model)
        {
            var client = new RestClient(URL_TODO_REST);
            var request = new RestRequest($"SaveTask", DataFormat.Json);
            request.AddJsonBody(model);

            var response = client.Post<TodoModel>(request);
        }

        public void DeleteTask(Guid id)
        {
            var client = new RestClient(URL_TODO_REST);

            var request = new RestRequest($"DeleteTask?id={id}", DataFormat.Json);          

            var response = client.Delete(request);

        }

        public void UpdateTask(TodoModel model)
        {
            var client = new RestClient(URL_TODO_REST);

            var request = new RestRequest($"UpdateTask", DataFormat.Json);
            request.AddJsonBody(model);

            var response = client.Put<TodoModel>(request);
        }
    }
}
