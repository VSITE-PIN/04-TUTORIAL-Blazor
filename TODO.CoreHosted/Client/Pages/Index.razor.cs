using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TODO.CoreHosted.Shared;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

[Inject] public HttpClient Http { get; set; }
private IList<TodoItem> tasks;
private string error;

protected override async Task OnInitializedAsync()
{
    try
    {
        string requestUri = "TodoItems"; //ovo je naziv kontrolera
                                         //pozivamo metodu na kontroleru za dohvat todo-ova
        tasks = await Http.GetFromJsonAsync<IList<TodoItem>>(requestUri);
    }
    catch (Exception)
    {
        error = "Error Encountered";
    }
}



namespace TODO.CoreHosted.Client.Pages
{
    public partial class Index
    {
    }
    private async Task CheckboxChecked(TodoItem task)
    {
        task.IsDone = !task.IsDone;
        string requestUri = $"TodoItems/{task.Id}";
        //pozivamo metodu za ažuriranje 
        var response = await Http.PutAsJsonAsync<TodoItem>(requestUri, task);
        if (!response.IsSuccessStatusCode)
        {
            error = response.ReasonPhrase;
        };
        private async Task DeleteTask(TodoItem task)
        {
            tasks.Remove(task);
            string requestUri = $"TodoItems/{task.Id}";
            var response = await Http.DeleteAsync(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                error = response.ReasonPhrase;
            }
        }

    }
}
