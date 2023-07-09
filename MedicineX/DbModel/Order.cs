using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedicineX.DbModel
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }


        [ForeignKey("Medicine")]
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal Total { get; set; }
    }
}
