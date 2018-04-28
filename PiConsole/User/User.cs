using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.GameHandling;

namespace PiConsole
{
    [Serializable]
    public class User : IUser
    {

        public User(string name, Guid? newGuid = null)
        {
            Name = name;
            if (newGuid != null) ID = (Guid) newGuid;
            else ID = Guid.NewGuid();
        }

        public Guid ID;
        public string Name;
        private List<IGame> _ownedGames;
        public List<IGame> OwnedGames => _ownedGames ?? (_ownedGames = GameManagement.AllOwnedGames
                                             .Where(game => game.Owner == ID).ToList());

        public void SyncGames()
        {
            List<IGame> allGames = GameManagement.AllOwnedGames;
            List<IGame> gamesExceptOwned = allGames.Except(OwnedGames).ToList();
            _ownedGames.AddRange(gamesExceptOwned.Where(game => game.Owner == ID));
        }


        private bool _isLent(IGame game) => !game.Owner.Equals(game.CurrentUser);

        //invoked by owner
        public void Lend(IGame game)
        {
            if (game._userWhoWantsToBorrow != null) game.CurrentUser = (Guid)game._userWhoWantsToBorrow;
            game._userWhoWantsToBorrow = null;
        }
        public void Reclaim(IGame game) => game.CurrentUser = game.Owner;

        //invoked by other user
        public void BorrowThisGame(Guid otherUserId, IGame game) => game._userWhoWantsToBorrow = otherUserId;
    }
}
