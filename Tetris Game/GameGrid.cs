using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Game
{
    public class GameGrid
    {
        // 2-D rectangular array to reprsent game grid
        private readonly int[,] grid;

        // rows property
        public int Rows { get; }
        //column property
        public int Columns { get; }

        // defining indexer to provide easy access to grid array
        public int this [int row, int column] 
        { 
            get => grid [row, column];
            set => grid [row, column] = value;
        }
    }
}
