using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Game
{
    public abstract class Block
    {
        // 2-D positional array to represent the block positions during rotation
        protected abstract Position[][] Tiles { get; }
        //block spawn offset
        protected abstract Position StartOffset { get; }
        //ID for distiguishing blocks
        public abstract int blockId { get; }

        //variables to handle the block roatation and offset state
        private int rotationState;
        private Position offsetState;

        //constructor
        public Block()
        {
            offsetState = new Position(StartOffset.Row, StartOffset.Column);
        }

        //method to return the grid position occupied by the block
        public IEnumerable<Position> TilePosition()
        {
            // loop to check the tile positions in current rotation
            foreach (Position p in Tiles[rotationState])
            {
                //adds the offset to the current positions
                yield return new Position(p.Row + offsetState.Row, p.Column+ offsetState.Column);
            }
        }

        //method to rotate black 90deg clockwise
        public void RotateCW()
        {
            rotationState = (rotationState+1)%Tiles.Length;
        }

        //method to rotate black 90deg counterclockwise
        public void RotateCCW()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }

        //method to move blocks
        public void MoveBlock(int row,  int column)
        {
            offsetState.Row += row;
            offsetState.Column += column;
        }

        //method to reset position & rotation
        public void Reset()
        {
            rotationState = 0;
            offsetState.Row = StartOffset.Row;
            offsetState.Column = StartOffset.Column;
        }
    }
}
