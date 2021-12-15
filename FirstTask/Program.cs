using System;


namespace FirstTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                Console.WriteLine(args[0].MaxCountOfUniqueSequentialCharacters());
            }
        }
    }
}
