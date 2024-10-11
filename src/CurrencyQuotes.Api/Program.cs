var builder = WebApplication.CreateBuilder(args);

builder.AddCustomServices()
    .Build()
    .UseCustomMiddlewares()
    .Run();
