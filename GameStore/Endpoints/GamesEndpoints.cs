using GameStore.DTOs;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{

 const String GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new(1, "The Witcher 3: Wild Hunt", "RPG", 59.99m, new DateOnly(2015, 5, 19)),
        new(2, "Red Dead Redemption 2", "Action-Adventure", 49.99m, new DateOnly(2018, 10, 26)),
        new(3, "The Legend of Zelda: Breath of the Wild", "Action-Adventure", 59.99m, new DateOnly(2017, 3, 3)),
        new(4, "Grand Theft Auto V", "Action-Adventure", 29.99m, new DateOnly(2013, 9, 17)),
        new(5, "Dark Souls III", "Action RPG", 39.99m, new DateOnly(2016, 4, 12))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app){

    
    var group = app.MapGroup("games")
    .WithParameterValidation();
    
    // GET /games
    group.MapGet("/", () => games);

    // GET by Id /games/1
    group.MapGet("/{id}", (int id) => 
    {
        GameDto? game = games.Find(game => game.Id == id );

        return game is null ? Results.NotFound() : Results.Ok(game);
    })
    .WithName(GetGameEndpointName);


    // POST /games
    group.MapPost("/", (CreateGameDto newGame) =>{

    GameDto game = new(
        games.Count +1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);

        games.Add(game);

        return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id},game);
});


    // Put 
    group.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) => {
    var index = games.FindIndex(game => game.Id == id);

    if(index == -1){
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate);

        return Results.NoContent();
    });


    // Delete /games
    group.MapDelete("games/{id}", (int id) =>
    {
     games.RemoveAll(game => game.Id == id);
     
     return Results.NoContent();
     });


        return group;
    } 



}
