using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Sprites;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Particles;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Collectable;

namespace SuperMarioYeah.Realm.Blocks
{
    public class MysteryPowerUpBlock : Block
    {
        private Random rand = new Random();

        public MysteryPowerUpBlock(int id) : base(id, new AnimatedSprite(Textures.BlockMystery, rows: 4, cols: 1, frameTime: 8)) {  }

        private IPowerUp RandomPowerUp(Mario mario, Vector2 position)
        {
            if(mario.State == Mario.MarioState.Small) { return new Mushroom(position); }
            else
            {
                double r = rand.NextDouble();

                if(r < 0.33) { return new Fireflower(position); }
                else if (r < 0.67) { return new Gun(position); }
                else { return new TNTPowerUp(position);  }
            }
        }

        public override void OnCollision(int x, int y, ICollidable obj, Direction dir, World world)
        {
            base.OnCollision(x, y, obj, dir, world);

            if (dir == Direction.Down && obj is Mario)
            {
                world.AddEntity(RandomPowerUp(obj as Mario, new Vector2(x, y + 1)));
                world.SetTileAt(x, y, Block.usedBlock);

                Sounds.SoundLoader.PowerupAppears(new Vector2(x, y + 1));
            }
        }
    }
}
