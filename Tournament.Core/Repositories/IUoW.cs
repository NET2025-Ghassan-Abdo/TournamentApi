using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournaments.Core.Repositories
{
    public interface IUoW
    {
        ITournamentRepository TournamentRepository { get; }
        IGameRepository GameRepository { get; }
        //CompletAsync Anropas för att spara aKtioner (Add, Update, Remove)
        Task CompletAsync();
    }
}
