using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournaments.Core.Entities
{
    public class Game
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Title is required")]
        [MaxLength(100)]
        public string Title { get; set; }
        public DateTime Time { get; set; }
        //Foreign Key
        public int TournamentId { get; set; }
        //Navigation property: matchen tillhör en turnering 
        public Tournament TournamentDetails { get; set; }
    }
}
