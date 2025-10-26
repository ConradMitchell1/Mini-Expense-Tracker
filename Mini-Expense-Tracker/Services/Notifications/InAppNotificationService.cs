using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Mini_Expense_Tracker.Services.Interfaces;
using System.Runtime.CompilerServices;

namespace Mini_Expense_Tracker.Services.Notifications
{
    public class InAppNotificationService : INotificationService
    {
        private readonly ITempDataDictionaryFactory _tempDataFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InAppNotificationService(
            ITempDataDictionaryFactory tempDataFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _tempDataFactory = tempDataFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task NotifyAsync(string message)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return Task.CompletedTask;
            var tempData = _tempDataFactory.GetTempData(httpContext);
            tempData["NotificationMessage"] = message;

            return Task.CompletedTask;

        }
    }
}
