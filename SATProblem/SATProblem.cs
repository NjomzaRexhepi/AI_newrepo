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


    }
}

