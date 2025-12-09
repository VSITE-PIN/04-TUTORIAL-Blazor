using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TODO.CoreHosted.Shared;

namespace TODO.CoreHosted.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] public HttpClient Http { get; set; } = default!;

        private IList<TodoItem>? tasks;
        private string? error;
        private string? newTodo;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                // API endpoint je /TodoItems
                tasks = await Http.GetFromJsonAsync<IList<TodoItem>>("TodoItems")
                         ?? new List<TodoItem>(); // čak i ako vrati null, imat ćemo praznu listu
            }
            catch (Exception ex)
            {
                error = ex.Message;
                tasks = new List<TodoItem>();
            }
        }

        private async Task CheckboxChecked(TodoItem task)
        {
            if (task == null)
                return;

            task.IsDone = !task.IsDone;

            try
            {
                var response = await Http.PutAsJsonAsync($"TodoItems/{task.Id}", task);
                if (!response.IsSuccessStatusCode)
                {
                    error = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
        }

        private async Task DeleteTask(TodoItem task)
        {
            if (tasks == null || task == null)
                return;

            tasks.Remove(task);

            try
            {
                var response = await Http.DeleteAsync($"TodoItems/{task.Id}");

                if (!response.IsSuccessStatusCode)
                {
                    error = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
        }

        private async Task AddTask()
        {
            if (string.IsNullOrWhiteSpace(newTodo))
                return;

            var newTaskItem = new TodoItem
            {
                Title = newTodo,
                IsDone = false
            };

            tasks ??= new List<TodoItem>();
            tasks.Add(newTaskItem);

            try
            {
                var response = await Http.PostAsJsonAsync("TodoItems", newTaskItem);

                if (response.IsSuccessStatusCode)
                {
                    newTodo = string.Empty;
                }
                else
                {
                    error = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
        }
    }
}
