using HR_Platform;
using HR_Platform.Infrastructure;
using HR_Platform.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using HR_Platform.API.Extensions;
using HR_Platform.Application;
using Microsoft.Extensions.DependencyInjection;
using HR_Platform.Infrastructure.BackgroundServices;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
builder.Services.AddPresentation()
                .AddInfrastructure(builder.Configuration)
                .AddApplication();
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

/* Scheduled services  */

builder.Services.AddHostedService<BirthdayNotificationService>();

/**/

WebApplication app = builder.Build(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler("/error");

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseCors("MyAllowSpecificOrigins");
app.UseHttpsRedirection();

app.UseMiddleware<GloblalExceptionHandlingMiddleware>();
 
app.MapControllers();

app.Run();
