namespace AdventOfCode_2021.Problems
{
    public interface IProblem<T, U>
    {
        public T DoPartA();

        public U DoPartB();
    }
}
