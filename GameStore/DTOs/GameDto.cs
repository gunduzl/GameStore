namespace GameStore.DTOs;

public record class GameDto(
    int Id,
    String Name,
    String Genre,
    decimal Price,
    DateOnly ReleaseDate
);