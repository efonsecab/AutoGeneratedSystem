﻿using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using AutoGeneratedSystem.ClientServices;

namespace AutoGeneratedSystem.MauiBlazor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            string appSettingsResourceName = string.Empty;
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MauiProgram)).Assembly;
#if DEBUG
            appSettingsResourceName = "AutoGeneratedSystem.MauiBlazor.appsettings.Development.json";
#else
		appSettingsResourceName = "AutoGeneratedSystem.MauiBlazor.appsettings.json";
#endif
            Stream stream = assembly.GetManifestResourceStream(appSettingsResourceName);
            builder.Configuration.AddJsonStream(stream);

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif
            var apiBaseAddress = builder.Configuration["ApiBaseAddress"];
            builder.Services.AddHttpClient($"AutoGeneratedSystem.ServerAPI.Anonymous", client =>
                client.BaseAddress = new Uri(apiBaseAddress));

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                            .CreateClient($"AutoGeneratedSystem.ServerAPI.Anonymous"));
            builder.Services.AddBlazoredToast();
            builder.Services.AddTransient<HttpClientService>();
            builder.Services.AddTransient<ApplicationUserClientService>();
            builder.Services.AddTransient<ApplicationRoleClientService>();
            builder.Services.AddTransient<CompanyClientService>();
            builder.Services.AddTransient<ApplicationUserApplicationRoleClientService>();
            builder.Services.AddTransient<StoreClientService>();
            builder.Services.AddSingleton<IErrorBoundaryLogger, ErrorBoundaryLogger>();

            return builder.Build();
        }
    }

    public class ErrorBoundaryLogger: IErrorBoundaryLogger
    {
        //
        // Summary:
        //     Logs the supplied exception.
        //
        // Parameters:
        //   exception:
        //     The System.Exception to log.
        //
        // Returns:
        //     A System.Threading.Tasks.ValueTask representing the completion of the operation.
        public ValueTask LogErrorAsync(Exception exception)
        {
            return ValueTask.CompletedTask;
        }
    }
}