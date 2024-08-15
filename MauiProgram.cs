using Microsoft.Extensions.Logging;
using StudentPaymentApp.Model.Services;
using StudentPaymentApp.ViewModel;
using StudentPaymentApp.Views;
using Syncfusion.Maui.Core.Hosting;
using StudentPaymentApp.Data;
using CommunityToolkit.Maui.Core;

namespace StudentPaymentApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .UseMauiCommunityToolkitCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            // Register services and view models
            builder.Services.AddTransient<IAppointmentService, AppointmentService>();
            builder.Services.AddTransient<IStudentService, StudentService>();
            builder.Services.AddTransient<IPaymentService, PaymentService>();
            builder.Services.AddTransient<IAnalyticsService, AnalyticsService>();
            builder.Services.AddTransient<AppointmentViewModel>();
            builder.Services.AddTransient<SchedulerViewModel>();
            builder.Services.AddTransient<StudentViewModel>();
            builder.Services.AddTransient<ShowStudentsViewModel>();
            builder.Services.AddTransient<AnalyticsViewModel>();
            builder.Services.AddSingleton<StudentPaymentDbContext>();


            // Register pages
            builder.Services.AddSingleton<SchedulePage>();
            builder.Services.AddSingleton<AddAppointmentPage>();
            builder.Services.AddSingleton<StudentsPage>();
            builder.Services.AddSingleton<AddStudentPage>();
            builder.Services.AddSingleton<EditStudentPage>();
            builder.Services.AddSingleton<EditAppointmentPage>();
            builder.Services.AddSingleton<FilteredAppointmentsPage>();
            builder.Services.AddSingleton<AnalyticsPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
