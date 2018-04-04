using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniMaxConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Game();

        }

        public static void Game()
        {
            //this bool determines if the user goes first
            bool first = true;

            Player player = new Player();
            Player scndPlayer = new Player();
            //so far there is always a human player so I get there input here for which to play as X or O
            scndPlayer.name = "";
            player.name = Validator.ValidUser();

            //asking the user to go first or second
            string data = Validator.ValidTurn().ToUpper();
            if (data == "YES" || data == "Y")
                first = true;
            else
                first = false;

            //this is where I build the game board that will be displayed
            string[,] board = TicTacToe.BoardStart();
            //display the game board
            TicTacToe.ConsoleBoardDisplay(board);
            //set what AI will play as depending on user choice
            if (player.name == "X")
                scndPlayer.name = "O";
            else
                scndPlayer.name = "X";
            //method that starts the game
            TicTacToe.Game(board, player, scndPlayer, first);
        }

        public static void Continue()
        {
            while (true)
            {
                Console.WriteLine("Play again? Y/N");
                string input = Console.ReadLine().ToUpper();
                if (input == "Y" || input == "YES")
                    Game();
                else if (input == "N" || input == "NO")
                    Environment.Exit(1);
            }
        }
    }
}
