namespace calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter math equation:");
            string equation = Console.ReadLine();
            RPNSolver solver = new(equation);
            float result = solver.Solve();
            Console.WriteLine($"Result: {result}");
        }
    }
}
