using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMaxConsole
{
    class MiniMax
    {
        //method takes the current state of the game board, which side is playing, and both player objects
        public static string Magic(string[,] moves, int side, Player player, Player scndPlayer)
        {

            int playerSide = 1; //variables to determine which side to evaluate
            int intSide = side; //reset's side to original
            int moveChoice = 0; //used to index the movelist
            int reps = 0; //depth counter
            int max = 0; //max value
            int min = 0; //min value
            int count = 2; //to switch sides in algrithm
            int moveCounter = 0; //counts the moves for each game board state
            string bestChoice = ""; //the move that is returned
            string maxChoice = ""; //max move
            string minChoice = ""; //min move
            Dictionary<string, bool> champ = new Dictionary<string, bool>(); //holds info for each gameboard state
            string[,] tempMoves = new string[3, 3]; //copy of moves argument that algo uses to determine states
            List<int> choiceIndex = new List<int>(); //parellel list for 2d array indexing, this is my rows counter
            List<int> choiceParlIndex = new List<int>(); //parellel list for 2d array indexing, this is my columns counter
            List<int> currChoiceIndex = new List<int>(); //parellel list to hold the current move row in the algo
            List<int> currChoiceParlIndex = new List<int>(); //parellel list to hold the current move column in the algo
            //allows me to give the correct move back to the game after determining the best choice
            string[,] possMoves = {
                { "1", "2", "3" },
                { "4", "5", "6" },
                { "7", "8", "9" }
            };
            //method that builds a list of the current possible choices
            moveCounter = MoveList(moves, moveCounter, choiceIndex, choiceParlIndex);
            //method that builds the 2d array that the algo will use to determine the game states
            tempMoves = ArrayBuilder(tempMoves, moves);
            //resets the counter for use in the algo
            moveCounter = 0;
            //loop runs until there are no more moves to evaluate
            while (moveChoice != choiceIndex.Count)
            {
                //this will set the current move being made in each game state and sets the correct player to that move
                if (intSide == count && moveCounter == 0)
                {
                    tempMoves[choiceIndex[moveChoice], choiceParlIndex[moveChoice]] = player.name;
                    moveCounter = 0;
                    intSide = 1;
                }
                else if (intSide != count && moveCounter == 0)
                {

                    tempMoves[choiceIndex[moveChoice], choiceParlIndex[moveChoice]] = scndPlayer.name;
                    moveCounter = 0;
                    intSide = 2;
                }
                //checks which side is currently playing, and also makes sure the current move isn't already taken
                if (intSide == count && moveCounter > 0 && tempMoves[choiceIndex[moveCounter], choiceParlIndex[moveCounter]] == null)
                {

                    tempMoves[choiceIndex[moveCounter], choiceParlIndex[moveCounter]] = player.name;

                    intSide = 1;
                }//same as previous, but if previous position wasn't null this increments the index of the move list, as long as that move isn't out of bounds
                else if (intSide == count && moveCounter > 0 && tempMoves[choiceIndex[moveCounter], choiceParlIndex[moveCounter]] != null && moveCounter + 1 < choiceIndex.Count)
                {

                    tempMoves[choiceIndex[moveCounter + 1], choiceParlIndex[moveCounter + 1]] = player.name;

                    intSide = 1;
                }//ssame as before but for opposite side
                else if (intSide != count && moveCounter > 0 && tempMoves[choiceIndex[moveCounter], choiceParlIndex[moveCounter]] == null)
                {

                    tempMoves[choiceIndex[moveCounter], choiceParlIndex[moveCounter]] = scndPlayer.name;

                    intSide = 2;
                }
                else if (intSide != count && moveCounter > 0 && tempMoves[choiceIndex[moveCounter], choiceParlIndex[moveCounter]] != null && moveCounter + 1 < choiceIndex.Count)
                {

                    tempMoves[choiceIndex[moveCounter + 1], choiceParlIndex[moveCounter + 1]] = scndPlayer.name;

                    intSide = 2;
                }

                //checks for a champ after each move, stores value in dictionary which holds the player name and bool for victory
                champ = Validator.Judge(tempMoves, player, scndPlayer);

                foreach (var item in champ)
                {   //if the dictionary hold the players name and a true bool value then the depth is counted
                    if (item.Key == player.name && item.Value == true)
                    {   //is the max minus the depth counter is greater then the best choice is set to current move
                        if (10 - reps >= max)
                        {
                            max = 10 - reps;
                            if (moveCounter == choiceIndex.Count)//makes sure to grab last move instead of out of bounds
                                moveCounter--; 
                            maxChoice = possMoves[choiceIndex[moveChoice], choiceParlIndex[moveChoice]]; //sets move for the max
                        }
                        reps = 0; //reset the depth counter
                    }
                    else if (item.Key == scndPlayer.name && item.Value == true)
                    {   //check for opposite side, does same calculation against depth counter, but for negative numbers to determine the min
                        if (-10 + reps <= min)
                        {
                            min = -10 + reps;
                            if (moveCounter == choiceIndex.Count)//keeps choice within bounds
                                moveCounter--;
                            minChoice = possMoves[choiceIndex[moveChoice], choiceParlIndex[moveChoice]]; //sets move for the min
                        }
                        reps = 0; //reset the depth counter
                    }
                }

                reps++; //increment depth counter
                moveCounter++; //increment move counter

                //resets move counter if it has reached last possible move for this game state
                if (moveCounter == choiceIndex.Count)
                {
                    //resets players side in case move list is not an even number of possible moves
                    intSide = side;
                    moveCounter = 0; //reset move counter
                    tempMoves = ArrayBuilder(tempMoves, moves); //rebuild temp array for new game state
                    reps = 0; //reset depth counter
                    moveChoice++; //advance first move of new game state
                    if (moveChoice == choiceIndex.Count && playerSide < 2) //if out of moves for first player then switch players
                    {
                        count = 1;
                        moveChoice = 0;
                        playerSide = 2;
                    }
                }

            }

            if (max == 10) //if the opponent has a instant victory then block that move
            {
                bestChoice = maxChoice;
            }
            else //or take move that is best for current player
                bestChoice = minChoice; 

            return bestChoice; //return the best choice
        }
        //method that looks at the current game board and creates a list of all the possible moves
        private static int MoveList(string[,] moves, int moveCounter, List<int> choiceIndex, List<int> choiceParlIndex)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (moves[i, j] == null) //check game board for null which will become a possible move
                    {
                        choiceIndex.Insert(moveCounter, i);
                        choiceParlIndex.Insert(moveCounter, j);

                        moveCounter++; //only increment when a possiblle move is found
                    }
                }
            }

            return moveCounter;
        }
        //method that builds a copy of the current game board to use within algo
        private static string[,] ArrayBuilder(string[,] tempMoves, string[,] moves)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tempMoves[i, j] = moves[i, j];
                }

            }

            return tempMoves;
        }
        //method that determines the best move of the game
        public static string BestDefense()
        {
            int selectionValue = 0;
            string selection = "";
            //collection that holds the posiible choices and a counter of how many time used to achieve victory
            Dictionary<string, int> positions = new Dictionary<string, int>();
            positions.Add("1", 0); positions.Add("2", 0); positions.Add("3", 0);
            positions.Add("4", 0); positions.Add("5", 0); positions.Add("6", 0);
            positions.Add("7", 0); positions.Add("8", 0); positions.Add("9", 0);
            //2d array that holds the possible victory scenarios
            string[,] victories = {
                { "1", "2", "3" }, { "3", "5", "7" },
                { "1", "5", "9" }, { "4", "5", "6" },
                { "7", "8", "9" }, { "1", "4", "7" },
                { "2", "5", "8" }, { "3", "6", "9" }
            };
            //these loops look at all the possible win scenarios and determines which
            //selection gets used more then the others, therefore it's either the best
            //move to start with or play it on the second move if it's not taken
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    positions[victories[i, j]]++;
                    if (selectionValue < positions[victories[i, j]])
                    {
                        selectionValue = positions[victories[i, j]];
                        selection = victories[i, j];
                    }
                }
            }
            return selection;
        }

    }


}
