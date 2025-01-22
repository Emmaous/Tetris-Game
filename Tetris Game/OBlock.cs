using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Game
{
    public class OBlock : Block
    {
        private readonly Position[][] occupTiles = new Position[][] // array representing all possible position of the O-block
        {
            new Position[] { new(0,0), new(0,1), new(1,0) ,new(1,1) }
        };

        public override int BlockId => 4; //set O-block ID to 4
        protected override Position StartOffset => new Position(0, 4); //new block spawn positon
        protected override Position[][] Tiles => occupTiles; //overrides Tiles property
    }
}
