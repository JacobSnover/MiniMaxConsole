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
            Random rndMove = new Random();
            int moveChoice = 0;
            int reps = 0;
            int max = 0;
            int min = 0;
            int count = 2;
            int moveCounter = 0;
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

            moveCounter = MoveList(moves, moveCounter, choiceIndex, choiceParlIndex);


            tempMoves = ArrayBuilder(tempMoves, moves);

            moveCounter = 0;

            while (moveChoice != choiceIndex.Count)
            {

                if (side == count)
                {
                
                    tempMoves[choiceIndex[moveCounter], choiceParlIndex[moveCounter]] = player.name;

                    side = 1;
                }
                else if (side != count)
                {
   
                    tempMoves[choiceIndex[moveCounter], choiceParlIndex[moveCounter]] = scndPlayer.name;

                    side = 2;
                }


                champ = Validator.Judge(tempMoves, player, scndPlayer);

                foreach (var item in champ)
                {
                    if (item.Key == player.name && item.Value == true)
                    {
                        if (10 - reps > max)
                        {
                            max = 10 - reps;
                            if (moveCounter == choiceIndex.Count)
                                moveCounter--;
                            maxChoice = possMoves[choiceIndex[moveCounter], choiceParlIndex[moveCounter]];
                        }
                        reps = 0;
                    }
                    else if (item.Key == scndPlayer.name && item.Value == true)
                    {
                        if (-10 + reps < min)
                        {
                            min = -10 + reps;
                            if (moveCounter == choiceIndex.Count)
                                moveCounter--;
                            minChoice = possMoves[choiceIndex[moveCounter], choiceParlIndex[moveCounter]];
                        }
                        reps = 0;
                    }
                }

                reps++;
                moveCounter++;

                if (moveCounter == choiceIndex.Count)
                {
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
                }

            }



            if (0 + max >= 0 - min)
                bestChoice = maxChoice;
            else
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

    }


}
