using orgcat.domain;
using orgcat.postgresdb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
var connString = builder.Configuration.GetConnectionString("OrgCatDb");
if (string.IsNullOrEmpty(connString))
{
    throw new Exception("Connection string is empty");
}
builder.Services.AddOrgCatPostgresDb(connString);
builder.Services.AddTransient<ISurveyService, SurveyService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.Urls.Add("http://*:5001");
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
