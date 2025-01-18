using Serilog;
using Serilog.Events;

namespace MedicalVisits.API.Extension;

public static class SerilogExtensions
{
    public static IHostBuilder UseSerilogLogging(this IHostBuilder hostBuilder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341", restrictedToMinimumLevel: LogEventLevel.Information)
            .Enrich.FromLogContext()
            .CreateLogger();

        hostBuilder.UseSerilog();
        return hostBuilder;
    }
}