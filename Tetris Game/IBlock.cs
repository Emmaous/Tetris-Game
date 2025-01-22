using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Game
{
    public class IBlock : Block
    {
        private readonly Position[][] occupTiles = new Position[][] // array representing all possible position of the I-block
        {
            new Position[] { new(1,0), new(1,1), new(1,2) ,new(1,3) },
            new Position[] { new(0,2), new(1,2), new(2,2) ,new(3,2) },
            new Position[] { new(2,0), new(2,1), new(2,2) ,new(2,3) },
            new Position[] { new(0,1), new(1,1), new(2,1) ,new(3,1) }
        };

        public override int BlockId => 1; //set I-block ID to 1
        protected override Position StartOffset => new Position(-1,3); //spawns new block in the middle of top row
        protected override Position[][] Tiles => occupTiles; //overrides Tiles property

    }
}
