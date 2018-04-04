using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMaxConsole
{
    class TicTacToe
    {
        private static string bestDefense = ""; //will be the best defensive choice
        private static int side = 1; //so alogorithm knows which side to judge for
        private static int mod = 1; //if user goes second then this gets changed later on
        private static int turn = 1; //used to determine what turn the game is on
        private static int draw = 0; //counter for when game is over
        private static int bestRow = 0; //row index of best choice
        private static int bestCol = 0; //column index of best choice
        public static Dictionary<string, bool> victDict = new Dictionary<string, bool>(); //to store output from algo
        private static string[] choices = new string[9]; //keeps track of available choices for validation
        public static string[,] moves = new string[3, 3]; //simplified game board
        private static Random start = new Random(); //to pick opening move for AI
        private static string[] startMoves = { "1", "2", "3", "4", "5", "6", "7", "8", "9" }; //this array is picked from randomly if AI goes first since AI is unbeatable


        //method that runs the whole game
        public static void Game(string[,] board, Player player, Player scndPlayer, bool first)
        {

            //algo that determines best move of the game
            if (turn == 1)
            {
                bestDefense = MiniMax.BestDefense();

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        if (board[i, j] == bestDefense)
                        {
                            bestRow = i;
                            bestCol = j;
                        }
                    }
                }
            }
            //string will hold the champs name
            string champ = "";

            //checks for which players turn it is by using modulus
            if (turn % 2 == mod && first == true)
            {
                //check for proper move choice
                player.choice = Validator.ValidChoice(player, choices);

                //display the board
                board = PlayerChoice(board, player, scndPlayer, choices);
                ConsoleBoardDisplay(board);

                //check if player has won
                victDict = Validator.Judge(moves, player, scndPlayer);

                //change side to other player this variable is used in the MiniMax algo
                side = 2;
                //set champs name to current player
                champ = player.name;
                //increment counter to check for a draw
                draw++;
                //increment turn counter to change sides
                turn++;
            }
            else if (turn % 2 != mod && first == true)
            {
                //checks if best move is taken and if not then select it
                if (board[bestRow, bestCol] == bestDefense)
                {
                    board[bestRow, bestCol] = scndPlayer.name;
                    choices[0] = bestDefense;
                    moves[1, 1] = scndPlayer.name;
                }
                else
                {
                    //use minimax algo to create AI
                    scndPlayer.choice = MiniMax.Magic(moves, side, player, scndPlayer);
                    board = PlayerChoice(board, scndPlayer, player, choices);
                }

                //display boardd
                ConsoleBoardDisplay(board);

                //check for a victor
                victDict = Validator.Judge(moves, player, scndPlayer);
                //change sides
                side = 1;
                //set champs name to current player
                champ = scndPlayer.name;
                //increment counter to check for a draw
                draw++;
                //increment turn counter to change sides
                turn++;
            }
            else
            {
                //display the board
                board = PlayerChoice(board, scndPlayer, player, choices);
                ConsoleBoardDisplay(board);
                //check for victor
                victDict = Validator.Judge(moves, player, scndPlayer);
                draw++;
                turn++;
                //set bool and modulus variable to true to change state of the game
                first = true;
                mod = 0;
            }

            //check for draw
            if (victDict.ContainsValue(false) && draw == 9)
            {
                //resets the game board and all necessary variables
                moves = new string[3, 3];
                side = 1;
                mod = 1; 
                turn = 1;
                draw = 0;
                Console.WriteLine("\nDraw!!\nPress any key to exit.");
                Console.ReadLine();
                Program.Continue();
            }
            else if (!victDict.ContainsValue(true))//check for victor contnue if no victor, and no draw
                Game(board, player, scndPlayer, first);
            else
                ConsoleVictory(champ);//celebrate the champ
        }

        private static string[,] PlayerChoice(string[,] board, Player player, Player scndPlayer, string[] choices)
        {
            //if AI chooses first move of the game then it chooses predetermined best move
            if (player.choice == null)
            {
                player.choice = bestDefense;
                board[2, 5] = player.name;
                choices[0] = bestDefense;
                moves[1, 1] = player.name;
                return board;
            }

            while (true)
            {
                //check if selection is taken, and if not then select it
                if (board[0, 1] == player.choice && board[0, 1] != player.name && board[0, 1] != scndPlayer.name)
                {
                    board[0, 1] = player.name;
                    choices[0] = "1";
                    moves[0, 0] = player.name;
                    return board;
                }//check if selection is taken, and if not then select it
                else if (board[0, 5] == player.choice && board[0, 5] != player.name && board[0, 5] != scndPlayer.name)
                {
                    board[0, 5] = player.name;
                    choices[0] = "2";
                    moves[0, 1] = player.name;
                    return board;
                }//check if selection is taken, and if not then select it
                else if (board[0, 9] == player.choice && board[0, 9] != player.name && board[0, 9] != scndPlayer.name)
                {
                    board[0, 9] = player.name;
                    choices[0] = "3";
                    moves[0, 2] = player.name;
                    return board;
                }//check if selection is taken, and if not then select it
                else if (board[2, 1] == player.choice && board[2, 1] != player.name && board[2, 1] != scndPlayer.name)
                {
                    board[2, 1] = player.name;
                    choices[0] = "4";
                    moves[1, 0] = player.name;
                    return board;
                }//check if selection is taken, and if not then select it
                else if (board[2, 5] == player.choice && board[2, 5] != player.name && board[2, 5] != scndPlayer.name)
                {
                    board[2, 5] = player.name;
                    choices[0] = "5";
                    moves[1, 1] = player.name;
                    return board;
                }//check if selection is taken, and if not then select it
                else if (board[2, 9] == player.choice && board[2, 9] != player.name && board[2, 9] != scndPlayer.name)
                {
                    board[2, 9] = player.name;
                    choices[0] = "6";
                    moves[1, 2] = player.name;
                    return board;
                }//check if selection is taken, and if not then select it
                else if (board[4, 1] == player.choice && board[4, 1] != player.name && board[4, 1] != scndPlayer.name)
                {
                    board[4, 1] = player.name;
                    choices[0] = "7";
                    moves[2, 0] = player.name;
                    return board;
                }//check if selection is taken, and if not then select it
                else if (board[4, 5] == player.choice && board[4, 5] != player.name && board[4, 5] != scndPlayer.name)
                {
                    board[4, 5] = player.name;
                    choices[0] = "8";
                    moves[2, 1] = player.name;
                    return board;
                }//check if selection is taken, and if not then select it
                else if (board[4, 9] == player.choice && board[4, 9] != player.name && board[4, 9] != scndPlayer.name)
                {
                    board[4, 9] = player.name;
                    choices[0] = "9";
                    moves[2, 2] = player.name;
                    return board;
                }
                else Error();//selection validation
                player.choice = Validator.ValidChoice(player, choices);//loop until proper input
            }
        }
        //method that displays the game board
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
        ///builds/displays the board for the first time
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
        //if there is a victor
        private static void ConsoleVictory(string champ)
        {
            //resets the game board
            moves = new string[3, 3];
            side = 1;
            mod = 1;
            turn = 1;
            draw = 0;
            Console.WriteLine($"\n{champ} Wins!!\nPress any key to exit.");
            Console.ReadLine();
            Program.Continue();
        }
        //if there is an error
        private static bool Error()
        {
            bool correct = true;
            Console.Clear();
            Console.WriteLine("\nInvalid choice!!\nPress any key to try again.\n");
            Console.ReadLine();
            return correct;
        }


    }
}
