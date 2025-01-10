using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Game
{
    public class JBlock : Block
    {
        private readonly Position[][] occupTiles = new Position[][] // array representing all possible position of the J-block
       {
            new Position[] { new(0,0), new(0,1), new(1,1) ,new(1,2) },
            new Position[] { new(0,1), new(0,2), new(1,1) ,new(2,1) },
            new Position[] { new(1,0), new(1,1), new(1,2) ,new(2,2) },
            new Position[] { new(0,1), new(1,1), new(2,0) ,new(2,1) }
       };

        public override int blockId => 2; //set J-block ID to 2
        protected override Position StartOffset => new Position(0, 3); //spawns new block in the middle of top row
        protected override Position[][] Tiles => occupTiles; //overrides Tiles property
    }
}
