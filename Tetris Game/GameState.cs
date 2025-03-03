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

                for (int i = 0; i < 2; i++)
                {
                    currentBlock.MoveBlock(1, 0);
                    if (!BlockFitsSpace())
                    {
                        currentBlock.MoveBlock(-1, 0);
                    }
                }
            } 
        }

        public GameGrid Grid { get; }
        public BlockQueue Queue { get; }
        public bool GameOver { get; private set; }
        public int GameScore { get; private set; }
        public Block HeldBlock { get; private set; }

        //bool for if a block is being held
        public bool Holding { get; private set; }

        //constructor
        public GameState() 
        {
            Grid = new GameGrid(22, 10);
            Queue = new BlockQueue();
            CurrentBlock = Queue.GetAndUpdate();
            Holding = true;
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

        //method to hold a block
        public void HoldBlock()
        {
            Block temp;

            if (!Holding)
            {
                return;
            }

            if (HeldBlock == null)
            {
                HeldBlock = CurrentBlock;
                CurrentBlock = Queue.GetAndUpdate();
            } 
            else
            {
                temp = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = temp;
            }

            Holding = false;
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

        //method to move the block down
        public void MoveBlockDown()
        {
            CurrentBlock.MoveBlock(1, 0);

            if (!BlockFitsSpace())
            {
                CurrentBlock.MoveBlock(-1, 0);
                PlaceBlock();
            }

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
                Grid[p.Row, p.Column] = CurrentBlock.BlockId;
            }

            GameScore += Grid.ClearCompleteRows();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = Queue.GetAndUpdate();
                Holding = true;
            }
        }

        //method to check drop distance for each tile
        private int TileDropDistance(Position p)
        {
            int drop = 0;

            while(Grid.IsEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }

            return drop;
        }

        //method to check minimum block drop distance
        public int BlockDropDistance()
        {
            int drop = Grid.Rows;

            foreach (Position p in CurrentBlock.TilePosition())
            {
                drop = System.Math.Min(drop, TileDropDistance(p));
            }

            return drop;
        }

        public void DropBlock()
        {
            CurrentBlock.MoveBlock(BlockDropDistance(), 0);
            PlaceBlock();
        }
    }
}
