using Demo.SiganlR.Chat.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(
      builder =>
      {
        builder.WithOrigins("http://192.168.1.2:8000")
              .AllowAnyHeader()
              .WithMethods("GET", "POST")
              .AllowCredentials();
      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();
app.MapHub<ChatHub>("/hub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();

