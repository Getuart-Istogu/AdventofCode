using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode.Puzzle
{
    internal class Day10
    {
        int cycle;
        int register;
        int signalStrengthSum;
        List<int> cycleList;
        String[] crtDrawing;

        public Day10()
        {
            cycle = 1;
            register = 1;
            signalStrengthSum = 0;
            cycleList = new List<int>() { 20, 60, 100, 140, 180, 220 };
            crtDrawing = new String[6];
        }

        public String[] Part2()
        {
            string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day10.txt";
            var input = File.ReadAllLines(inputPath);

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == "noop")
                {
                    DrawCRT(ref crtDrawing);
                    cycle++;
                }
                else
                {
                    DrawCRT(ref crtDrawing);
                    cycle++;
                    DrawCRT(ref crtDrawing);
                    cycle++;
                    string line = input[i];
                    int vVal = Int32.Parse(input[i].Split(" ").Last());
                    register += vVal;
                }
            }

            return crtDrawing;
        }

        public void DrawCRT(ref String[] crtDrawing)
        {
            int row = cycle / 40;
            int column = cycle % 40;

            if (column == 0)
            {
                row--;
                column = 40;
            }

            if (row == 4 && column == 0)
                Console.WriteLine("Falsch");

            if (column - 1 == register || column - 1 == register + 1 || column - 1 == register - 1)
                crtDrawing[row] += "#";
            else
                crtDrawing[row] += ".";
        }

        public int Part1()
        {
            string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day10.txt";
            var input = File.ReadAllLines(inputPath);

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == "noop")
                {
                    cycle++;
                    if (cycleList.Contains(cycle))
                    {
                        int signalStrength = cycle * register;
                        signalStrengthSum += signalStrength;
                    }
                }
                else
                {
                    cycle++;
                    if(cycleList.Contains(cycle))
                    {
                        int signalStrength = cycle * register;
                        signalStrengthSum += signalStrength;
                    }
                    cycle++;
                    string line = input[i];
                    int vVal = Int32.Parse(input[i].Split(" ").Last());
                    register += vVal;
                    if (cycleList.Contains(cycle))
                    {
                        int signalStrength = cycle * register;
                        signalStrengthSum += signalStrength;
                    }                                       
                }
            }

            return signalStrengthSum;
        }
    }
}
