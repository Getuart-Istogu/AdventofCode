using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode.Puzzle
{
    internal class Day9
    {
        string[] input;
        HashSet<string> visitedCordinates;
        List<(int x, int y)> rope;

        public Day9(int amountOfKnots)
        {
            visitedCordinates = new HashSet<string>();
            rope = new List<(int x, int y)>(amountOfKnots);
            for(int i = 0; i < rope.Capacity; i++)
            {
                if(i==0)
                {
                    rope.Add((0, 0));
                }else
                {
                    rope.Add((0, 0));
                }
            }
            Parse();
        }

        void Parse()
        {
            string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day9.txt";
            input = File.ReadAllLines(inputPath);
            foreach (string line in input)
            {
                var splitLine = line.Split(' ');
                string direction = splitLine[0];
                int amount = int.Parse(splitLine[1]);

                for(int i = 0; i < amount; i++)
                {
                    rope[0] = UpdateHead(direction, rope[0]);
                    //Debug.WriteLine("Head:" + currentHeadPosition.x + ", " + currentHeadPosition.y);

                    // for-schleife: wiederhole Anzahl an Tails (9)
                    // Übergebe UpdateTail 2 Koordinaten. (H, T) --> (H, T1), (T1, T2)...
                    for(int j = 1; j < rope.Count; j++)
                    {
                        rope[j] = UpdateTail(rope[j - 1], rope[j]);
                    }
                    //Debug.WriteLine("Tail:" + currentTailPosition.x + ", " + currentTailPosition.y);
                    
                    visitedCordinates.Add(rope.Last().x.ToString() + "," + rope.Last().y.ToString());
                }
            }
        }

        public int CountUniqueVisits()
        {
            return visitedCordinates.Count;
        }

        private (int x, int y) UpdateHead(string direction, (int x, int y) currentHeadPosition)
        {
            switch(direction)
            {
                case ("U"):
                    currentHeadPosition.y++;
                    break;

                case ("D"):
                    currentHeadPosition.y--;
                    break;

                case ("L"):
                    currentHeadPosition.x--;
                    break;

                case ("R"):
                    currentHeadPosition.x++;
                    break;

                default:
                    Debug.WriteLine("Something regarding with parsing direction went wrong.");
                    break;
            }

            return currentHeadPosition;
        }

        private (int x, int y) UpdateTail((int x, int y) currentHeadPosition, (int x, int y) currentTailPosition)
        {
            int diffX = Math.Abs(currentHeadPosition.x - currentTailPosition.x);
            int diffY = Math.Abs(currentHeadPosition.y - currentTailPosition.y);

            bool previousDiagonal = (diffX + diffY) == 3;

            if (diffY > 1 || previousDiagonal)
            {
                // Up
                if (currentHeadPosition.y > currentTailPosition.y)
                    currentTailPosition.y++;

                // Down
                if (currentHeadPosition.y < currentTailPosition.y)
                    currentTailPosition.y--;
            }

            if (diffX > 1 || previousDiagonal)
            {
                // Right
                if (currentHeadPosition.x > currentTailPosition.x)
                    currentTailPosition.x++;

                // Left
                if (currentHeadPosition.x < currentTailPosition.x)
                    currentTailPosition.x--;
            }

            return currentTailPosition;
        }
    }
}
