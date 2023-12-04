using orgcat.web;

var builder = WebApplication.CreateBuilder(args);
WebBuilder.ConfigureBuilder(builder);

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
