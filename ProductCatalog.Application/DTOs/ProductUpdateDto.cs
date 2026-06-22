using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Application.DTOs;

public sealed class ProductUpdateDto
{
    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Name { get; init; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; init; }

    [Range(0, 999999999)]
    public decimal Price { get; init; }

    [Range(0, int.MaxValue)]
    public int Stock { get; init; }
}
