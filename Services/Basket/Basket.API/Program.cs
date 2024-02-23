//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorPages();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//}
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

//app.Run();

public class Program
{
    public static void Main(string[] args)
    {
        var hostBuilder = CreateHostBuilder(args);
        hostBuilder.Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(x =>
        {
            x.UseStartup<Startup>();
        });
}