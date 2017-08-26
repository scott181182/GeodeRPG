using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioYeah.Enemies;
using SuperMarioYeah.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarioYeah.Realm.Blocks;

namespace SuperMarioYeah.Realm
{
    public class Camera
    {
        public const float Scale = 2;
        public const int TILE_SIZE = (int)(World.BLOCK_SIZE * Scale);

        public const int BLOCK_WIDTH = 24;
        public const int BLOCK_HEIGHT = 16;
        public const int CAMERA_WIDTH = BLOCK_WIDTH * TILE_SIZE;
        public const int CAMERA_HEIGHT = BLOCK_HEIGHT * TILE_SIZE;
        public static readonly Rectangle VIEWPORT = new Rectangle(0, 0, CAMERA_WIDTH, CAMERA_HEIGHT);

        public const int BG_HEIGHT = 240;
        public const float BG_RATIO = (float)BG_HEIGHT / CAMERA_HEIGHT;
        public const int BG_WIDTH = (int)(CAMERA_WIDTH * BG_RATIO);

        private Vector2 cameraPosition;
        public Vector2 WorldPosition { get => cameraPosition / TILE_SIZE; }


        private World world;

        public Camera(World world)
        {
            cameraPosition = Vector2.Zero;
            this.world = world;
        }

        private void SetPositionOffPlayer(List<IEntity> entities)
        {
            if (entities != null && entities.OfType<Mario>().Count() > 0)
            {
                Mario player = entities.OfType<Mario>().First();
                cameraPosition.X = WorldToScreenCoord(player.Position, 0).X - (CAMERA_WIDTH / 2);
                cameraPosition = Vector2.Clamp(cameraPosition,
                    new Vector2(world.CurrentRegion.start * TILE_SIZE, 0),
                    new Vector2(world.CurrentRegion.end * TILE_SIZE - CAMERA_WIDTH, 0));
            }
        }

        public static Vector2 WorldToScreenCoord(Vector2 worldCoord, float height)
        {
            return new Vector2(worldCoord.X, World.WORLD_HEIGHT - worldCoord.Y - height) * TILE_SIZE;
        }
        public Vector2 ScreenToCameraCoord(Vector2 screenCoord)
        {
            return screenCoord - cameraPosition;
        }

        public static Rectangle GetBlockBounds(Vector2 position)
        {
            return new Rectangle(
                position.ToPoint(),
                new Point(TILE_SIZE, TILE_SIZE));
        }
        public Rectangle GetEntityBounds(IEntity entity)
        {
            Vector2 screenPosition = WorldToScreenCoord(entity.Position, entity.Height);
            Vector2 cameraPosition = ScreenToCameraCoord(screenPosition);

            return new Rectangle(
                cameraPosition.ToPoint(),
                new Point(
                    (int)(entity.Width * TILE_SIZE),
                    (int)(entity.Height * TILE_SIZE)));
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D background, List<IEntity> entities, int[,] tiles)
        {
            SetPositionOffPlayer(entities);

            spriteBatch.Begin();
            spriteBatch.Draw(background, VIEWPORT, new Rectangle(new Point((int)(cameraPosition.X * BG_RATIO), 0), new Point(BG_WIDTH, BG_HEIGHT)), Color.White);
            
            foreach (IEntity entity in entities)
            {
                if (entity != null && GetEntityBounds(entity).Intersects(VIEWPORT))
                {
                    Vector2 entityCoord = WorldToScreenCoord(entity.Position, entity.Height);
                    Vector2 cameraCoord = ScreenToCameraCoord(entityCoord);

                    entity.Draw(spriteBatch, cameraCoord, Scale, true);
                }
            }
            spriteBatch.End();

            spriteBatch.Begin();

            for (int x = 0; x < World.WORLD_WIDTH; x++)
            {
                for (int y = 0; y < World.WORLD_HEIGHT; y++)
                {
                    if (tiles[x, y] != 0)
                    {
                        Vector2 blockCoord = WorldToScreenCoord(new Vector2(x, y), 1);
                        Vector2 cameraCoord = ScreenToCameraCoord(blockCoord);

                        if (GetBlockBounds(cameraCoord).Intersects(VIEWPORT))
                        {
                            Block.GetByID(tiles[x, y]).Draw(spriteBatch, cameraCoord, Scale, true);
                        }
                    }
                }
            }
            spriteBatch.End();
        }
    }
}


