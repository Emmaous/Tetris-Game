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

        //constructor
        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[Rows, Columns];
        }

        //Convenience Methods
        //check for presence of a given row or column
        public bool IsInside(int row, int column)
        {
            return row >= 0 && row < Rows && column >= 0 && column < Columns;
        }

        //check that a given cell is empty
        public bool IsEmpty(int row, int column)
        {
            return IsInside(row, column) && grid[row, column] == 0;
        }

        //check if row is full
        public bool IsRowFull(int row)
        {
            for (int column = 0; column < Columns; column++)
            {
                if (grid[row, column] == 0) return false;
            }

            return true;
        }

        //check if row is empty
        public bool IsRowEmpty(int row)
        {
            for (int column = 0; column < Columns; column++)
            {
                if (grid[row, column] != 0) return false;
            }

            return true;
        }

        //method to clear row
        private void ClearRow(int row)
        {
            for (int column = 0; column < Columns; column++)
            {
                grid[row, column] = 0;
            }
        }

        //method to move rows down
        private void MoveDown(int row, int numOfRows)
        {
            for (int column = 0; column < Columns; column++)
            {
                grid[row + numOfRows, column] = grid[row, column];
                grid[row, column] = 0;
            }
        }

        //method to clear complete rows
        private int ClearCompleteRows()
        {
            int clearedRows = 0;

            for (int row = Rows - 1; row >= 0; row--)
            {
                if (IsRowFull(row))
                {
                    ClearRow(row);
                    clearedRows++;
                }
                else if (clearedRows>0)
                {
                    MoveDown(row, clearedRows);
                }
            }
            return clearedRows;
        }
    }
}
