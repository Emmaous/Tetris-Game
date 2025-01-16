using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Game
{
    //class to handle the game status
    public class GameState
    {
        private Block currentBlock;

        public Block CurrentBlock 
        { 
            get => currentBlock;
            private set 
            { 
                currentBlock = value;
                currentBlock.Reset();
            } 
        }

        public GameGrid Grid { get; }
        public BlockQueue Queue { get; }
        public bool GameOver { get; private set; }

        //constructor
        public GameState() 
        {
            Grid = new GameGrid(22, 10);
            Queue = new BlockQueue();
            CurrentBlock = Queue.GetAndUpdate();
        }

        //method to check if block fits in space
        private bool BlockFitsSpace()
        {
            foreach (Position p in CurrentBlock.TilePosition())
            {
                if(!Grid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }
            return true;
        }

        //method to rotate block clockwise
        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();

            if (!BlockFitsSpace())
                CurrentBlock.RotateCCW();
        }

        //method to rotate block counter clockwise
        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();

            if (!BlockFitsSpace())
                CurrentBlock.RotateCW();
        }

        //method to move block left
        public void MoveBlockLeft()
        {
            CurrentBlock.MoveBlock(0, -1);
            if (!BlockFitsSpace())
                CurrentBlock.MoveBlock(0, 1);
        }

        //method to move block right
        public void MoveBlockRight()
        {
            CurrentBlock.MoveBlock(0, 1);
            if (!BlockFitsSpace())
                CurrentBlock.MoveBlock(0, -1);
        }

        //method to check if the game is over
        private bool IsGameOver()
        {
            return !(Grid.IsRowEmpty(0) && Grid.IsRowEmpty(1));
        }

        // method to place the current block
        private void PlaceBlock()
        {
            foreach (Position p in CurrentBlock.TilePosition())
            {
                Grid[p.Row, p.Column] = CurrentBlock.blockId;
            }

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = Queue.GetAndUpdate();
            }
        }

        public void MoveBlockDown()
        {
            CurrentBlock.MoveBlock(1, 0);

            if (!BlockFitsSpace())
            {
                CurrentBlock.MoveBlock(-1, 0);
                PlaceBlock();
            }

        }
    }
}
