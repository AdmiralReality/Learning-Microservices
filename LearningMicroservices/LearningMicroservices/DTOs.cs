using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LM.Shop.Service
{
    public record ShopItemDto(Guid Id, [Required] string Name, string Description, decimal Price);

    public record UpdateShopItemDto([Required] Guid Id, [Required] string Name, string Description, [Required][Range(0, double.MaxValue)] decimal Price);

    public record CreateShopItemDto([Required] string Name, string Description, [Required][Range(0, double.MaxValue)] decimal Price);

    public record RemoveShopItemDto([Required] Guid Id);
}
