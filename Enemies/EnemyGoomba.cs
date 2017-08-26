using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using SuperMarioYeah.Sprites;
using SuperMarioYeah.Collectable;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Realm;
using SuperMarioYeah.Entities;

namespace SuperMarioYeah.Enemies
{
    public class EnemyGoomba : AbstractEnemy
    {
        public static readonly ISprite walkingSprite
            = new AnimatedSprite(Textures.GoombaWalking, rows: 1, cols: 2, frameTime: 10);
        public static readonly ISprite deadSprite
            = new StaticSprite(Textures.GoombaDead);

        public EnemyGoomba(Vector2 position) : base(walkingSprite, position, deathTime: 30) {  }

        public override void TakeDamage()
        {
            this.sprite = deadSprite;
            Kill();
        }

        public override IEntity Clone()
        {
            EnemyGoomba fred = new EnemyGoomba(Position);
            fred.Velocity = Velocity;
            return fred;
        }
    }
}
