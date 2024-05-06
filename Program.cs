// var builder = WebApplication.CreateBuilder(args);

// const string host = "140.146.23.39"; //Change depending on local or production environment

// // Add services to the container.
// builder.WebHost.UseUrls($"https://{host}:5001");

// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// app.UseCors(
//     options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
// );

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();

namespace KnowledgeBase
{
    public class Program()
    {
        static void Main()
        {
            Directory dir = new Directory(Path.Combine(System.IO.Directory.GetCurrentDirectory(),"Root"));
            var relative = dir.ToJSON();
            Console.WriteLine(relative);
        }
    }
}