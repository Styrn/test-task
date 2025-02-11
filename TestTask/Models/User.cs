using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Models
{
    [Index(nameof(Username), IsUnique=true)]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(75)]
        [Required]
        public string Username { get; set; }

        [MaxLength(128), Required]
        public string Password { get; set; }

        [MaxLength(100), Required]
        public string First_Name { get; set; }

        [MaxLength(100), Required]
        public string Last_Name { get; set; }

        public virtual List<Message> Messages { get; set; } = new();
    }
}
