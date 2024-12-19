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


            for (int j = 0; j < numTables; j++)
            {
                List<ILiteral> tableOccupants = new List<ILiteral>();
                for (int i = 0; i < numGuests; i++)
                {
                    tableOccupants.Add(x[i, j]);
                }
                model.AddLinearConstraint(LinearExpr.Sum(tableOccupants), 0, seatsPerTable);
            }


            foreach (var pair in cannotSitTogether)
            {
                int guestA = pair.Item1 - 1;
                int guestB = pair.Item2 - 1;
                for (int j = 0; j < numTables; j++)
                {
                    model.AddBoolOr(new ILiteral[] { x[guestA, j].Not(), x[guestB, j].Not() });
                }
            }

            foreach (var pair in mustSitTogether)
            {
                int guestA = pair.Item1 - 1;
                int guestB = pair.Item2 - 1;
                for (int j = 0; j < numTables; j++)
                {
                    model.AddImplication(x[guestA, j], x[guestB, j]);
                    model.AddImplication(x[guestB, j], x[guestA, j]);
                }
            }

            CpSolver solver = new CpSolver();
            CpSolverStatus status = solver.Solve(model);






        }


    }
}

