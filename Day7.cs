namespace AdventofCode
{
    internal class Day7
    {
        public static int Part1()
        {
            string inputPath = @"C:\Users\Geti\source\repos\AdventofCode\Input\Day7Test.txt";
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
                    int j;
                    for(j = i+1; !input[j].StartsWith("$") && j < input.Count()-1 ;j++)
                    {
                        if (input[j].StartsWith("dir "))
                        {
                            currentDirectoryName = input[j].Split(" ").Last();
                            currentDirectory.Childs.Add(new ElfDirectory(currentDirectoryName, currentDirectory));
                        }
                        else if (char.IsDigit(input[j][0]))
                        {
                            String[] fileInfo = input[j].Split(" "); //0:Size ; 1:Name
                            currentDirectory.Files.Add(new ElfFile(name: fileInfo[1], size: Int32.Parse(fileInfo[0])));
                        }
                    }
                    i = j - 1;
                }
            }     
            return rootDirectory.SizeWhichCanBeDeleted();
        }
        class ElfDirectory
        {
            public string Name { get; set; }              // /                a           e
            public List<ElfFile> Files { get; set; }         // b.txt, c.dat     f,g,h.lst   i
            public ElfDirectory Parent { get; set; }         // null             /           a
            public List<ElfDirectory> Childs { get; set; }   // a, d             e           null
        
            public ElfDirectory(string name, ElfDirectory parent)
            {
                Name = name;
                Files = new List<ElfFile>();
                Parent = parent;
                Childs = new List<ElfDirectory>();
            }

            public int SizeWhichCanBeDeleted()
            {
                int result = 0;
                if(this.Files.Count > 0)
                {
                    foreach (var file in this.Files)
                    {
                        result += file.Size;
                    }
                }    

                if (this.Childs.Count > 0)
                {
                    foreach (var child in this.Childs)
                    {
                        result += child.SizeWhichCanBeDeleted();
                    }
                }

                if (result <= 100000)
                    return result;
                else
                    return 0;                
            }
        }

        class ElfFile
        {
            public string Name { get; set;}
            public int Size { get; set; }

            public ElfFile(string name, int size) 
            {
                Name= name;
                Size = size;
            }
        }
    }
}
