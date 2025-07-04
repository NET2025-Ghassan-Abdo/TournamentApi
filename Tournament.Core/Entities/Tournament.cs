using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournaments.Core.Entities
{
    public class Tournament
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Title is required")]
        [MaxLength(25)]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        //Navigation property: en turnering kan ha många matcher
        public ICollection<Game> Games { get; set; }
    }
}
