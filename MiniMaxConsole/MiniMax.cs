using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMaxConsole
{
    class MiniMax
    {
        public static string Magic(string[,] moves, int side, Player player, Player scndPlayer)
        {
           
            int playerSide = 1;
            int intSide = side;
            Random rndMove = new Random();
            int moveChoice = 0;
            int reps = 0;
            int max = 0;
            int min = 0;
            int count = 2;
            int moveCounter = 0;
            string bestDefense = "";
            string bestChoice = "";
            string maxChoice = "";
            string minChoice = "";
            Dictionary<string, bool> champ = new Dictionary<string, bool>();
            string[,] tempMoves = new string[3, 3];
            string[,] tempState = new string[3, 3];
            List<int> choiceIndex = new List<int>();
            List<int> choiceParlIndex = new List<int>();
            List<int> currChoiceIndex = new List<int>();
            List<int> currChoiceParlIndex = new List<int>();
            string[,] possMoves = {
                { "1", "2", "3" },
                { "4", "5", "6" },
                { "7", "8", "9" }
            };

         ;

            moveCounter = MoveList(moves, moveCounter, choiceIndex, choiceParlIndex);

            tempMoves = ArrayBuilder(tempMoves, moves);

            moveCounter = 0;

            while (moveChoice != choiceIndex.Count)
            {

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

                if (intSide == count && moveCounter > 0 && tempMoves[choiceIndex[moveCounter], choiceParlIndex[moveCounter]] == null)
                {

                    tempMoves[choiceIndex[moveCounter], choiceParlIndex[moveCounter]] = player.name;

                    intSide = 1;
                }
                else if (intSide == count && moveCounter > 0 && tempMoves[choiceIndex[moveCounter], choiceParlIndex[moveCounter]] != null && moveCounter + 1 < choiceIndex.Count)
                {

                    tempMoves[choiceIndex[moveCounter + 1], choiceParlIndex[moveCounter + 1]] = player.name;

                    intSide = 1;
                }
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


                champ = Validator.Judge(tempMoves, player, scndPlayer);

                foreach (var item in champ)
                {
                    if (item.Key == player.name && item.Value == true)
                    {
                        if (10 - reps >= max)
                        {
                            max = 10 - reps;
                            if (moveCounter == choiceIndex.Count)
                                moveCounter--;
                            maxChoice = possMoves[choiceIndex[moveChoice], choiceParlIndex[moveChoice]];
                        }
                        reps = 0;
                    }
                    else if (item.Key == scndPlayer.name && item.Value == true)
                    {
                        if (-10 + reps <= min)
                        {
                            min = -10 + reps;
                            if (moveCounter == choiceIndex.Count)
                                moveCounter--;
                            minChoice = possMoves[choiceIndex[moveChoice], choiceParlIndex[moveChoice]];
                        }
                        reps = 0;
                    }
                }

                reps++;
                moveCounter++;

                if (moveCounter == choiceIndex.Count)
                {
                    intSide = side;
                    moveCounter = 0;
                    //choiceIndex.Insert(moveChoice, currChoiceIndex[0]);
                    //choiceParlIndex.Insert(moveChoice, currChoiceParlIndex[0]);
                    //choiceIndex.Add(choiceIndex[0]);
                    //choiceParlIndex.Add(choiceParlIndex[0]);
                    //choiceIndex.RemoveAt(0);
                    //choiceParlIndex.RemoveAt(0);
                    tempMoves = ArrayBuilder(tempMoves, moves);
                    reps = 0;
                    moveChoice++;
                    if (moveChoice == choiceIndex.Count && playerSide < 2)
                    {
                        count = 1;
                        moveChoice = 0;
                        playerSide = 2;
                    }
                }

            }



            if (max == 10)
            {
                bestChoice = maxChoice;
            }
            else if (0 + max < 0 - min)
                bestChoice = minChoice;

            if (bestChoice == "")
            {
                int index = rndMove.Next(0, choiceIndex.Count - 1);
                bestChoice = possMoves[choiceIndex[index], choiceParlIndex[index]];
                return bestChoice;
            }
            else
                return bestChoice;

        }

        private static int MoveList(string[,] moves, int moveCounter, List<int> choiceIndex, List<int> choiceParlIndex)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (moves[i, j] == null)
                    {
                        choiceIndex.Insert(moveCounter, i);
                        choiceParlIndex.Insert(moveCounter, j);

                        moveCounter++;
                    }
                }
            }

            return moveCounter;
        }

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

        public static string BestDefense()
        {
            int selectionValue = 0;
            string selection = "";
            Dictionary<string, int> positions = new Dictionary<string, int>();
            positions.Add("1", 0); positions.Add("2", 0); positions.Add("3", 0);
            positions.Add("4", 0); positions.Add("5", 0); positions.Add("6", 0);
            positions.Add("7", 0); positions.Add("8", 0); positions.Add("9", 0);
            string[,] victories = { { "1", "2", "3" }, { "3", "5", "7" }, { "1", "5", "9" }, { "4", "5", "6" },
                { "7", "8", "9" }, { "1","4","7"}, { "2","5","8"}, { "3","6","9"} };
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    positions[victories[i, j]]++;
                    if (selectionValue < positions[victories[i,j]])
                    {
                        selectionValue = positions[victories[i, j]];
                        selection = victories[i, j];
                    }
                }
            }

            //selection = positions ;

            return selection;
        }

    }


}
