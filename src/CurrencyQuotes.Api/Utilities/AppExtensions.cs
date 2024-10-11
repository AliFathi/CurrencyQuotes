using CurrencyQuotes.Utilities.ExchangeRates;
using CurrencyQuotes.Utilities.Http;

namespace Microsoft.AspNetCore.Builder;

public static class AppExtensions
{
    public static WebApplicationBuilder AddCustomServices(
        this WebApplicationBuilder builder
    )
    {
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<ApiHttpClient>();

        builder.Services.AddSingleton<ExchangeRatesApi>();
        builder.Services.AddSingleton(
            builder.Configuration.GetRequiredSection("ExchangeRates").Get<ExchangeRatesOptions>()!
        );

        builder.Services.AddControllersWithViews()
            .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = null);

        return builder;
    }

    public static WebApplication UseCustomMiddlewares(
        this WebApplication app
    )
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        return app;
    }
}
