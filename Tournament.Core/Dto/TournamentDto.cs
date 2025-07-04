using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.Entities;

namespace Tournaments.Core.Dto
{
    public class TournamentDto
    {
        public int Id { get; set; }
        

        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate
        {
            get
            {
                return StartDate.AddMonths(3);
            }
        }
        public IEnumerable<GameDto> Games { get; set; }
    }
}
