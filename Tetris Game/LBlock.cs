using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Game
{
    public class LBlock : Block
    {
        private readonly Position[][] occupTiles = new Position[][] // array representing all possible position of the L-block
        {
            new Position[] { new(0,2), new(1,0), new(1,1) ,new(1,2) },
            new Position[] { new(0,1), new(1,1) ,new(2,1), new(2,2) },
            new Position[] { new(1,0), new(1,1), new(1,2) ,new(2,0) },
            new Position[] { new(0,0), new(0,1), new(1,1) ,new(2,1) }
        };

        public override int blockId => 3; //set L-block ID to 3
        protected override Position StartOffset => new Position(0, 3); //spawns new block in the middle of top row
        protected override Position[][] Tiles => occupTiles; //overrides Tiles property
    }
}
