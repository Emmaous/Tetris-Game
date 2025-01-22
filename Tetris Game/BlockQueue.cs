using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Game
{
    public class BlockQueue
    {
        // array containing instance of all block sub-classes
        public readonly Block[] blocks = new Block[]
        {
            new IBlock(), new JBlock(), new LBlock(), new OBlock(), new SBlock(), new TBlock(), new ZBlock()
        };

        private readonly Random random = new Random();
        //property to represent next block in queue
        public Block NewBlock {  get; private set; }
        
        //constructor
        public BlockQueue()
        {
            NewBlock = RandomBlock();
        }
        
        //method to return a random block
        private Block RandomBlock ()
        {
            return blocks[random.Next(blocks.Length)];
        }

        //method to return new block and update property
        public Block GetAndUpdate()
        {
            Block block = NewBlock;

            do //loop till a different block
            {
                NewBlock = RandomBlock();
            }
            while (block.BlockId == NewBlock.BlockId);

            return block;
        }

    }
}
