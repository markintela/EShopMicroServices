using Carter;

var builder = WebApplication.CreateBuilder(args);


//Add Services
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

//Add Marten
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();


app.MapCarter();

//app.MapGet("/", () => "Hello World!");

app.Run();
