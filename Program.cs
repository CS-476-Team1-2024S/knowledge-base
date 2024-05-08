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

using System.Text.Json.Nodes;

namespace KnowledgeBase
{
    public class Program()
    {
        static void Main()
        {
            Directory dir = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(),"Root"));
            var paths = Indexer.SearchTFIDF("text");

            var jsonArray = new JsonArray();
            foreach (var path in paths)
            {
                jsonArray.Add(path);
            }
            var response = JResponse.Create(true, "Search complete.",new JsonObject{["FilePaths"] = jsonArray});

            Console.WriteLine(response);

            Directory source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Root"));
            response = JResponse.Create(true,"Scan complete.",source.ToJSON());

            Console.WriteLine(response);
        }
    }
}