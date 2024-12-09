namespace StoreInventoryBackend
{
    internal class Product
    {
        public required string Name { get; set; }
        public int Amount { get; set; }
        public required string Description { get; set; }
    }
}