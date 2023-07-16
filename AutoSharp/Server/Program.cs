using Microsoft.AspNetCore.ResponseCompression;

#if !DEBUG
AppDomain.CurrentDomain.ProcessExit += Bootstrap.OnProcessExit;
#endif

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");


CancellationTokenSource tokenSource = new CancellationTokenSource();
var task = new Task(() =>
{
    while (!tokenSource.IsCancellationRequested)
    {
        string? a = Console.ReadLine();
        if (a == "q")
        {
            tokenSource.Cancel();
        }
        else
        {
            Console.WriteLine(a);
        }
    }
}, tokenSource.Token);
task.Start();

app.Run();
