using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Entities;

namespace SuperMarioYeah.Particles
{
    public class PipeMarioParticle : AbstractEntity
    {
        public const float STEP = 1f / 48f;

        private float traversed, target;
        private bool done, outward;
        private Direction inDir, outDir;
        private Sprites.MarioSprite marioSprite;

        private Func<Vector2> teleport;
        private Action onFinish;

        public override bool IsGravitable => false;
        public override bool IsTransitioning => !done;

        

        public PipeMarioParticle(Sprites.MarioSprite sprite, Vector2 location, Direction inDir, Action onFinish): this(sprite, location, inDir, Direction.None, () => Vector2.Zero, onFinish) {  }
        public PipeMarioParticle(Sprites.MarioSprite sprite, Vector2 location, Direction inDir, Direction outDir, Func<Vector2> teleport, Action onFinish) : base(sprite.GetIdleSprite(), location)
        {
            target = (inDir.IsHorizontal() ? Width : Height) * 1.5f;
            marioSprite = sprite;
            marioSprite.Visible = false;
            done = false;
            outward = false;
            this.inDir = inDir;
            this.onFinish = onFinish;

            this.outDir = outDir;
            this.teleport = teleport;
        }

        public override void Update()
        {
            base.Update();

            Direction dir = outward ? outDir : inDir;
            
            switch(dir)
            {
                case Direction.Up:    Move(0, STEP);  break;
                case Direction.Right: Move(STEP, 0);  break;
                case Direction.Down:  Move(0, -STEP); break;
                case Direction.Left:  Move(-STEP, 0); break;
            }
            traversed += STEP;
            if (traversed >= target)
            {
                if(outward || outDir == Direction.None)
                {
                    onFinish();
                    done = true;
                    Kill();
                }
                else
                {
                    target = (outDir.IsHorizontal() ? Width : Height) * 1.5f;
                    traversed = 0;
                    outward = true;
                    Position = teleport();
                    switch(outDir)
                    {
                        case Direction.Up:    Position -= new Vector2(0, Height * 1.5f); break;
                        case Direction.Right: Position -= new Vector2(Width * 1.5f, 0);  break;
                        case Direction.Down:  Position += new Vector2(0, Height * 1.5f); break;
                        case Direction.Left:  Position += new Vector2(Width * 1.5f, 0);  break;
                    }
                }
            }
        }

        public override IEntity Clone()
        {
            if (outDir == Direction.None) { return new PipeMarioParticle(marioSprite, Position, inDir, onFinish); }
            else { return new PipeMarioParticle(marioSprite, Position, inDir, outDir, teleport, onFinish); }
        }
    }
}
