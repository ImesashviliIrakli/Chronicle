using Chronicle.Api.Configurations;
using Chronicle.Application.Middleware;
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .InstallServices(
        builder.Configuration,
typeof(IServiceInstaller).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
