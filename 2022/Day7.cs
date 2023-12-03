using System.Diagnostics;

namespace _2022
{
	public class Day7 : General.PuzzleWithObjectInput<(Day7.folder rootFolder, List<Day7.folder> AllFolders )>
    {
        public Day7() : base(7,2022)
        {
        }

        protected override (folder rootFolder, List<folder> AllFolders) CastToObject(string rawData)
        {
            List<folder> folders = new List<folder>();
            folder root = new folder(@"/", null);
            folders.Add(root);
            folder currentDirectory = null;


            string[] input = rawData.Split(Environment.NewLine);
            for (int i = 0; i < input.Length; i++)
            {
                var parts = input[i].Split(" ");
                switch (parts[0])
                {
                    case "$":
                        switch (parts[1])
                        {
                            case "cd":
                                if (parts[2] == "..")
                                {
                                    if (currentDirectory.Name == "/") continue;
                                    currentDirectory = currentDirectory.Parent;
                                }
                                else if (parts[2] == "/")
                                {
                                    currentDirectory = root;
                                }
                                else
                                {
                                    currentDirectory = (folder)currentDirectory.childs.Where(x => x.Name == parts[2]).First();

                                }
                                break;
                            case "ls":
                                break;
                            default:
                                break;
                        }
                        break;
                    case "dir":
                        folder newFolder = new folder(parts[1], currentDirectory);
                        currentDirectory.childs.Add(newFolder);
                        folders.Add(newFolder);
                        break;
                    default:
                        file newfile = new file(long.Parse(parts[0]), parts[1]);
                        currentDirectory.childs.Add(newfile);
                        break;
                }
            }

            return (root, folders);
        }


        public override string SolvePart1((folder rootFolder, List<folder> AllFolders) input)
        {
            return input.AllFolders.Where(x => x.Size <= 100000).Sum(x => x.Size).ToString();
        }


        public override string SolvePart2((folder rootFolder, List<folder> AllFolders) input)
        {
            long available = 70000000 - input.rootFolder.Size;
            long required = 30000000 - available;

            folder bestMatch = input.AllFolders.Where(x => x.Size >= required).OrderBy(x => x.Size).First();
            return bestMatch.Size.ToString();
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1(@"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k") == "95437");

            Debug.Assert(SolvePart2(@"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k") == "24933642");
        }


        public abstract class Item
        {
            public abstract long Size { get; }

            public string Name { get; protected set; }
        }

        public class file : Item 
        {
            public file (long size, string name )
            {
                Size = size;
                Name = name;
            }

            public override long Size { get; }
            
        }


        public class folder : Item
        {
            public List<Item> childs;
            public folder(string name, folder parent)
            {
                childs = new List<Item>();
                Name = name;
                Parent = parent;
            }

            public folder Parent { get; }

            public override long Size
            {
                get
                {
                    return childs.Sum(x => x.Size);
                }
            }


        }
    }
}
