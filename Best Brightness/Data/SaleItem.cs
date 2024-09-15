namespace BestBrightness.Data
{
     public class SaleItem
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public string ProductIdString { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Add this property
        public decimal _total;
        public decimal Total => Quantity * Price;
        /*public decimal Total
        {
            get
            {
                return Quantity * Price;
            }
        }*/

        public Sale Sale { get; set; }
        public Product Product { get; set; }
    }


}
