using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using PiConsole.Commands;
using PiConsole.FileHandling;
using PiConsole.GameHandling;
using PiConsole.GameStates;

namespace PiConsole
{
    public static class UserManagement
    {
        private static List<User> _users;
        public static List<User> Users => _users ?? (_users = SerializeHandling.Deserialize(Constants.UsersFile).Cast<User>().ToList());
        public static void AddUser(User newUser)
        {
            _users.Add(newUser);
            SerializeHandling.Serialize(_users);
        }

        public static string FindUserName(Guid userGuid) => _users.Find(user => user.ID == userGuid)?.Name;
        public static User _currentUser;

        public static IGame AddGame(IGame newGameProto)
        {
            if (_currentUser == null) throw new ArgumentNullException();
            var newGame = new GamePlaceHolder(newGameProto, _currentUser.ID);
            _currentUser.OwnedGames.Add(newGame);
            SerializeHandling.Serialize(_users);
            return newGame;
        }

        public static void UserInteraction()
        {
            var x = GameManagement.SavedAvailableGames;
            var y = UserManagement.Users;
            do
            {
                _currentUser = InputOutput.Intro();
                if (_currentUser != null) InputOutput.UserMenu(_currentUser);
            } while (_currentUser != null);
        }
    }
}
