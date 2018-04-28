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
        private static void AddUser(User newUser)
        {
            _users.Add(newUser);
            SerializeHandling.Serialize(_users);
        }
        private static User _currentUser;
        private static List<IGame> _buyableGames;
        private static List<IGame> BuyableGames
        {
            get
            {
                IEnumerable<string> ownedNames = _currentUser.OwnedGames
                    .Select(game => game.Name);
                IEnumerable<IGame> ownedInAvailable = GameManagement.AllAvailableGames
                    .Where(game => ownedNames.Contains(game.Name));
                _buyableGames = GameManagement.AllAvailableGames.Except(ownedInAvailable).ToList();
                return _buyableGames;
            }
        }



        private static IGame AddGame(IGame newGameProto)
        {
            if (_currentUser == null) throw new ArgumentNullException();
            var newGame = new GamePlaceHolder(newGameProto, _currentUser.ID);
            _currentUser.OwnedGames.Add(newGame);
            SerializeHandling.Serialize(_users);
            return newGame;
        }

        private static void ListGames(List<IGame> games)
        {
            string message;
            if (games == null || games.Count == 0)
            {
                message = "No Games to show!";
                Console.WriteLine('\t' + message);
            }
            else
            {
                games.ForEach(game => Console.WriteLine('\t' + game?.Name));
                message = games.Last().Name;
            }
            Console.WriteLine(new string(' ', 8) + new string('º', message.Length) + '\n');
        }


        public static void UserInteraction()
        {
            _currentUser = Intro();
            UserMenu();
        }

        #region Intro
        public static User Intro()
        {
            User user;
            Printer.PrintUnderlined("What do you want to do?");
            do
            {
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. List all available Games");
                Console.WriteLine("9. Exit Console");

                string input = Console.ReadLine()?.ToLower();
                switch (input)
                {
                    case "1":
                    case "1.":
                    case "login":
                    {
                        user = Login();
                        if (user != null)
                        {
                            Console.WriteLine($"Hi, {user.Name}!");
                            return user;
                        }
                            break;
                    }
                    case "2":
                    case "2.":
                    case "register":
                    {
                        user = Register();
                        if (user != null)
                        {
                            Console.WriteLine($"Welcome, {user.Name}!");
                            return user;
                        }
                        break;
                    }
                    case "3":
                    case "3.":
                    case "list":
                    case "list all available games":
                    case "games":
                    {
                        ListGames(GameManagement.AllAvailableGames);
                        break;
                    }
                    case "9":
                    case "9.":
                    case "exit":
                    case "exit console":
                        return null;
                    default:
                    {
                        Printer.PrintUnderlined("Please choose one of the following:");
                        break;
                    }
                }
            } while (true);
        }

        private static User Register()
        {
            bool alreadyExists = false;
            bool isEmptyName = false;
            string input;
            string message = "Provide valid Username or \"exit\":";
            do
            {
                Console.WriteLine(message);
                input = Console.ReadLine()?.Split(' ')[0];
                if (input?.ToLower() == "exit") return null;

                alreadyExists = Users.Exists(user => user.Name == input);
                if (alreadyExists) message = $"\"{input}\" already exists! Choose another Name or \"exit\"!";

                isEmptyName = string.IsNullOrWhiteSpace(input);
                if (isEmptyName) message = "Empty Name hardly counts as valid, does it?";
            } while (alreadyExists || isEmptyName);

            User newUser = new User(input);
            AddUser(newUser);
            return newUser;
        }
        private static User Login()
        {
            User existingUser;
            bool userExists = true;
            string input;
            do
            {
                Console.WriteLine(userExists ? "Provide Username: or \"exit\"" : "User not found, provide existing Username or \"exit\"!");
                input = Console.ReadLine()?.Split(' ')[0];
                if (input?.ToLower() == "exit") return null;
                existingUser = Users.Find(user => user.Name == input);
                userExists = existingUser != null;
            } while (!userExists);

            return existingUser;
        }
        #endregion

        #region UserMenu
        private static void UserMenu()
        {

            Printer.PrintUnderlined($"What do you want to do, {_currentUser.Name}?");
            do
            {
                Console.WriteLine("1. List all my Games");
                Console.WriteLine("2. List all Games i can buy");
                Console.WriteLine("3. Enter command");
                Console.WriteLine("4. List Games from friend"); //TODO
                Console.WriteLine("5. Borrow Game from friend"); //TODO
                Console.WriteLine("6. Any Borrow Requests?"); //TODO
                Console.WriteLine("9. Exit Console");

                switch (Console.ReadLine()?.ToLower())
                {
                    case "1":
                    case "1.":
                    case "list all my games":
                    {
                        ListGames(_currentUser.OwnedGames);
                        break;
                    }
                    case "2":
                    case "2.":
                    case "list all games i can buy":
                    {
                        ListGames(BuyableGames);
                        break;
                    }
                    case "3":
                    case "3.":
                    case "command":
                    case "enter command":
                    {
                        Configure()?.Execute();
                        break;
                    }
                    case "4":
                    case "4.":
                    case "list games from friend":
                    {
                        //TODO
                        break;
                    }
                    case "9":
                    case "9.":
                    case "exit":
                    case "exit console":
                        return;
                    default:
                    {
                        Printer.PrintUnderlined($"{_currentUser.Name}, please choose one of the following:");
                        break;
                    }
                }
            } while (true);
        }


        private static ICommand Configure()
        {
            List<string> inputList;
            IGame chosenGame;
            string gameName;
            do
            {
                Console.WriteLine($"Please provide the command like this: \"[Command] [Name of the Game]\"");
                Printer.PrintUnderlined("or exit with \"Exit\". You can list all available games with \"list\"");

                inputList = Console.ReadLine()?.Split(' ').ToList();
                if (inputList == null || inputList.Count < 2) continue;
                gameName = inputList[1];
                switch (inputList[0].ToLower())
                {
                    case "buy":
                    {
                        chosenGame = ChooseGame(gameName, false);
                        if (chosenGame != null)
                        {
                            if (_currentUser.OwnedGames.Any(game => game.Name == chosenGame.Name))
                            {
                                Console.WriteLine("You already own that Game!");
                                break;
                            }
                            IGame gameCopy = AddGame(chosenGame);
                            return new Buy(gameCopy);
                        }
                        break;
                    }
                    case "download":
                    {
                        chosenGame = ChooseGame(gameName, true);
                        if (chosenGame != null) return new Download(chosenGame);
                        break;
                        }
                    case "install":
                    {
                        chosenGame = ChooseGame(gameName, true);
                        if (chosenGame != null) return new Install(chosenGame);
                        break;
                        }
                    case "start":
                    {
                        chosenGame = ChooseGame(gameName, true);
                        if (chosenGame != null) return new Start(chosenGame);
                        break;
                        }
                    case "uninstall":
                    {
                        chosenGame = ChooseGame(gameName, true);
                        if (chosenGame != null) return new Uninstall(chosenGame);
                        break;
                        }
                    case "update":
                    {
                        chosenGame = ChooseGame(gameName, true);
                        if (chosenGame != null) return new Update(chosenGame);
                        break;
                        }
                    case "exit":
                    {
                        return null;
                    }
                    case "list":
                    {
                        ListGames(GameManagement.AllAvailableGames);
                        break;
                    }
                }
            } while (true);
        }
        private static IGame ChooseGame(string gameName, bool ownGames)
        {
            List<IGame> listToUse = ownGames ? _currentUser.OwnedGames : GameManagement.AllAvailableGames;
            return listToUse?.Find(game => game.Name == gameName);
        }
        #endregion
    }
}
