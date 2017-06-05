using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.DAL.DAO
{
    public class GenreItem
    {
        [Key]
        public int GenreId { get; set; }
        public string Genre { get; set; }
    }
}
