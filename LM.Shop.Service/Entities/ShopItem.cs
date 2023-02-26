namespace LM.Shop.Service.Entities
{
    public class ShopItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
