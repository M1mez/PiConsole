using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using PiConsole.Commands;
using PiConsole.FileHandling;
using PiConsole.GameHandling;

namespace PiConsole
{
    public static class InputOutput
    {
        public static void PrintUnderlined(string message, char c = 'º') => Console.WriteLine($"{message}\n{new string(c, message.Length)}");

        public static void ListGames(List<IGame> games, bool seeBorrow = false)
        {
            string message;
            if (games == null || games.Count == 0)
            {
                message = "No Games to show!";
                Console.WriteLine('\t' + message);
            }
            else
            {
                if (seeBorrow)
                {
                    foreach (var game in games)
                    {
                        var currentUserString = game.CurrentUser == null || !game.IsLent ? "-" : UserManagement.FindUserName((Guid) game.CurrentUser);
                        var wantsToBorrow = game._userWhoWantsToBorrow == null ? "-" : UserManagement.FindUserName((Guid) game._userWhoWantsToBorrow);
                        Console.WriteLine($"\t{game.Name}, borrowed by: {currentUserString}, wants to be borrowed by: {wantsToBorrow}");
                    }
                }
                else games.ForEach(game => Console.WriteLine($"\t{game?.Name}"));
                message = games.Last().Name;
            }
            Console.WriteLine(new string(' ', 8) + new string('º', message.Length) + '\n');
        }

        public static User Register()
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

                alreadyExists = UserManagement.Users.Exists(user => user.Name == input);
                if (alreadyExists) message = $"\"{input}\" already exists! Choose another Name or \"exit\"!";

                isEmptyName = String.IsNullOrWhiteSpace(input);
                if (isEmptyName) message = "Empty Name hardly counts as valid, does it?";
            } while (alreadyExists || isEmptyName);

