using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllersWithViews().AddNewtonsoftJson();



var app = builder.Build();
app.MapControllers();

string webhookHandler = "https://de6e-95-25-141-238.ngrok-free.app";

string? token = app.Configuration["Token"];


string regWebhook = string.Format($"https://api.telegram.org/bot{token}/setWebhook?url={webhookHandler}");
using HttpClient httpClient = new();

var response = await httpClient.GetAsync(regWebhook);
var deserializeResponse = await response.Content.ReadAsStringAsync();
System.Console.WriteLine(deserializeResponse);
if (response.IsSuccessStatusCode)
{
    app.Run();
}
else System.Console.WriteLine("Unable to register a webhook");



