// See https://aka.ms/new-console-template for more information
using AdventofCode.Puzzle;
using System.Diagnostics;

Console.WriteLine("Hello, World!");
Day11 day11 = new Day11();
Console.WriteLine(day11.SolvePart2());



static int Day6(int amountOfUniqueCharsNeeded)
{
    string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day6.txt";
    string inputAll = File.ReadAllText(inputPath);
    
    for(int i = 0; i <= inputAll.Length; i++)
    {
        var checkMarker = inputAll.Substring(i);
        if(checkMarker.Count() >= amountOfUniqueCharsNeeded)
        {
            string marker = inputAll.Substring(i, amountOfUniqueCharsNeeded);

            if(marker.Distinct().Count() == amountOfUniqueCharsNeeded)
            {
                return i + amountOfUniqueCharsNeeded;
            }
        }
    }
    return 0;
}

static string Day5_2()
{
    string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day5.txt";
    string inputAll = File.ReadAllText(inputPath);
    string[] inputs = inputAll.Split("\n\n");

    
    var crateStacks = new List<Stack<char>>(9);
    var crateInputs = new List<List<char>>(9);
    for (int i = 0; i < crateInputs.Capacity; i++)
    {
        crateInputs.Add(new List<char>());
        crateStacks.Add(new Stack<char>());
    }

    // Stacks-Parsing
    /*  inputs[0] = header or beginning stack
     *  inputs[1] = command
     */
    var chunks = inputs[0].Chunk(4);
    int crateID = 0;
    foreach (var chunk in chunks)
    {
        if (Char.IsDigit(chunk[1]))
            break;

        if (Char.IsLetter(chunk[1]))
        {
            crateInputs[crateID].Add(chunk[1]);
        }
        crateID++;
        if (crateID == 9)
            crateID = 0;
    }

    // Reverse & Push in Stack
    for (int i = 0; i < crateInputs.Capacity; i++)
    {
        crateInputs[i].Reverse();
        for (int j = 0; j < crateInputs[i].Count; j++)
            crateStacks[i].Push(crateInputs[i][j]);
    }

    // Commands-Parsing
    var commands = inputs[1].Split(new[] { "move", "from", "to" }, StringSplitOptions.RemoveEmptyEntries);
    /* commadsInt[0] = move
     * commandsInt[1] = from
     * commandsInt[2] = to
     */
    List<int[]> commandsInt = new List<int[]>();
    int commandsIntIndex = 0;
    List<char> tempStackForPush = new List<char>();
    for (int i = 0; i < commands.Length; i += 3)
    {
        commandsInt.Add(new int[] { Int32.Parse(commands[i]), Int32.Parse(commands[i + 1]), Int32.Parse(commands[i + 2]) });
        for (int moveAmount = 0; moveAmount < commandsInt[commandsIntIndex][0]; moveAmount++)
        {
            tempStackForPush.Add(crateStacks[commandsInt[commandsIntIndex][1] - 1].Pop());          
        }
        tempStackForPush.Reverse();
        foreach(char c in tempStackForPush)
        {
            crateStacks[commandsInt[commandsIntIndex][2] - 1].Push(c);
        }        

        commandsIntIndex++;
        tempStackForPush.Clear();
    }

    string result = "";
    foreach (var crate in crateStacks)
    {
        result += crate.Peek();
    }

    return result;
}

static string Day5_1()
{
    string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day5.txt";
    string inputAll = File.ReadAllText(inputPath);
    string[] inputs = inputAll.Split("\n\n");

    // Stacks-Parsing
    var crateStacks = new List<Stack<char>>(9);      
    var crateInputs = new List<List<char>>(9);
    for (int i = 0; i < crateInputs.Capacity; i++)
    {
        crateInputs.Add(new List<char>());
        crateStacks.Add(new Stack<char>());
    }    

    /*  inputs[0] = header or beginning stack
     *  inputs[1] = command
     */
    var chunks = inputs[0].Chunk(4);
    int crateID = 0;
    foreach (var chunk in chunks) 
    {
        if (Char.IsDigit(chunk[1]))
            break;

        if (Char.IsLetter(chunk[1]))
        {
            crateInputs[crateID].Add(chunk[1]);
        }
        crateID++;
        if(crateID == 9)
            crateID = 0;
    }

    // Reverse & Push in Stack
    for(int i = 0; i < crateInputs.Capacity; i++)
    {
        crateInputs[i].Reverse();
        for(int j=0; j < crateInputs[i].Count; j++)
            crateStacks[i].Push(crateInputs[i][j]);
    }
    
    // Commands-Parsing
    var commands = inputs[1].Split(new[] { "move", "from", "to" }, StringSplitOptions.RemoveEmptyEntries);
    /* commadsInt[0] = move
     * commandsInt[1] = from
     * commandsInt[2] = to
     */
    List <int[]> commandsInt = new List<int[]>();
    int commandsIntIndex = 0;
    for(int i=0; i<commands.Length; i+=3)
    {
        commandsInt.Add(new int[] { Int32.Parse(commands[i]), Int32.Parse(commands[i + 1]), Int32.Parse(commands[i + 2]) });
        for(int moveAmount=0; moveAmount < commandsInt[commandsIntIndex][0]; moveAmount++)
        {
            char crate = crateStacks[commandsInt[commandsIntIndex][1]-1].Pop();
            crateStacks[commandsInt[commandsIntIndex][2]-1].Push(crate);
        }
        commandsIntIndex++;
    }

    string result = "";
    foreach(var crate in crateStacks)
    {
        result += crate.Peek();
    }

    return result;
}


