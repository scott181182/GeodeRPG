using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioYeah.Enemies;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Realm;
using SuperMarioYeah.Sprites;
using SuperMarioYeah.Collectable;

/**
 * More complicated example on making the Entity moved, via a Func class
 * It's basically just a parametric equation: you input time, it gives you a position back
 */
namespace SuperMarioYeah.Entities
{
    public class ParametricEntity : AbstractEntity
    {
        public override float Width { get { return (float)sprite.Bounds.X / World.BLOCK_SIZE; } }
        public override float Height { get { return (float)sprite.Bounds.Y / World.BLOCK_SIZE; } }
        public override bool IsTransitioning { get { return false; } }

        public float Speed;

        private Vector2 startPosition;
        private Func<float, Vector2> path;
        private float counter, start, stop;

        public ParametricEntity(ISprite sprite, Vector2 position, Func<float, Vector2> path, float start = 0, float stop = 0, float speed = 1) : base(sprite, position)
        {
            this.startPosition = position;

            this.path = path;
            this.counter = start;
            this.start = start;
            this.stop = stop;
            this.Speed = speed;
        }


        public override void Update()
        {
            this.Position = this.startPosition + this.path.Invoke(counter);
            counter += Speed;
            if (start < stop && counter >= stop) { counter = start; }

            base.Update();
        }

        public override IEntity Clone()
        {
            ParametricEntity fred = new ParametricEntity(sprite, startPosition, path, start, stop, Speed);
            fred.counter = counter;
            return fred;
        }
    }
}