using System.ComponentModel.DataAnnotations;

namespace LM.Shop.Service
{
    public record ShopItemDto(Guid Id, string Name, string Description, decimal Price);

    public record UpdateShopItemDto([Required] Guid Id, string Name, string Description, [Required][Range(0, double.MaxValue)] decimal Price);

    public record CreateShopItemDto([Required] string Name, string Description, [Required][Range(0, double.MaxValue)] decimal Price, DateTimeOffset CreatedDate);

    public record RemoveShopItemDto([Required] Guid Id);
}
