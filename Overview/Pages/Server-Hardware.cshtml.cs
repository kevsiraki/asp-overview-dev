using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Overview.Pages
{
    public class Server_HardwareModel : PageModel
    {
        public List<int> Results { get; private set; } = new();
        public List<string> Logs { get; private set; } = new();

        public async Task OnGetAsync()
        {
            //Results = await ProcessAsyncTasks();
        }

        static async Task<int> DelayAndReturnAsync(int val, List<string> logs)
        {
            logs.Add($"Starting task for value {val}...");
            await Task.Delay(TimeSpan.FromSeconds(val));
            logs.Add($"Completed task for value {val}.");
            return val;
        }

        private async Task<List<int>> ProcessAsyncTasks()
        {
            var results = new List<int>();
            var tasks = new List<Task<int>>
            {
                DelayAndReturnAsync(2, Logs), // Pass Logs property
                DelayAndReturnAsync(3, Logs),
                DelayAndReturnAsync(1, Logs)
            };

            while (tasks.Count > 0)
            {
                var completedTask = await Task.WhenAny(tasks); // Wait for any task to complete
                results.Add(await completedTask); // Add the result of the completed task
                tasks.Remove(completedTask); // Remove the completed task from the list
            }

            return results;
        }

    }
}

/*
static async Task<int> DelayAndReturnAsync(int val)
{
    await Task.Delay(TimeSpan.FromSeconds(val));
    return val;
}

// 2,3,1 -> 1,2,3
static async Task ProcessAsyncTasks()
{
    Task<int> taskA = DelayAndReturnAsync(2);
    Task<int> taskB = DelayAndReturnAsync(3);
    Task<int> taskC = DelayAndReturnAsync(1);
    var tasks = new[] { taskA, taskB, taskC };

    // await order
    foreach (var task in tasks)
    {
        var result = await task;
        //Log/Print
    }
}

//In cshtml: 

<h1>Server Hardware Async Task Results</h1>

<h2>Task Logs</h2>
<ul>
    @foreach (var log in Model.Logs)
    {
        <li>@log</li>
    }
</ul>

<h2>Final Results</h2>
<ul>
    @foreach (var result in Model.Results)
    {
        <li>@result</li>
    }
</ul>
*/