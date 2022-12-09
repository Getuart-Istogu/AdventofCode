using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode.Puzzle
{
    internal class Day8
    {
        enum Direction
        {
            Top,
            Bottom,
            Left,
            Right,
        }

        static int[,] treeMap;
        static string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day8.txt";
        static int numOfRows, numOfColumns;


        static Day8()
        {
            Parser();
        }

        private static void Parser()
        {
            string[] input = File.ReadAllLines(inputPath);
            numOfRows = input.Length;
            numOfColumns = input[0].Length;
            treeMap = new int[numOfRows, numOfColumns];

            for (int i = 0; i < numOfRows; i++)
            {
                for (int j = 0; j < numOfColumns; j++)
                {
                    treeMap[i,j] = (int)Char.GetNumericValue(input[i][j]);
                }
            }
        }

        public static int CountVisibleTrees(ref List<(int,int)> visibleTreeCordinates, ref int maxScenicScore)
        {
            int counter = 0;
            
            int scenicScore = 0;
            List<int> scenicScoreList = new List<int>();

            for (int i = 0; i < numOfRows; i++)
            {
                for (int j = 0; j < numOfColumns; j++)
                {
                    if (i == 0 || i == numOfRows - 1 || j == 0 || j == numOfColumns - 1)
                        counter++;
                    else
                    {
                        if (Visible(treeMap[i, j], i, j, ref visibleTreeCordinates, ref scenicScore))
                        {
                            counter++;                                                       
                        }
                        scenicScoreList.Add(scenicScore);
                    }                            
                }
            }
            maxScenicScore = scenicScoreList.Max();
            return counter;
        }

        private static bool Visible(int checkedTree, int row, int column, ref List<(int,int)> visibleTreeCordinates, ref int scenicScore)
        {
            bool result;            

            List<int> rowsUp = GetDimension(row, column, Direction.Top);      // row - 0
            List<int> rowsDown = GetDimension(row, column, Direction.Bottom); // row - max
            List<int> columnUp = GetDimension(row, column, Direction.Left);   // column - 0
            List<int> columnDown = GetDimension(row, column, Direction.Right);// column - max

            int rowsUpCounter = 0, rowsDownCounter = 0, columnUpCounter = 0, columnDownCounter = 0;
            bool top = VisibleBool(checkedTree, rowsUp, ref rowsUpCounter);
            bool bottom = VisibleBool(checkedTree, rowsDown, ref rowsDownCounter);
            bool left = VisibleBool(checkedTree, columnUp, ref columnUpCounter);
            bool right = VisibleBool(checkedTree, columnDown, ref columnDownCounter);

            if (top || bottom || left || right)
            {
                visibleTreeCordinates.Add((row, column));
                Debug.WriteLine("Visible Tree: " + checkedTree + "| Cordinates: " + row + ", " + column);
                result = true;
            }else
            {
                Debug.WriteLine("Not visible Tree: " + checkedTree + "| Cordinates: " + row + ", " + column);
                result = false;
            }

            scenicScore = rowsUpCounter * rowsDownCounter * columnUpCounter * columnDownCounter;

            return result;
        }

        private static List<int> GetDimension(int row, int column, Direction direction)
        {
            List<int> ints = new List<int>();

            switch(direction)
            {
                case Direction.Top:
                    for(int i = row - 1; i >= 0; i--)
                    {
                        ints.Add(treeMap[i,column]);
                    }
                    break;

                case Direction.Bottom:
                    for (int i = row + 1; i < numOfRows; i++)
                    {
                        ints.Add(treeMap[i, column]);
                    }
                    break;

                case Direction.Left:
                    for (int j = column - 1; j >= 0; j--)
                    {
                        ints.Add(treeMap[row, j]);
                    }
                    break;

                case Direction.Right:
                    for (int j = column + 1; j < numOfColumns; j++)
                    {
                        ints.Add(treeMap[row, j]);
                    }
                    break;

                default:
                    Debug.WriteLine("GetDimensional() - Switch Case found another enum, which isn't supported");
                    break;
            }               

            return ints;
        }

        private static bool VisibleBool(int checkedTree, List<int> dimension, ref int counter2)
        {
            bool result = true;
            int i;
            for(i=0; i < dimension.Count; i++)
            {
                if (dimension[i] >= checkedTree)
                {                    
                    result = false;
                    break;
                }
            }
            if (i == dimension.Count)
                i--;

            counter2 = i+1;
            
            return result;
        }
    }
}
