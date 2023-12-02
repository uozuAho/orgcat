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
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
