using LocalUserServiceProvider.Middlewares;
using LocalUserServiceProvider.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IAppUserService, AppUserService>();

var app = builder.Build();

app.MapGet("/", () => "LocalUserServiceProvider is running.");
app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
//app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();