            User newUser = new User(input);
            UserManagement.AddUser(newUser);
            return newUser;
        }

        public static User Login()
        {
            User existingUser;
            bool userExists = true;
            string input;
            do
            {
                Console.WriteLine(userExists ? "Provide Username: or \"exit\"" : "User not found, provide existing Username or \"exit\"!");
                input = Console.ReadLine()?.Split(' ')[0];
                if (input?.ToLower() == "exit") return null;
                existingUser = UserManagement.Users.Find(user => user.Name == input);
                userExists = existingUser != null;
            } while (!userExists);

            return existingUser;
        }

        public static User InputFriendName()
        {
            string friendName;
            User friend = null;
            bool userExists = true;
            PrintUnderlined($"Please provide a user's name or \"exit\"!");
            do
            {
                if (!userExists) Console.WriteLine("User not found! Provide name or \"exit\"!");
                friendName = Console.ReadLine()?.Trim().ToLower();
                if (friendName == null || friendName.Equals("exit")) return null;
                userExists = (friend = UserManagement.Users.Find(user => user.Name == friendName)) != null;
            } while (!userExists);

            return friend;
        }

        public static IGame ChooseGameFromList(List<IGame> games)
        {
            string gameString;
            IGame chosenGame = null;
            bool gameExists = true;
            PrintUnderlined($"Please provide a game's game or \"exit\"!");
            do
            {
                if (!gameExists) Console.WriteLine("Game not found! Provide game or \"exit\"!");
                gameString = Console.ReadLine()?.Trim().ToLower();
                if (gameString == null || gameString.Equals("exit")) return null;
                gameExists = (chosenGame = games.Find(game => game.Name.ToLower() == gameString)) != null;
            } while (!gameExists);

            return chosenGame;
        }

        public static ICommand Configure(User currentUser)
        {
            IGame chosenGame;
            do
            {
                Console.WriteLine($"Please provide the command like this: \"[Command] [Name of the Game]\"");
                PrintUnderlined("or exit with \"Exit\". You can list all available games with \"list\"");

                List<string> inputList = Console.ReadLine()?.Split(' ').ToList();
                if (inputList == null || inputList.Count < 2) continue;
                string commandName = inputList[0].ToLower();
                string gameName = inputList[1];
                ICommand cmd = null;

                if (commandName == "buy")
                {
                    chosenGame = GameManagement.ChooseGame(gameName, false, currentUser);
                    if (chosenGame != null)
                    {
                        if (currentUser.OwnedGames.Any(game => game.Name == chosenGame.Name))
                        {
                            Console.WriteLine("You already own that Game!");
                            break;
                        }
                        IGame gameCopy = UserManagement.AddGame(chosenGame);
                        cmd = new Buy(gameCopy);
                    }
                    break;
                }

                chosenGame = GameManagement.ChooseGame(gameName, true, currentUser);
                switch (inputList[0].ToLower())
                {
                    case "download":
                    {
                        cmd = new Download(chosenGame);
                        break;
                    }
                    case "install":
                    {
                        cmd = new Install(chosenGame);
                        break;
                    }
                    case "start":
                    {
                        cmd = new Start(chosenGame);
                        break;
                    }
                    case "uninstall":
                    {
                        cmd = new Uninstall(chosenGame);
                        break;
                    }
                    case "update":
                    {
                        cmd = new Update(chosenGame);
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

                return cmd;
            } while (true);

            return null;
        }

        public static User Intro()
        {
            Console.Clear();
            PrintUnderlined("What do you want to do?");
            do
            {
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. List all available Games");
                Console.WriteLine("4. Sync Games");
                Console.WriteLine("9. Exit Console");

                string input = Console.ReadLine()?.ToLower();
                User user;
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
                    {
                        ListGames(GameManagement.AllAvailableGames);
                        break;
                    }
                    case "4":
                    case "4.":
                    case "sync games":
                    {
                        GameManagement.SyncGames();
                        break;
                    }
                    case "9":
                    case "9.":
                    case "exit":
                    case "exit console":
                        return null;
                    default:
                    {
                        Console.Clear();
                        PrintUnderlined("Please choose one of the following:");
                        break;
                    }
                }
                SerializeHandling.SyncSerialize();
            } while (true);
        }

        public static void UserMenu(User currentUser)
        {
            Console.Clear();
            PrintUnderlined($"What do you want to do, {currentUser.Name}?");
            do
            {
                //PrintUnderlined($"Logged in: {currentUser.Name}");
                Console.WriteLine("1. List all my Games");
                Console.WriteLine("2. List all Games i can buy");
                Console.WriteLine("3. Enter command");
                Console.WriteLine("4. List Games from friend");
                Console.WriteLine("5. Borrow Game from friend");
                Console.WriteLine("6. Any Borrow Requests?");
                Console.WriteLine("7. Lend Games");
                Console.WriteLine("8. Reclaim Games");
                Console.WriteLine("9. Log out");

                switch (Console.ReadLine()?.ToLower())
                {
                    case "1":
                    case "1.":
                    case "list all my games":
                    {
                        Console.WriteLine("Owned Games:");
                        ListGames(currentUser.OwnedGames, true);
                        Console.WriteLine("Borrowed Games:");
                        ListGames(currentUser.BorrowedGames);
                        break;
                    }
                    case "2":
                    case "2.":
                    case "list all games i can buy":
                    {
                        ListGames(currentUser.BuyableGames);
                        break;
                    }
                    case "3":
                    case "3.":
                    case "command":
                    case "enter command":
                    {
                        Configure(currentUser)?.Execute();
                        break;
                    }
                    case "4":
                    case "4.":
                    case "list games from friend":
                    {
                        User friend = InputFriendName();
                        if (friend != null) ListGames(friend.OwnedGames);
                        break;
                    }
                    case "5":
                    case "5.":
                    case "borrow":
                    case "borrow game from friend":
                    {
                        User friend = InputFriendName();
                        if (friend == null) break;
                        IGame game = ChooseGameFromList(friend.OwnedGames);
                        if (game != null) game._userWhoWantsToBorrow = currentUser.ID;
                        break;
                    }
                    case "6":
                    case "6.":
                    case "requests":
                    {
                        ListGames(currentUser.WantsToBeBorrowed, true);
                        break;
                    }
                    case "7":
                    case "7.":
                    case "lend":
                    case "lend games":
                    {
                        ListGames(currentUser.WantsToBeBorrowed, true);
                        IGame game = ChooseGameFromList(currentUser.WantsToBeBorrowed);
                        currentUser.Lend(game);
                        break;
                    }
                    case "8":
                    case "8.":
                    case "reclaim":
                    case "reclaim games":
                    {
                        var borrowedOut = currentUser.OwnedGames.Where(g => g.IsLent).ToList();
                        ListGames(borrowedOut, true);
                        IGame game = ChooseGameFromList(borrowedOut);
                        currentUser.Reclaim(game);
                        break;
                    }
                    case "9":
                    case "9.":
                    case "logout":
                    case "log out":
                    {
                        return;
                    }
                    default:
                    {
                        Console.Clear();
                        PrintUnderlined($"{currentUser.Name}, please choose one of the following:");
                        break;
                    }
                }
                SerializeHandling.SyncSerialize();
            } while (true);
        }


    }
}
