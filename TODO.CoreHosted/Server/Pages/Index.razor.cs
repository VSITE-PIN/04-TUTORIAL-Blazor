using Microsoft.AspNetCore.Components;
using TODO.CoreHosted.Shared;

namespace TODO.CoreHosted.Server.Pages
{
	public partial class Index
	{
		[Inject] public HttpClient Http { get; set; }

		private IList<TodoItem> tasks;
		private string error;

		protected override async Task OnInitializedAsync()
		{
			try
			{
				string requestUri = "TodoItems"; //ovo je naziv kontrolera//pozivamo metodu na kontroleru za dohvat todo-ova
				tasks = await Http.GetFromJsonAsync<IList<TodoItem>>(requestUri);
			}
			catch (Exception)
			{
				error = "Error Encountered";
			}
		}
	}
}
