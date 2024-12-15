using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerSudoku;
public class Cage
{
    public int Sum { get; set; }
    public List<(int, int)> Cells { get; set; }

    public Cage(int sum, List<(int, int)> cells)
    {
        Sum = sum;
        Cells = cells;
    }
}

