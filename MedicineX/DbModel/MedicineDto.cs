namespace MedicineX.DbModel
{
    public class MedicineDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string ExpDate { get; set; }
        public int Quantity { get; set; }
    }
}
