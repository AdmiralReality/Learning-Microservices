using LM.Shop.Service.Entities;

namespace LM.Shop.Service.Extensions
{
    public static class ShopItemExtension
    {
        public static ShopItemDto ToDto(this ShopItem item)
        {
            return new ShopItemDto(item.Id, item.Name, item.Description, item.Price);
        }
    }
}
