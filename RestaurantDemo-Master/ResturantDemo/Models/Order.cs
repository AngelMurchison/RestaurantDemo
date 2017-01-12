using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResturantDemo.Models
{
    public class Orders
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public DateTime timeCreated { get; set; } = DateTime.Now;

        public bool isFulfilled { get; set; }

        public string userId { get; set; }

        public string destination { get; set; }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double TotalPrice
        {
            get
            {
                return Order.Sum(s => s.Price);
            }
        }

        public virtual ICollection<MenuItem> Order { get; set; } = new HashSet<MenuItem>();

        [ForeignKey("userId")]
        public virtual ApplicationUser User { get; set; }
    }
}