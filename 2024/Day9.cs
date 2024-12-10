using MoreLinq;

namespace _2024
{
    public class Day9:abstractPuzzleClass
    {
        public Day9() : base(9,2024)
        {
            
        }

        public override string SolvePart1(string input = null)
        {
            int[] memory = ToMemoryPart1(input);
            int freeIndex = Array.IndexOf(memory, int.MinValue);
            int filledIndex = Array.FindLastIndex(memory, x => x != int.MinValue);
            if (freeIndex != -1 && filledIndex != -1)
            {
                while (freeIndex < filledIndex)
                {
                    memory[freeIndex] = memory[filledIndex];
                    memory[filledIndex] = int.MinValue;
                    freeIndex = Array.IndexOf(memory, int.MinValue, freeIndex);
                    filledIndex = Array.FindLastIndex(memory, filledIndex, x => x != int.MinValue);
                }
            }
            return CalculateChecksum(memory).ToString();
        }

        public override string SolvePart2(string input = null)
        {
            (List<Block> filledBlocks, List<Block> emptyBlocks) = ToMemoryPart2(input);
            Queue<Block> blocksToMove = new Queue<Block>(filledBlocks.OrderByDescending(x => x.Index));
            while (blocksToMove.TryDequeue(out Block? current))
            {
                int destinationIndex = emptyBlocks.FindIndex(x => x.Size >= current.Size && x.Index < current.Index);
                if (destinationIndex == -1)
                {
                    continue;
                }
                Block destination = emptyBlocks[destinationIndex];
                emptyBlocks.RemoveAt(destinationIndex);
                current.Index = destination.Index;
                if (destination.Size > current.Size)
                {
                    int index = destination.Index + current.Size;
                    emptyBlocks.Insert(destinationIndex, new(int.MinValue, destination.Size - current.Size, index));
                }
            }
            return CalculateChecksum(filledBlocks).ToString();
        }

        private static long CalculateChecksum(int[] memory)
        {
            int[] filledBlocks = memory.Where(x => x != int.MinValue).ToArray();
            return Enumerable.Range(0, filledBlocks.Length).Aggregate(0L, (a, i) => a + i * memory[i]);
        }

        private static long CalculateChecksum(List<Block> blocks)
        {
            long checksum = 0L;
            Block[] sorted = blocks.OrderBy(x => x.Index).ToArray();
            foreach (Block? block in sorted)
            {
                for (int i = 0; i < block.Size; i++)
                {
                    checksum += (block.Index + i) * block.Id;
                }
            }
            return checksum;
        }

        public override void Tests()
        {
            Debug.Assert(SolvePart1("2333133121414131402") == "1928");
            Debug.Assert(SolvePart2("2333133121414131402") == "2858");
        }

        private int[] ToMemoryPart1(string diskMap)
        {
            List<int> memory = new List<int>(diskMap.Length * 5);
            bool freeSpace = false;
            int id = 0;
            for (int i = 0; i < diskMap.Length; i++)
            {
                int size = diskMap[i] - '0';
                int blockId = freeSpace ? int.MinValue : id++;
                for (int j = 0; j < size; j++)
                {
                    memory.Add(blockId);
                }
                freeSpace = !freeSpace;
            }
            return [.. memory];
        }

        private (List<Block> FilledBlocks, List<Block> FreeBlocks) ToMemoryPart2(string diskMap)
        {
            List<Block> filledBlocks = new List<Block>(diskMap.Length / 2);
            List<Block> emptyBlocks = new List<Block>(diskMap.Length / 2);
            bool freeSpace = false;
            int index = 0;
            int id = 0;
            for (int i = 0; i < diskMap.Length; i++)
            {
                int size = diskMap[i] - '0';
                if (size > 0)
                {
                    int blockId = freeSpace ? int.MinValue : id++;
                    (freeSpace ? emptyBlocks : filledBlocks).Add(new Block(blockId, size, index));
                    index += size;
                }
                freeSpace = !freeSpace;
            }
            return (filledBlocks, emptyBlocks);
        }

        private class Block(int id, int size, int index)
        {
            public int Id => id;
            public int Size => size;
            public int Index { get; set; } = index;
        }
    }
}