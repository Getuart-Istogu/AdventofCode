using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode.Puzzle
{
    internal class Day11
    {
        List<Monkey> monkeyList;        

        public Day11()
        {
            monkeyList = new List<Monkey>();
            Parse();
        }
        
        private void Parse()
        {
            string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day11.txt";
            var input = File.ReadAllText(inputPath);

            foreach (var monkeyText in input.Split("\n\n"))
            {
                monkeyList.Add(new Monkey(monkeyText));
            }
            Console.WriteLine();
        }

        public ulong SolvePart1()
        {
            for(int i = 0; i < 20; i++)
            {
                foreach (var monkey in monkeyList)
                {
                    while (monkey.Items.Count > 0)
                    {
                        ulong item = monkey.Items.Dequeue();
                        //Debug.WriteLine(monkey.ID + " " + item);
                        ulong worryLevel = monkey.Operation.Invoke(item) / 3;
                        if(worryLevel % monkey.Divisible == 0)
                            monkeyList[monkey.TrueMonkeyReceiver].Items.Enqueue(worryLevel);
                        else
                            monkeyList[monkey.FalseMonkeyReceiver].Items.Enqueue(worryLevel);

                        monkey.AmountOfInspectedItems++;
                    }                    
                }
                Console.WriteLine("Round " + i);
                for (int k = 0; k < monkeyList.Count; k++)
                {
                    string line = "";
                    foreach (var item in monkeyList[k].Items)
                        line += " " + item + ",";

                    Console.WriteLine("Monkey" + k + line);
                }
                Console.WriteLine("");
            }

            List<Monkey> sortedList = monkeyList.OrderByDescending(m => m.AmountOfInspectedItems).ToList();
            ulong result = sortedList[0].AmountOfInspectedItems * sortedList[1].AmountOfInspectedItems;
            return result;
        }

        public ulong SolvePart2()
        {
            ulong divisor = 1;
            foreach (var monkey in monkeyList)
                divisor *= monkey.Divisible;

            for (int i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkeyList)
                {
                    while (monkey.Items.Count > 0)
                    {
                        ulong item = monkey.Items.Dequeue();
                        //Debug.WriteLine(monkey.ID + " " + item);
                        ulong worryLevel = monkey.Operation.Invoke(item) % divisor;

                        if (worryLevel % monkey.Divisible == 0)
                            monkeyList[monkey.TrueMonkeyReceiver].Items.Enqueue(worryLevel);
                        else
                            monkeyList[monkey.FalseMonkeyReceiver].Items.Enqueue(worryLevel);

                        monkey.AmountOfInspectedItems++;
                    }
                }
                Console.WriteLine("Round " + i);
                /*
                for (int k = 0; k < monkeyList.Count; k++)
                {
                    string line = "";
                    foreach (var item in monkeyList[k].Items)
                        line += " " + item + ",";

                    Console.WriteLine("Monkey" + k + line);
                }
                Console.WriteLine("");*/
            }

            List<Monkey> sortedList = monkeyList.OrderByDescending(m => m.AmountOfInspectedItems).ToList();
            ulong result = sortedList[0].AmountOfInspectedItems * sortedList[1].AmountOfInspectedItems;
            return result;
        }

        internal class Monkey
        {
            public int ID { get; set; }
            public Queue<ulong> Items { get; set; }
            public Func<ulong, ulong> Operation { get; set; }
            public ulong Divisible { get; set; }
            public int TrueMonkeyReceiver { get; set; }
            public int FalseMonkeyReceiver { get; set; }
            public ulong AmountOfInspectedItems { get; set; }

            public Monkey(string monkeyInputText)
            {
                Items = new Queue<ulong>();

                string[] lines = monkeyInputText.Split("\n");
                ID = int.Parse(lines[0].Split(" ")[1].Trim(':'));
                
                foreach(var item in lines[1].Split(":")[1].Trim().Split(", "))
                {
                    Items.Enqueue((ulong)Double.Parse(item));
                }

                var operationText = lines[2].Split(" ");
                var operation = operationText.Reverse().ElementAt(1);
                double operationNum;

                if (Double.TryParse(operationText.Last(), out operationNum))
                {
                    if (operation == "+")
                        Operation = x => x + (ulong)operationNum;
                    else if (operation == "*")
                        Operation = x => x * (ulong)operationNum;
                }
                else
                {
                    if (operation == "+")
                        Operation = x => x + x;
                    else if (operation == "*")
                        Operation = x => x * x;
                }


                Divisible = (ulong)Double.Parse(lines[3].Split(" ").Last());
                TrueMonkeyReceiver = Int32.Parse(lines[4].Split(" ").Last());
                FalseMonkeyReceiver = Int32.Parse(lines[5].Split(" ").Last());

                AmountOfInspectedItems = 0;
            }
        }

    }
}
