using System.ComponentModel.DataAnnotations;

namespace GameStore.DTOs;

public record class CreateGameDto(
    [Required][StringLength(50)]String Name,
    [Required][StringLength(20)]String Genre,
    [Range(1,100)]decimal Price,
    DateOnly ReleaseDate
);