
using Microsoft.AspNetCore.Authentication;

namespace albumsMinimalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            var albums = new List<Album>
           {
               new Album {Id = 1, Artist = "Tame Impala", Name = "Innerspeaker", Year = 2010},
               new Album {Id = 2, Artist = "MGMT", Name = "Congratulations", Year = 2010},
               new Album {Id = 3, Artist = "Devendra Banhart", Name = "Cripple Crow", Year = 2005},
               new Album {Id = 4, Artist = "Tyler The Creator", Name = "Flowerboy", Year = 2017}
           };


            // Create
            app.MapPost("/album", (Album album) =>
            {
                albums.Add(album);
                return Results.Ok(albums);

            });

            //Read all
            app.MapGet("/albums", () =>
            {
                return Results.Ok(albums);
            });



            //Read by Id
            app.MapGet("/album/{id}", (int id) =>
            {
                var album = albums.Find(x => x.Id == id);

                if (album == null)
                {
                    return Results.NotFound("Sorry, the album was not found");
                }

                return Results.Ok(album);
            });

            //Update
            app.MapPut("/album/{id}", (Album updateAlbum, int id) =>
            {
                var album = albums.Find(x => x.Id == id);

                if (album == null)
                {
                    return Results.NotFound("Sorry, the album was not found");
                }

                albums[id - 1] = updateAlbum;
                return Results.Ok(album);
            });

            //Delete
            app.MapDelete("/album/{id}", (int id) =>
            {
                var album = albums.Find(x => x.Id == id);

                if (album == null)
                {
                    return Results.NotFound("Sorry, the album was not found");
                }

                albums.Remove(album);
                return Results.Ok(album);
            });


            app.Run();
        }
    }
}
