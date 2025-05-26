using LocalUserServiceProvider.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IAppUserService, AppUserService>();

//builder.Services.AddGrpcClient<ProfileContract.ProfileContractClient>(x =>
//{
//    x.Address = new Uri(builder.Configuration["ProfileServiceProvider"]!);
//});

//builder.Services.AddGrpcClient<AccountContract.AccountContractClient>(x =>
//{
//    x.Address = new Uri(builder.Configuration["AccountServiceProvider"]!);
//});

var app = builder.Build();

app.MapGet("/", () => "LocalUserServiceProvider is running.");
app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();