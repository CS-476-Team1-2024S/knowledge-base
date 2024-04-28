var builder = WebApplication.CreateBuilder(args);

const string host = "localhost"; //Change depending on local or production environment

// Add services to the container.
builder.WebHost.UseUrls($"http://{host}:5000", $"https://{host}:5001");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(
    options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
