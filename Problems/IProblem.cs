namespace AdventOfCode.Problems
{
    public interface IProblem<T, U>
    {
        public T DoPartA();

        public U DoPartB();
    }
}