static int Day4_2()
{
    string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day4.txt";
    List<String> items = new List<string>();
    int counter = 0;

    foreach (string line in File.ReadLines(inputPath))
    {
        string[] pairs = line.Split(',');

        string[] borderA = pairs[0].Split('-');
        int startA = Int32.Parse(borderA[0]);
        int endA = Int32.Parse(borderA[1]);
        var rangeA = Enumerable.Range(startA, endA - startA + 1);

        string[] borderB = pairs[1].Split('-');
        int startB = Int32.Parse(borderB[0]);
        int endB = Int32.Parse(borderB[1]);
        var rangeB = Enumerable.Range(startB, endB - startB + 1);

        if (rangeA.Intersect(rangeB).Count() > 0)
        {
            counter++;
        }
    }

    return counter;
}

static int Day4_1()
{
    string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day4.txt";
    List<String> items = new List<string>();
    int counter = 0;

    foreach (string line in File.ReadLines(inputPath))
    {
        string[] pairs = line.Split(',');
        
        string[] borderA = pairs[0].Split('-');
        int startA = Int32.Parse(borderA[0]);
        int endA = Int32.Parse(borderA[1]);
        var rangeA = Enumerable.Range(startA, endA-startA+1);

        string[] borderB = pairs[1].Split('-');
        int startB = Int32.Parse(borderB[0]);
        int endB = Int32.Parse(borderB[1]);
        var rangeB = Enumerable.Range(startB, endB - startB + 1);

        if (rangeA.All(i=>rangeB.Contains(i)) || rangeB.All(i => rangeA.Contains(i)))
        {
            counter++;
        }
    }

    return counter;
}

static int Day3_2()
{
    string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day3.txt";
    List<String> items = new List<string>();
    int priority;
    int sumOfPrio = 0;
    
    foreach (string line in File.ReadLines(inputPath))
    {
        items.Add(line);
        if (items.Count == 3)
        { 
            var firstResult = items[0].Intersect(items[1]);
            var intersection = firstResult.Intersect(items[2]).First();
            if (Char.IsUpper(intersection))
            {
                priority = (int)intersection - 38;
            }
            else
            {
                priority = (int)intersection - 96;
            }

            sumOfPrio += priority;
            items.Clear();
        }
        
    }
    return sumOfPrio;
}

static int Day3_1()
{
    string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day3.txt";
    List<char[]> itemTypes = new List<char[]>();
    int priority;
    int sumOfPrio = 0;
    foreach (string line in File.ReadLines(inputPath))
    {
        int length = line.Length;
        string[] compartments = new[]
        {
            line.Substring(0, length/2),
            line.Substring(length/2, length - length/2)
        };

        var intersection = compartments[0].Intersect(compartments[1]).First();            
        if(Char.IsUpper(intersection))
        {
            priority = (int)intersection - 38;
        } else
        {
            priority = (int)intersection - 96;
        }

        sumOfPrio += priority;      
    }
    return sumOfPrio;
}

static void Day2_2()
{
    /* 
     * A = St   1
     * B = P    2
     * C = Sc   3
     * 
     * X = L    0
     * Y = D    3
     * Z = W    6
     */
    string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day2.txt";
    int points = 0;

    foreach (string line in File.ReadLines(inputPath))
    {
        switch(line)
        {
           case "A X": 
                points += 3; // 0 + 3
                break;
            case "A Y":
                points += 4; // 3 + 1
                break;
            case "A Z":
                points += 8; // 6 + 2
                break;
            case "B X":
                points += 1; // 0 + 1
                break;
            case "B Y":
                points += 5; // 3 + 2 
                break;
            case "B Z":
                points += 9; // 6 + 3
                break;
            case "C X":
                points += 2; // 0 + 2
                break;
            case "C Y":
                points += 6; // 3 + 3
                break;
            case "C Z":
                points += 7; // 6 + 1
                break;
        }
    }
    Console.WriteLine(points);
}

static void Day2_1()
{
    /* 
     * A, X = St   1
     * B, Y = P    2
     * C, Z = Sc   3
     */
    string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day2.txt";
    int points = 0;

    foreach(string line in File.ReadLines(inputPath))
    {
        switch (line[2])
        {
            case 'X': points += 1;
                break;
            case 'Y':
                points += 2;
                break;
            case 'Z':
                points += 3;
                break;
            default:
                Debug.WriteLine("Invalid char for: " + line);
                break;
        }

        if (line[0] == 'A' && line[2] == 'X' || line[0] == 'B' && line[2] == 'Y' || line[0] == 'C' && line[2] == 'Z')
            points += 3;
        else if (line[0] == 'A' && line[2] == 'Z' || line[0] == 'B' && line[2] == 'X' || line[0] == 'C' && line[2] == 'Y')
            points += 0;
        else
            points += 6;
    }
    Console.WriteLine(points);
}

static void Day1()
{
    string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day1.txt";

    List<int> elvesSum = new List<int>();
    int currentElf = 0;

    foreach (string line in File.ReadLines(inputPath))
    {
        if (String.IsNullOrWhiteSpace(line))
        {
            elvesSum.Add(currentElf);
            currentElf = 0;
            continue;
        }

        currentElf += Int32.Parse(line);
    }

    Console.WriteLine("Part 1 " + elvesSum.Max());
    elvesSum.Sort();
    elvesSum.Reverse();
    int result = elvesSum.Take(3).Sum();

    Console.WriteLine("Part 2 " + result);
}