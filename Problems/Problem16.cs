namespace AdventOfCode_2021.Problems
{
    public class Problem16 : IProblem<int, int>
    {
        public int DoPartA()
        {
            var line = Utils.InputToStringArray("16_mini").Single();

            string binarystring = string.Join(string.Empty, line.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));


            // 8A004A801A8002F478
            // 100 010 1 00000000001 001 010 1 00000000001 101 010 0 000000000001011 110 100 01111 000
            //  V4   2 1           1  V1   2 1           1  V5   2 0              11  V6   4    15 ---

            // 620080001611562C8802118E34
            // 011 000 1 00000000010 00000000000000000101 100 001000101010110001011001000100000000010000100011000111000110100
            //  V3   0 1           2                   V5   4     ?????????????????????????????????        


            return 0;
        }

        public int DoPartB()
        {
            return 0;
        }
    }
}
