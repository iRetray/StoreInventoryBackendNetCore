namespace StoreInventoryBackend
{
    internal class Product
    {

        public string Id { get; set; }
        public required string Name { get; set; }
        public required int Amount { get; set; }
        public required int Price { get; set; }
        public required string Description { get; set; }
    }
}