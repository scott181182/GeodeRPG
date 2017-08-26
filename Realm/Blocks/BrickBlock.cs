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

namespace SuperMarioYeah.Realm.Blocks
{
    public class BrickBlock : Block
    {
        public BrickBlock(int id) : base(id, new StaticSprite(Textures.BlockBrick)) {  }

        public override void OnCollision(int x, int y, ICollidable obj, Direction dir, World world)
        {
            base.OnCollision(x, y, obj, dir, world);

            if (dir == Direction.Down && obj is Mario && (obj as Mario).State > Mario.MarioState.Small)
            {
                world.AddEntity(new BrokenBlockParticle(x, y));
                world.AddEntity(new BrokenBlockParticle(x, y));
                world.AddEntity(new BrokenBlockParticle(x, y));
                world.AddEntity(new BrokenBlockParticle(x, y));

                world.SetTileAt(x, y, 0);

                Sounds.SoundLoader.BreakBlock(new Vector2(x + 0.5f, y + 0.5f));
            }
            else if (obj is SuperMarioYeah.Projectiles.Bullet)
            {
                world.AddEntity(new BrokenBlockParticle(x, y));
                world.AddEntity(new BrokenBlockParticle(x, y));
                world.AddEntity(new BrokenBlockParticle(x, y));
                world.AddEntity(new BrokenBlockParticle(x, y));

                world.SetTileAt(x, y, 0);

                Sounds.SoundLoader.BreakBlock(new Vector2(x + 0.5f, y + 0.5f));
            }
        }
    }
}
