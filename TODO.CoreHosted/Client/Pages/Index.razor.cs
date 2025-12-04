using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TODO.CoreHosted.Shared;

namespace TODO.CoreHosted.Client.Pages
{
    public partial class Index
    {
        [Inject] public HttpClient Http { get; set; }
        private IList<ToDoItem> tasks;
        private string error;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                string requestUri = "ToDoItems"; //ovo je naziv kontrolera 
                                                 //pozivamo metodu na kontroleru za dohvat todo-ova 
                tasks = await Http.GetFromJsonAsync<IList<ToDoItem>>(requestUri);
            }
            catch (Exception)
            {
                error = "Error Encountered";
            }
        }

        private async Task CheckboxChecked(ToDoItem task)
        {
            task.IsDone = !task.IsDone;
            string requestUri = $"ToDoItems/{task.Id}";
            //pozivamo metodu za ažuriranje  
            var response = await Http.PutAsJsonAsync<ToDoItem>(requestUri, task);
            if (!response.IsSuccessStatusCode)
            {
                error = response.ReasonPhrase;
            }
            ;
        }

        private async Task DeleteTask(ToDoItem task)
        {
            tasks.Remove(task);
            string requestUri = $"ToDoItems/{task.Id}";
            var response = await Http.DeleteAsync(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                error = response.ReasonPhrase;
            }
        }


        private string newTodo
        {  get; set; }

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
                string requestUri = "ToDoItems";
                var response = await Http.PostAsJsonAsync(requestUri,
                newTaskItem);
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

    }
}
