using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AdventofCode.Puzzle
{
    internal class Day7
    {
        public static int Part1()
        {
            string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day7.txt";
            ElfDirectory rootDirectory = new ElfDirectory("/", null);
            ElfDirectory currentDirectory = null;
            var input = File.ReadAllLines(inputPath);
            string currentDirectoryName = "";

            for (int i = 0; i < input.Length; i++)
            {

                if (input[i].StartsWith("$ cd"))
                {
                    currentDirectoryName = input[i].Split(' ').Last();

                    switch (currentDirectoryName)
                    {
                        case "..":
                            currentDirectory = currentDirectory.Parent;
                            break;

                        case "/":
                            currentDirectory = rootDirectory;
                            break;

                        default:
                            currentDirectory = currentDirectory.Childs.Find(x => x.Name == currentDirectoryName);
                            break;

                    }
                }
                else if (input[i].StartsWith("$ ls"))
                {
                    for (i = i + 1; i < input.Count() && !input[i].StartsWith("$"); i++)
                    {
                        if (input[i].StartsWith("dir "))
                        {
                            currentDirectoryName = input[i].Split(" ").Last();
                            currentDirectory.Childs.Add(new ElfDirectory(currentDirectoryName, currentDirectory));
                        }
                        else if (char.IsDigit(input[i][0]))
                        {
                            string[] fileInfo = input[i].Split(" "); //0:Size ; 1:Name
                            currentDirectory.Files.Add(new ElfFile(name: fileInfo[1], size: int.Parse(fileInfo[0])));
                        }
                    }
                    i--;
                }
            }
            int sum = 0;
            List<int> directorySpace = new List<int>();
            rootDirectory.SizeWhichCanBeDeleted(ref sum, ref directorySpace);

            int answerPart2;
            int totalDiskSpace = 70000000;
            int updateSpace = 30000000;
            int currentlyUsed = rootDirectory.Size;

            int needed = updateSpace - (totalDiskSpace - currentlyUsed);
            directorySpace.Sort();
            directorySpace.Reverse();
            for (int i = 0; i < directorySpace.Count(); i++)
            {
                if (needed > directorySpace[i]) //358913 > 341260 [73] 366028 [72]
                {
                    answerPart2 = directorySpace[i - 1];
                    break;
                }

            }
            return 0;
        }

        class ElfDirectory
        {
            public string Name { get; set; }                 // /                a           e
            public List<ElfFile> Files { get; set; }         // b.txt, c.dat     f,g,h.lst   i
            public ElfDirectory Parent { get; set; }         // null             /           a          
            public List<ElfDirectory> Childs { get; set; }   // a, d             e           null
            public int Size { get; set; }

            public ElfDirectory(string name, ElfDirectory parent)
            {
                Name = name;
                Files = new List<ElfFile>();
                Parent = parent;
                Childs = new List<ElfDirectory>();
                Size = 0;
            }

            public int SizeWhichCanBeDeleted(ref int sum, ref List<int> directorySpace)
            {
                Debug.WriteLine(Name);
                int result = 0;
                if (Files.Count > 0)
                {
                    foreach (var file in Files)
                    {
                        result += file.Size;
                    }
                }

                if (Childs.Count > 0)
                {
                    foreach (var child in Childs)
                    {
                        result += child.SizeWhichCanBeDeleted(ref sum, ref directorySpace);
                    }
                }

                if (result <= 100000)
                {
                    sum += result;
                }

                Size = result;
                directorySpace.Add(result);
                return Size;
            }
        }

        class ElfFile
        {
            public string Name { get; set; }
            public int Size { get; set; }

            public ElfFile(string name, int size)
            {
                Name = name;
                Size = size;
            }
        }
    }
}
