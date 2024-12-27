using System;
using SATProblemNamespace;

class Program
{
    static void Main(string[] args)
    {
        int numGuests = 100;
        int numTables = 10;
        int seatsPerTable = 10;


        var problem = new SATProblem(numGuests, numTables, seatsPerTable);

        Console.WriteLine("Jepni numrin e çifteve të mysafirëve që NUK mund të ulen së bashku:");
        int notTogetherCount = int.Parse(Console.ReadLine() ?? "0");
        for (int i = 0; i < notTogetherCount; i++)
        {
            Console.Write($"Çifti {i + 1} (jepni si format M1 M2): ");
            var input = Console.ReadLine()?.Split() ?? new string[0];
            if (input.Length < 2) continue;

            int guestA = int.Parse(input[0][1..]);
            int guestB = int.Parse(input[1][1..]);
            problem.AddCannotSitTogether(guestA, guestB);
        }


        Console.WriteLine("Jepni numrin e çifteve të mysafirëve që DUHET të ulen së bashku:");
        int togetherCount = int.Parse(Console.ReadLine() ?? "0");
        for (int i = 0; i < togetherCount; i++)
        {
            Console.Write($"Çifti {i + 1} (jepni si format M1 M2): ");
            var input = Console.ReadLine()?.Split() ?? new string[0];
            if (input.Length < 2) continue;

            int guestA = int.Parse(input[0][1..]);
            int guestB = int.Parse(input[1][1..]);
            problem.AddMustSitTogether(guestA, guestB);
        }

        problem.Solve();

    }
}

