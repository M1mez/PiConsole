using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.FileHandling;
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
        private List<IGame> _ownedGames = new List<IGame>();
        public List<IGame> OwnedGames
        {
            get { return _ownedGames; }
            set
            {
                _ownedGames = value;
            }
        }

        private List<IGame> _buyableGames;
        public List<IGame> BuyableGames
        {
            get
            {
                IEnumerable<string> ownedNames = OwnedGames
                    ?.Select(game => game.Name);
                IEnumerable<IGame> ownedInAvailable = new List<IGame>();
                if (ownedNames != null)
                    ownedInAvailable = GameManagement.AllAvailableGames
                        .Where(game => ownedNames.Contains(game.Name));
                _buyableGames = GameManagement.AllAvailableGames.Except(ownedInAvailable).ToList();
                return _buyableGames ?? new List<IGame>();
            }
        }

        private List<IGame> _borrowedGames;
        public List<IGame> BorrowedGames => _borrowedGames ?? (_borrowedGames = new List<IGame>());

        public List<IGame> WantsToBeBorrowed =>
            OwnedGames.FindAll(game => game._userWhoWantsToBorrow != null).ToList();

        //invoked by owner
        public void Lend(IGame game)
        {
            if (!WantsToBeBorrowed.Contains(game)) throw new Exception($"game {game.Name} does not exist in OwnedGames!");
            if (game._userWhoWantsToBorrow != null) game.CurrentUser = (Guid) game._userWhoWantsToBorrow;
            var userWhoWantsToBorrow = UserManagement.Users?.Find(user => user.ID == game._userWhoWantsToBorrow);
            userWhoWantsToBorrow?.BorrowedGames.Add(game);
            game._userWhoWantsToBorrow = null;
        }
        public void Reclaim(IGame game)
        {
            if (!OwnedGames.Contains(game)) throw new Exception($"game {game.Name} does not exist in OwnedGames!");
            var currentUser = UserManagement.Users?.Find(user => user.ID == game.CurrentUser);
            game.CurrentUser = game.Owner;
            currentUser?.BorrowedGames.RemoveAll(borrowed => borrowed.Name == game.Name && borrowed.Owner == game.Owner);
        }
    }
}
