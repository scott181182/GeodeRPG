using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SuperMarioYeah;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Sprites;
using SuperMarioYeah.Projectiles;
using SuperMarioYeah.Realm.TileEntity;
using SuperMarioYeah.Collectable;

namespace SuperMarioYeah.Realm.Blocks
{
    public abstract class Block : IDrawable, IBlockCollidable
    {
        private static Block[] BLOCKS = new Block[32];

        public static readonly Block brickBlock          = new BrickBlock         (1);
        public static readonly Block platformBlock       = new PlatformBlock      (2);
        public static readonly Block mysteryCoinBlock    = new MysteryCoinBlock   (3);
        public static readonly Block mysteryPowerUpBlock = new MysteryPowerUpBlock(4);
        public static readonly Block usedBlock           = new UsedBlock          (5);
        public static readonly Block crackedBlock        = new CrackedBlock       (6);
        public static readonly Block hiddenBlock         = new HiddenBlock        (7);
        public static readonly Block coinBrickBlock      = new CoinBrickBlock     (8);
        public static readonly Block starBrickBlock      = new StarBrickBlock     (9);

        public static readonly Block pipeUpLeftTop  = new PipeBlock(16, new StaticSprite(Textures.PipeUpLeftTop));
        public static readonly Block pipeUpRightTop = new PipeBlock(17, new StaticSprite(Textures.PipeUpRightTop));
        public static readonly Block pipeUpLeft     = new PipeBlock(18, new StaticSprite(Textures.PipeUpLeft));
        public static readonly Block pipeUpRight    = new PipeBlock(19, new StaticSprite(Textures.PipeUpRight));

        public static readonly Block pipeSideTop     = new PipeBlock(20, new StaticSprite(Textures.PipeSideTop));
        public static readonly Block pipeSideBottom  = new PipeBlock(21, new StaticSprite(Textures.PipeSideBottom));
        public static readonly Block pipeLeftTop     = new PipeBlock(22, new StaticSprite(Textures.PipeLeftTop));
        public static readonly Block pipeLeftBottom  = new PipeBlock(23, new StaticSprite(Textures.PipeLeftBottom));
        public static readonly Block pipeRightTop    = new PipeBlock(24, new StaticSprite(Textures.PipeRightTop));
        public static readonly Block pipeRightBottom = new PipeBlock(25, new StaticSprite(Textures.PipeRightBottom));


        private int id;
        public int ID { get { return this.id; } }

        protected ISprite sprite;

        protected Block(int id, ISprite sprite)
        {
            this.id = id;
            this.sprite = sprite;
            BLOCKS[id] = this;
        }

        public static Block GetByID(int id) { return BLOCKS[id]; }

        public static void Update()
        {
            foreach(Block blk in BLOCKS)
            {
                if(blk == null) { continue; }
                blk.sprite.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale, bool chain)
        {
            Draw(spriteBatch, position, Color.White, scale, chain);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float scale, bool chain)
        {
            this.sprite.Draw(spriteBatch, position, color, scale, chain);
        }


        public bool Visible { get { return this.sprite.Visible; } set { this.sprite.Visible = value; } }

    

        public virtual void OnCollision(int x, int y, ICollidable obj, Direction dir, World world)
        {
            IEntity entity = obj as IEntity;
            if(entity == null) { return; }

            switch (dir)
            {
                case Direction.Up:
                    entity.Position = new Vector2(entity.Position.X, y + 1);
                    entity.Velocity = new Vector2(entity.Velocity.X, 0);
                    entity.IsGrounded = true;
                    break;
                case Direction.Down:
                    entity.Position = new Vector2(entity.Position.X, y - obj.BoundingPolygon.Height);
                    entity.Velocity = new Vector2(entity.Velocity.X, 0);

                    foreach(Enemies.IEnemy enemy in world.GetEntitiesAt<Enemies.IEnemy>(x, y + 1))
                    {
                        enemy.TakeDamage();
                        enemy.Accelerate(0, 0.2f);
                    }
                    break;
                case Direction.Left:
                    entity.Position = new Vector2(x - obj.BoundingPolygon.Width, entity.Position.Y);
                    entity.Velocity = new Vector2(0, entity.Velocity.Y);
                    break;
                case Direction.Right:
                    entity.Position = new Vector2(x + 1, entity.Position.Y);
                    entity.Velocity = new Vector2(0, entity.Velocity.Y);
                    break;
            }
        }

        public void OnCollision(ICollidable obj, Direction dir, World world) {  }
        public IPolygon BoundingPolygon { get { return null; } set { BoundingPolygon = value; } }
    }
}