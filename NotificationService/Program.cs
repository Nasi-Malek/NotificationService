using NotificationService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();      // gRPC
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapGrpcService<NotifierService>();
app.MapGet("/", () => "This service exposes a gRPC endpoint. Use a gRPC client.");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
