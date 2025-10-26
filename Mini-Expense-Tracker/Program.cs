using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Mini_Expense_Tracker.Data;
using Mini_Expense_Tracker.Services;
using Mini_Expense_Tracker.Services.Budget;
using Mini_Expense_Tracker.Services.Export;
using Mini_Expense_Tracker.Services.Interfaces;
using Mini_Expense_Tracker.Services.Notifications;

namespace Mini_Expense_Tracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var home = Environment.GetEnvironmentVariable("HOME")
                       ?? Directory.GetCurrentDirectory();

            var dataDir = Path.Combine(home, "data");
            Directory.CreateDirectory(dataDir);
            var dbPath = Path.Combine(dataDir, "expenses.sqlite");
            

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"
            ));
            builder.Services.AddScoped<IBudgetRule>(sp =>
    new MonthlyBudgetRule(sp.GetRequiredService<IExpenseReader>(), monthlyLimit: 500m));
            builder.Services.AddScoped<IExpenseReader, EfExpenseRepository>();
            builder.Services.AddScoped<IExpenseWriter, EfExpenseRepository>();
            builder.Services.AddScoped<ICategoryReader, EFCategoryRepository>();

            builder.Services.AddScoped<IExpenseService, ExpenseService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped<IExporter, CsvExporter>();
            builder.Services.AddScoped<IExporter, JsonExporter>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            builder.Services.AddScoped<INotificationService, InAppNotificationService>();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    db.Database.Migrate();
                    logger.LogInformation("SQLite migrate OK at {Path}", dbPath);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "SQLite migrate FAILED at {Path}", dbPath);
                }
            }



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Expense}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
