namespace MedicineX.DbModel
{
    public class OrderDto
    {
        public int UserId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
