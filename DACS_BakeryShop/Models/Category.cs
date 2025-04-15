using System.ComponentModel.DataAnnotations;

namespace DACS_BakeryShop.Models
{
    public class Category
    {

        public int Id { get; set; }
        [Required, StringLength(50)]
        public string CategoryName { get; set; }
    }
}
