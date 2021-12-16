namespace AdventOfCode_2021.Problems
{
    public class Problem16 : IProblem<int, int>
    {
        public int DoPartA()
        {
            var line = Utils.InputToStringArray("16").Single();

            string binarystring = string.Join(string.Empty, line.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            // 8A004A801A8002F478
            // 100 010 1 00000000001 001 010 1 00000000001 101 010 0 000000000001011 110 100 01111 000
            //  V4   2 1           1  V1   2 1           1  V5   2 0              11  V6   4    15 ---

            // 620080001611562C8802118E34
            // 011 000 1 00000000010 000 000 0 000000000010110 000 100 01010 101 100 01011 001 000 1 00000000010 000 100 01100 011 100 01101 00
            //  V3   0 1           2  V0   0 0              22  V0   4    10  V5   4    11  V1   0 1           2  V0   4    12  V3   4    13 --

            // C0015000016115A2E0802F182340
            // 110 000 0 000000001010100 000 000 0 000000000010110 000 100 01010 110 100 01011 100 000 1 00000000010 111 100 01100 000 100 01101 000000
            //  V6   0 0              84  V0   0 0              22  V0   4    10  V6   4    11  V4   0 1           2  V7   4    12  V0  4     13 ------        

            // A0016C880162017C3686B18A3D4780
            // 101 000 0 000000001011011 001 000 1 00000000001 011 000 1 00000000101 111 100 00110 110 100 00110 101 100 01100 010 100 01111 010 100 01111 0000000
            //  V5                        V1                    V3                    V7            V6            V5            V2            V2           -------

            return ProcessBinaryString(binarystring);
        }

        public int DoPartB()
        {
            return 0;
        }

        private int ProcessBinaryString(string binarystring)
        {
            var version = 0;

            if (binarystring.Length > 7)
            {
                version = Convert.ToInt32(binarystring.Substring(0, 3), 2);
                var typeId = Convert.ToInt32(binarystring.Substring(3, 3), 2);

                if (typeId == 4)
                {
                    var zeroPosition = 6;

                    while (Convert.ToInt32(binarystring.Substring(zeroPosition, 1), 2) != 0)
                        zeroPosition += 5;

                    zeroPosition += 5;

                    version += ProcessBinaryString(binarystring.Substring(zeroPosition));
                }
                else
                {
                    var lengthTypeId = Convert.ToInt32(binarystring.Substring(6, 1), 2);

                    version += ProcessBinaryString(binarystring.Substring(lengthTypeId == 1 ? 18 : 22));
                }
            }

            return version;
        }
    }
}
