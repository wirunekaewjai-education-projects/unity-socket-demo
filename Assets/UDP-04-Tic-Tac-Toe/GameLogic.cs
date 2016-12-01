using UnityEngine;
using System.Collections;

namespace Udp04
{
    public static class GameLogic 
    {
        public static string GetResult(SlotView[,] slots, int dimension)
        {
            return  GetHorizontalResult(slots, dimension) ?? 
                GetVerticalResult(slots, dimension) ??
                GetDiagonalResult(slots, dimension) ?? string.Empty;
        }

        private static string GetHorizontalResult(SlotView[,] slots, int dimension)
        {
            for (int y = 0; y < dimension; y++)
            {
                string firstWord = slots[0, y].text;

                //            if (string.IsNullOrEmpty(firstWord))
                //                break;

                bool finished = true;
                for (int x = 1; x < dimension; x++)
                {
                    string otherWord = slots[x, y].text;

                    if (string.IsNullOrEmpty(otherWord) || !string.Equals(firstWord, otherWord))
                    {
                        finished = false;
                        break;
                    }
                }

                if (finished)
                {
                    return firstWord;
                }
            }

            return null;
        }

        private static string GetVerticalResult(SlotView[,] slots, int dimension)
        {
            for (int x = 0; x < dimension; x++)
            {
                string firstWord = slots[x, 0].text;

                //            if (string.IsNullOrEmpty(firstWord))
                //                break;

                bool finished = true;
                for (int y = 0; y < dimension; y++)
                {
                    string otherWord = slots[x, y].text;

                    if (string.IsNullOrEmpty(otherWord) || !string.Equals(firstWord, otherWord))
                    {
                        finished = false;
                        break;
                    }
                }

                if (finished)
                {
                    return firstWord;
                }
            }

            return null;
        }

        private static string GetDiagonalResult(SlotView[,] slots, int dimension)
        {
            // Diagonal Check #1 : 00, 11, 22, 33, 44, ...
            string firstWord = slots[0, 0].text;

            if (!string.IsNullOrEmpty(firstWord))
            {
                bool finished = true;
                for (int xy = 1; xy < dimension; xy++)
                {
                    string otherWord = slots[xy, xy].text;
                    if (!string.Equals(firstWord, otherWord))
                    {
                        finished = false;
                        break;
                    }
                }

                if (finished)
                {
                    return firstWord;
                }
            }

            // Diagonal Check #2 : 40, 31, 22, 13, 04
            int lastIndex = dimension - 1;
            firstWord = slots[lastIndex, 0].text;

            if (!string.IsNullOrEmpty(firstWord))
            {
                bool finished = true;
                for (int y = 1; y < dimension; y++)
                {
                    int x = lastIndex - y;

                    string otherWord = slots[x, y].text;
                    if (!string.Equals(firstWord, otherWord))
                    {
                        finished = false;
                        break;
                    }
                }

                if (finished)
                {
                    return firstWord;
                }
            }

            return null;
        }
    }
}