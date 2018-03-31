using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMaxConsole
{
    class TicTacToe
    {

        private static int side = 1; //so alogorithm knows which side to judge for
        private static int mod = 1; //if user goes second then this gets changed later on
        private static int turn = 1; //used to dtermine what turn the game is on
        private static int draw = 0; //counter for when game is over
        public static Dictionary<string, bool> victDict = new Dictionary<string, bool>(); //to store output from algo
        private static string[] choices = new string[9]; //keeps track of available choices, might not be needed
        public static string[,] moves = new string[3, 3]; //simplified game board
        private static Random start = new Random(); //to pick opening move for AI
        private static string[] startMoves = { "1", "3", "7", "9" };


        public static void Game(string[,] board, Player player, Player scndPlayer, bool first)
        {
            string champ = ""; bool victory = true;

            if (turn % 2 == mod && first == true)
            {
                player.choice = Validator.ValidChoice(player, choices);

                board = PlayerChoice(board, player, scndPlayer, choices);
                ConsoleBoardDisplay(board);

                victDict = Validator.Judge(moves, player, scndPlayer);
                side = 2;
                champ = player.name;
                draw++;
                turn++;
            }
            else if (turn % 2 != mod && first == true)
            {
                if (board[2, 5] == "5")
                {
                    board[2, 5] = scndPlayer.name;
                    choices[0] = "5";
                    moves[1, 1] = scndPlayer.name;
                }
                else
                {
                    scndPlayer.choice = MiniMax.Magic(moves, side, player, scndPlayer);
                    board = PlayerChoice(board, scndPlayer, player, choices);
                }

                ConsoleBoardDisplay(board);

                victDict = Validator.Judge(moves, player, scndPlayer);
                side = 1;
                champ = scndPlayer.name;
                draw++;
                turn++;
            }
            else
            {
                board = PlayerChoice(board, scndPlayer, player, choices);
                ConsoleBoardDisplay(board);
                victDict = Validator.Judge(moves, player, scndPlayer);
                draw++;
                turn++;
                first = true;
                mod = 0;
            }

            if (victDict.ContainsValue(false) && draw == 9)
                Console.WriteLine("\nDraw!!\n");
            else if (!victDict.ContainsValue(true))
                Game(board, player, scndPlayer, first);
            else
                ConsoleVictory(champ);
        }

        private static string[,] PlayerChoice(string[,] board, Player player, Player scndPlayer, string[] choices)
        {
            if (player.choice == null)
                player.choice = startMoves[start.Next(0, 3)];

            if (board[0, 1] == player.choice && board[0, 1] != player.name && board[0, 1] != scndPlayer.name)
            {
                board[0, 1] = player.name;
                choices[0] = "1";
                moves[0, 0] = player.name;
            }
            else if (board[0, 5] == player.choice && board[0, 5] != player.name && board[0, 5] != scndPlayer.name)
            {
                board[0, 5] = player.name;
                choices[0] = "2";
                moves[0, 1] = player.name;
            }
            else if (board[0, 9] == player.choice && board[0, 9] != player.name && board[0, 9] != scndPlayer.name)
            {
                board[0, 9] = player.name;
                choices[0] = "3";
                moves[0, 2] = player.name;
            }
            else if (board[2, 1] == player.choice && board[2, 1] != player.name && board[2, 1] != scndPlayer.name)
            {
                board[2, 1] = player.name;
                choices[0] = "4";
                moves[1, 0] = player.name;
            }
            else if (board[2, 5] == player.choice && board[2, 5] != player.name && board[2, 5] != scndPlayer.name)
            {
                board[2, 5] = player.name;
                choices[0] = "5";
                moves[1, 1] = player.name;
            }
            else if (board[2, 9] == player.choice && board[2, 9] != player.name && board[2, 9] != scndPlayer.name)
            {
                board[2, 9] = player.name;
                choices[0] = "6";
                moves[1, 2] = player.name;
            }
            else if (board[4, 1] == player.choice && board[4, 1] != player.name && board[4, 1] != scndPlayer.name)
            {
                board[4, 1] = player.name;
                choices[0] = "7";
                moves[2, 0] = player.name;
            }
            else if (board[4, 5] == player.choice && board[4, 5] != player.name && board[4, 5] != scndPlayer.name)
            {
                board[4, 5] = player.name;
                choices[0] = "8";
                moves[2, 1] = player.name;
            }
            else if (board[4, 9] == player.choice && board[4, 9] != player.name && board[4, 9] != scndPlayer.name)
            {
                board[4, 9] = player.name;
                choices[0] = "9";
                moves[2, 2] = player.name;
            }
            else Error();

            return board;
        }

        public static void ConsoleBoardDisplay(string[,] board)
        {
            Console.Clear();
            int counter = 0;
            for (int i = 0; i < 5; i++)
            {

                for (int j = 0; j < 11; j++)
                {

                    Console.Write(board[i, j]);
                    if (counter == 10 || counter == 21 || counter == 32 || counter == 43 || counter == 54)
                        Console.WriteLine();
                    counter++;
                }

            }

        }

        public static string[,] BoardStart()
        {
            string[,] board = new string[5, 11];

            string data =
                " 1 | 2 | 3 " +
                "--- --- ---" +
                " 4 | 5 | 6 " +
                "--- --- ---" +
                " 7 | 8 | 9 ";

            int counter = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    board[i, j] = data[counter].ToString();
                    counter++;
                }
            }

            return board;
        }

        private static void ConsoleVictory(string champ)
        {
            Console.WriteLine($"\n{champ} Wins!!\n");
        }

        private static void Error()
        {
            Console.Clear();
            Console.WriteLine("\nInvalid choice!!\nPress any key to try again.\n");
            Console.ReadLine();
        }


    }
}
