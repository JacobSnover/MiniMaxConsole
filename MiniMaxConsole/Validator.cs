using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniMaxConsole
{
    class Validator
    {
        //validates for either n X or an O
        public static string ValidUser()
        {
            Console.Write("\nType exit to quit playing or,\nPlay as X or O:");
            string data = "";
            data = Console.ReadLine().ToUpper();
            while (data != "X" && data != "O")
            {

                if (data.ToUpper() == "EXIT") Environment.Exit(1);

                Console.WriteLine("\nInvalid Choice");
                Console.Write("\nType exit to quit playing or,\nPlay as X or O:");
                data = "";
                data = Console.ReadLine().ToUpper();
            }
            return data;
        }
        //validates for if user goes first or second
        public static string ValidTurn()
        {
            Console.WriteLine("\nType exit to quit playing or,\nMake the first move? Y/N");
            string data = "";
            data = Console.ReadLine().ToUpper();
            while (data != "Y" && data != "YES" && data != "N" && data != "NO")
            {

                if (data.ToUpper() == "EXIT") Environment.Exit(1);

                Console.WriteLine("\nInvalid Choice");
                Console.WriteLine("\nType exit to quit playing or,\nMake the first move? Y/N");
                data = "";
                data = Console.ReadLine().ToUpper();
            }
            return data;
        }
        //validates for proper choice on the board
        public static string ValidChoice(Player player, string[] choices)
        {
            Regex reggie = new Regex(@"^[1-9]$");
            Console.Write("\nType exit to quit playing or,\nChoice: ");
            player.choice = "";
            player.choice = Console.ReadLine();
            while (!reggie.IsMatch(player.choice) || choices.Contains(player.choice))
            {
                if (player.choice.ToUpper() == "EXIT") Environment.Exit(1);
                Console.WriteLine("\nInvalid Choice\n");
                Console.Write("\nChoice: ");
                player.choice = "";
                player.choice = Console.ReadLine();
            }
            return player.choice;

        }
        //checks for victory, put this in validator instead of game because of being final decision
        public static Dictionary<string,bool> Judge(string[,] moves, Player player, Player scndPlayer)
        {
            Dictionary<string, bool> champ = new Dictionary<string, bool>();
            bool decision = true;
            Player[] temp = { player, scndPlayer };

            for (int i = 0; i < temp.Length; i++)
            {
                if (moves[0, 0] == temp[i].name && moves[0, 1] == temp[i].name && moves[0, 2] == temp[i].name)
                {
                    decision = true;
                    champ.Add(temp[i].name, decision);                   
                }
                else if (moves[0, 2] == temp[i].name && moves[1, 1] == temp[i].name && moves[2, 0] == temp[i].name)
                {
                    decision = true;
                    champ.Add(temp[i].name, decision);
                }
                else if (moves[0, 0] == temp[i].name && moves[1, 1] == temp[i].name && moves[2, 2] == temp[i].name)
                {
                    decision = true;
                    champ.Add(temp[i].name, decision);
                }
                else if (moves[1, 0] == temp[i].name && moves[1, 1] == temp[i].name && moves[1, 2] == temp[i].name)
                { decision = true;
                    champ.Add(temp[i].name, decision);
                }
                else if (moves[2, 0] == temp[i].name && moves[2, 1] == temp[i].name && moves[2, 2] == temp[i].name)
                {
                    decision = true;
                    champ.Add(temp[i].name, decision);
                }
                else if (moves[0, 0] == temp[i].name && moves[1, 0] == temp[i].name && moves[2, 0] == temp[i].name)
                {
                    decision = true;
                    champ.Add(temp[i].name, decision);
                }
                else if (moves[0, 1] == temp[i].name && moves[1, 1] == temp[i].name && moves[2, 1] == temp[i].name)
                {
                    decision = true;
                    champ.Add(temp[i].name, decision);
                }
                else if (moves[0, 2] == temp[i].name && moves[1, 2] == temp[i].name && moves[2, 2] == temp[i].name)
                {
                    decision = true;
                    champ.Add(temp[i].name, decision);
                }
                else
                {
                    decision = false;
                    champ.Add(temp[i].name, decision);
                } 
            }
            return champ;
        }
    }
}
