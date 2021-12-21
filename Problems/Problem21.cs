using AdventOfCode_2021.Problems.Model.Problem21;

namespace AdventOfCode_2021.Problems
{
    public class Problem21 : IProblem<int, long>
    {
        public int DoPartA()
        {
            var lines = Utils.InputToStringArray("21").Select(x => Convert.ToInt32(x.Split(": ").Last()));

            Player[] players = new Player[] {
                new Player() { Position = lines.First() },
                new Player() { Position = lines.Last() }
            };

            int die = 0;
            int step = 0;

            while (players.All(player => player.Score < 1000))
            {
                var player = step % 2 == 0 ? players.First() : players.Last();

                die += 3;

                if (die > 100)
                    die -= 100;

                var moves = (die - 1) * 3 % 10;

                player.Position = (player.Position + moves) % 10;
                if (player.Position == 0)
                    player.Position = 10;

                player.Score += player.Position;

                step++;
            }

            return step * 3 * players.Where(player => player.Score < 1000).Single().Score;
        }

        public long DoPartB()
        {
            var lines = Utils.InputToStringArray("21").Select(x => Convert.ToInt32(x.Split(": ").Last()));

            Player[] players = new Player[] {
                new Player() { Position = lines.First() },
                new Player() { Position = lines.Last() }
            };

            return 0;
        }
    }
}
