using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using TODO.CoreHosted.Shared;

namespace TODO.CoreHosted.Client.Pages
{
    public partial class Index
    {
        [Inject] public HttpClient Http { get; set; }

        private IList<ToDoItem> tasks;
        private string error;
//        private string newTodo;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                string requestUri = "TodoItems"; // Ovo je naziv API kontrolera
                tasks = await Http.GetFromJsonAsync<IList<ToDoItem>>(requestUri);
            }
            catch (Exception)
            {
                error = "Error encountered while fetching data.";
            }
        }

        private async Task AddTask()
        {
            if (!string.IsNullOrWhiteSpace(newTodo))
            {
                ToDoItem newTaskItem = new ToDoItem
                {
                    Title = newTodo,
                    IsDone = false
                };
                tasks.Add(newTaskItem);
                string requestUri = "TodoItems";
                var response = await Http.PostAsJsonAsync(requestUri, newTaskItem);
                if (response.IsSuccessStatusCode)
                {
                    newTodo = string.Empty;
                }
                else
                {
                    error = response.ReasonPhrase;
                }
            }
        }

        private async Task DeleteTask(ToDoItem task)
        {
            tasks.Remove(task);
            string requestUri = $"TodoItems/{task.Id}";
            var response = await Http.DeleteAsync(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                error = response.ReasonPhrase;
            }
        }

        private async Task CheckboxChecked(ToDoItem task)
        {
            task.IsDone = !task.IsDone;
            string requestUri = $"TodoItems/{task.Id}";
            var response = await Http.PutAsJsonAsync(requestUri, task);
            if (!response.IsSuccessStatusCode)
            {
                error = response.ReasonPhrase;
            }
        }
    }
}
