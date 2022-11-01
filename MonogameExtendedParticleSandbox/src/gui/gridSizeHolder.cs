using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class GridSizeHolder
    {
        public GridSizeHolder()
        {
            RowCount = 0;
            ColumnCount = 0;
        }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
    }
}
