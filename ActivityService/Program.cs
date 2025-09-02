using NotificationContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
// If you have a specific gRPC client type, use it here. Otherwise, remove or comment out this line until the correct client is available.
builder.Services.AddOpenApi();

var notifUrl = builder.Configuration["Grpc:NotificationService"]
               ?? "https://localhost:5065";
builder.Services.AddGrpcClient<Notifier.NotifierClient>(o =>
{
    o.Address = new Uri(notifUrl);
});

builder.Services.AddCors(o => o.AddPolicy("Dev", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("Dev");

app.UseAuthorization();

app.MapControllers();

app.Run();
