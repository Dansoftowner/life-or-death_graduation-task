using System;

namespace LifeOrDeathCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var lod = new LifeOrDeathSimulation(10, 10);
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                lod.Run();
            }
            Console.ReadKey();
        }
    }
}
