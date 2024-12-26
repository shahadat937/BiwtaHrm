using Hrm.Application;
using Hrm.Persistence;
using Hrm.Identity;
using Hrm.Api.Extensions;
using Newtonsoft.Json.Converters;
using Hrm.Api.Middleware;
using Hrm.Infrastructure;
using Hrm.Infrastructure.SignalRHub;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);
builder.Services.ConfigureInfrastructureService(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddControllers().AddNewtonsoftJson(jsonOptions =>
{
    jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
});


builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowOrigin", options => options
                 .WithOrigins("http://localhost:4200")
                 .AllowAnyMethod()
                 .SetIsOriginAllowed((host) => true)
              .AllowAnyHeader()
              .AllowCredentials());
    /*o.AddPolicy("AllowOrigin",
       builder => builder.AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader());*/
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseSwaggerDocumention();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowOrigin");
app.UseAuthorization();

app.MapHub<NotificationHub>("/notification");
app.MapControllers();

//app.Run();
await app.RunAsync();
