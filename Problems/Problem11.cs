using AdventOfCode_2021.Problems.Model.Problem11;

namespace AdventOfCode_2021.Problems
{
    public class Problem11 : IProblem<int, int>
    {
        public int DoPartA()
        {
            var lines = Utils.InputToStringArray("11").ToArray();
            const int steps = 100;
            int result = 0;

            List<((int, int) Position, Octopus Octopus)> Octos = new();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    Octos.Add(((i, j), new Octopus() { EnergyLevel = Convert.ToInt32(lines[i][j].ToString()) }));
                }
            }

            for (int i = 0; i < steps; i++)
            {
                foreach (var octo in Octos)
                {
                    octo.Octopus.EnergyLevel++;
                }

                while (Octos.Any(x => x.Octopus.EnergyLevel > 9 && !x.Octopus.Flashed))
                {
                    var overLoadingOctos = Octos.Where(x => x.Octopus.EnergyLevel > 9 && !x.Octopus.Flashed).ToList();

                    foreach (var octo in overLoadingOctos)
                    {
                        octo.Octopus.Flash();
                        result++;
                        var flashingOctoPosition = octo.Position;

                        // Top
                        if (flashingOctoPosition.Item1 - 1 >= 0)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1 - 1, flashingOctoPosition.Item2) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // Bottom
                        if (flashingOctoPosition.Item1 + 1 < lines.Length)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1 + 1, flashingOctoPosition.Item2) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // Left
                        if (flashingOctoPosition.Item2 - 1 >= 0)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1, flashingOctoPosition.Item2 - 1) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // Right
                        if (flashingOctoPosition.Item2 + 1 < lines[0].Length)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1, flashingOctoPosition.Item2 + 1) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // TopLeft
                        if (flashingOctoPosition.Item1 - 1 >= 0 && flashingOctoPosition.Item2 - 1 >= 0)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1 - 1, flashingOctoPosition.Item2 - 1) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // TopRight
                        if (flashingOctoPosition.Item1 - 1 >= 0 && flashingOctoPosition.Item2 + 1 < lines[0].Length)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1 - 1, flashingOctoPosition.Item2 + 1) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // BottomLeft
                        if (flashingOctoPosition.Item1 + 1 < lines.Length && flashingOctoPosition.Item2 - 1 >= 0)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1 + 1, flashingOctoPosition.Item2 - 1) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // BottomRight
                        if (flashingOctoPosition.Item1 + 1 < lines.Length && flashingOctoPosition.Item2 + 1 < lines[0].Length)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1 + 1, flashingOctoPosition.Item2 + 1) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }
                    }
                }

                foreach (var octo in Octos)
                {
                    octo.Octopus.Flashed = false;
                }
            }

            return result;
        }

        public int DoPartB()
        {
            var lines = Utils.InputToStringArray("11").ToArray();
            int step = 0;
            int result = 0;

            List<((int, int) Position, Octopus Octopus)> Octos = new();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    Octos.Add(((i, j), new Octopus() { EnergyLevel = Convert.ToInt32(lines[i][j].ToString()) }));
                }
            }

            while(result != 100)
            {
                result = 0;

                foreach (var octo in Octos)
                {
                    octo.Octopus.EnergyLevel++;
                }

                while (Octos.Any(x => x.Octopus.EnergyLevel > 9 && !x.Octopus.Flashed))
                {
                    var overLoadingOctos = Octos.Where(x => x.Octopus.EnergyLevel > 9 && !x.Octopus.Flashed).ToList();

                    foreach (var octo in overLoadingOctos)
                    {
                        octo.Octopus.Flash();
                        result++;
                        var flashingOctoPosition = octo.Position;

                        // Top
                        if (flashingOctoPosition.Item1 - 1 >= 0)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1 - 1, flashingOctoPosition.Item2) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // Bottom
                        if (flashingOctoPosition.Item1 + 1 < lines.Length)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1 + 1, flashingOctoPosition.Item2) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // Left
                        if (flashingOctoPosition.Item2 - 1 >= 0)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1, flashingOctoPosition.Item2 - 1) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // Right
                        if (flashingOctoPosition.Item2 + 1 < lines[0].Length)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1, flashingOctoPosition.Item2 + 1) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // TopLeft
                        if (flashingOctoPosition.Item1 - 1 >= 0 && flashingOctoPosition.Item2 - 1 >= 0)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1 - 1, flashingOctoPosition.Item2 - 1) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // TopRight
                        if (flashingOctoPosition.Item1 - 1 >= 0 && flashingOctoPosition.Item2 + 1 < lines[0].Length)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1 - 1, flashingOctoPosition.Item2 + 1) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // BottomLeft
                        if (flashingOctoPosition.Item1 + 1 < lines.Length && flashingOctoPosition.Item2 - 1 >= 0)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1 + 1, flashingOctoPosition.Item2 - 1) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }

                        // BottomRight
                        if (flashingOctoPosition.Item1 + 1 < lines.Length && flashingOctoPosition.Item2 + 1 < lines[0].Length)
                        {
                            var neighbourOcto = Octos.Where(x => x.Position == (flashingOctoPosition.Item1 + 1, flashingOctoPosition.Item2 + 1) && !x.Octopus.Flashed).SingleOrDefault();
                            if (neighbourOcto.Octopus != null)
                                neighbourOcto.Octopus.EnergyLevel++;
                        }
                    }
                }

                step++;

                foreach (var octo in Octos)
                {
                    octo.Octopus.Flashed = false;
                }
            }

            return step;
        }
    }
}
