using System;
using System.Collections.Generic;
using Google.OrTools.Sat;

namespace SATProblemNamespace
{
    public class SATProblem
    {
        private int numGuests;
        private int numTables;
        private int seatsPerTable;
        private List<(int, int)> cannotSitTogether;
        private List<(int, int)> mustSitTogether;

        public SATProblem(int numGuests, int numTables, int seatsPerTable)
        {
            this.numGuests = numGuests;
            this.numTables = numTables;
            this.seatsPerTable = seatsPerTable;
            this.cannotSitTogether = new List<(int, int)>();
            this.mustSitTogether = new List<(int, int)>();
        }
        public void AddCannotSitTogether(int guestA, int guestB)
        {
            cannotSitTogether.Add((guestA, guestB));
        }

        public void AddMustSitTogether(int guestA, int guestB)
        {
            mustSitTogether.Add((guestA, guestB));
        }

        public void Solve()
        {
            CpModel model = new CpModel();


            BoolVar[,] x = new BoolVar[numGuests, numTables];
            for (int i = 0; i < numGuests; i++)
            {
                for (int j = 0; j < numTables; j++)
                {
                    x[i, j] = model.NewBoolVar($"x[{i},{j}]");
                }
            }

            for (int i = 0; i < numGuests; i++)
            {
                List<ILiteral> tableAssignments = new List<ILiteral>();
                for (int j = 0; j < numTables; j++)
                {
                    tableAssignments.Add(x[i, j]);
                }
                model.AddExactlyOne(tableAssignments);
            }





        }


    }
}

