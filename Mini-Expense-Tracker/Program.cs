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

            string connectionString;
            var home = Environment.GetEnvironmentVariable("HOME");
            if (!string.IsNullOrEmpty(home))
            {
                var dataDir = Path.Combine(home, "data");
                Directory.CreateDirectory(dataDir);
                var dbPath = Path.Combine(dataDir, "expenses.db");// /home/data  (persistent)
                connectionString = $"Data Source={dbPath}";
            }
            else
            {
                // local dev fallback: project folder "App_Data"
                connectionString = builder.Configuration.GetConnectionString("Default")!;
            }
            

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));
            builder.Services.AddScoped<IBudgetRule>(sp =>
    new MonthlyBudgetRule(sp.GetRequiredService<IExpenseReader>(), monthlyLimit: 500m));
            builder.Services.AddScoped<IExpenseReader, EfExpenseRepository>();
            builder.Services.AddScoped<IExpenseWriter, EfExpenseRepository>();
            builder.Services.AddScoped<ICategoryReader, EFCategoryRepository>();

            builder.Services.AddScoped<IExpenseService, ExpenseService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped<IBudgetRule, MonthlyBudgetRule>();
            builder.Services.AddScoped<IExporter, CsvExporter>();
            builder.Services.AddScoped<IExporter, JsonExporter>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            builder.Services.AddScoped<INotificationService, InAppNotificationService>();

            var app = builder.Build();


            

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
