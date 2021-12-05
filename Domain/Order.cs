namespace Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Client Client { get; set; }
        public string Status { get; set; }
    }
}
