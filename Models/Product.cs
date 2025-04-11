namespace blazorappdemo
{
    public class Product{
        public int id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }
        public string[] images { get; set; }
        public string image{ get; set; }
    }
}