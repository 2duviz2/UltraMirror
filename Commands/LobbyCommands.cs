using GameConsole;
using plog;
using UltraMirror;

namespace GameConsole.Commands
{
    public class CreateLobbyCommand : ICommand, IConsoleLogger
    {
        public Logger Log { get; } = new Logger("Lobby command");

        public string Name
        {
            get
            {
                return "Create lobby";
            }
        }

        public string Description
        {
            get
            {
                return "Creates a lobby";
            }
        }

        public string Command
        {
            get
            {
                return "createLobby";
            }
        }

        public void Execute(Console con, string[] args)
        {
            Plugin.CreateLobby();
        }
    }

    public class JoinLobbyCommand : ICommand, IConsoleLogger
    {
        public Logger Log { get; } = new Logger("Lobby command");

        public string Name
        {
            get
            {
                return "Join lobby";
            }
        }

        public string Description
        {
            get
            {
                return "Joins a lobby";
            }
        }

        public string Command
        {
            get
            {
                return "joinLobby";
            }
        }

        public void Execute(Console con, string[] args)
        {
            if (args.Length != 1)
            {
                Plugin.JoinLobby("localhost");
                Log.Info($"IP address not provided, doing localhost", null, null, null);
                return;
            }

            Plugin.JoinLobby(args[0]);
        }
    }
}