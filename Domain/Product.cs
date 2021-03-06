namespace Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public Category Category { get; set; }
        public string? Img { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Pieces { get; set; }
        public decimal Price { get; set; }
    }
}
