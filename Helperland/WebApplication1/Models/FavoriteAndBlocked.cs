using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class FavoriteAndBlocked
    {
        [Key]
        public int Id { get; set; }
        public string UserID { get; set; }
        public string TargetUserId { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsBlocked { get; set; }
    }
}
