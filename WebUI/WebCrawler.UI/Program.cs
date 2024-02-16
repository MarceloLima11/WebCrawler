using WebCrawler.Application.Interfaces;
using WebCrawler.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<CrawlService>();
builder.Services.AddTransient<IDocumentGenerator, PDFGeneratorService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Crawler}/{action=Index}");

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run(port);
