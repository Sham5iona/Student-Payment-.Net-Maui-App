using Microsoft.Extensions.Logging;
using StudentPaymentApp.Model.Services;
using StudentPaymentApp.ViewModel;
using StudentPaymentApp.Views;
using Syncfusion.Maui.Core.Hosting;
using StudentPaymentApp.Data;

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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            // Register services and view models
            builder.Services.AddTransient<IAppointmentService, AppointmentService>();
            builder.Services.AddTransient<IStudentService, StudentService>();
            builder.Services.AddTransient<AppointmentViewModel>();
            builder.Services.AddTransient<SchedulerViewModel>();
            builder.Services.AddTransient<StudentViewModel>();
            builder.Services.AddTransient<ShowStudentsViewModel>();
            builder.Services.AddSingleton<StudentPaymentDbContext>();


            // Register pages
            builder.Services.AddSingleton<SchedulePage>();
            builder.Services.AddSingleton<AddAppointmentPage>();
            builder.Services.AddSingleton<StudentsPage>();
            builder.Services.AddSingleton<AddStudentPage>();
            builder.Services.AddSingleton<EditStudentPage>();
            builder.Services.AddSingleton<EditAppointmentPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
