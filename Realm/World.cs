using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMarioYeah.Collectable;
using SuperMarioYeah.Collision;
using SuperMarioYeah.Enemies;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Sprites;
using SuperMarioYeah.Realm.Blocks;

namespace SuperMarioYeah.Realm
{
    public enum WorldState
    {
        NORMAL, PAUSED, DYING, SWITCHING, ENDING
    }

    public class World
    {
        public const int BLOCK_SIZE = 16;
        public const float GRAVITY = -0.015f;
        public const float TERMINAL_VELOCITY = 10;

        public static int WORLD_WIDTH = 250;//214;
        public static int WORLD_HEIGHT = 16;


        private WorldState state;
        public WorldState State
        {
            get => state;
            set
            {
                state = value;
                if(state == WorldState.DYING) { transitionTimer = 30 * 6; }
            }
        }
        public List<WorldRegion> Regions;
        public WorldRegion CurrentRegion { get; set; }
        private SavedGame lastSave;

        public int Lives { get; set; }

        private bool isPaused;


        private MarioGame game;
        private Mario player;
        private Camera camera;
        private WorldCollider collider;
        private HUD hud;

        private int[,] tiles;
        private List<IEntity> entities;
        private Texture2D background;

        private List<IEntity> toAdd, toRemove;

        private int coinsCollected, points;
        private int  transitionTimer;

        private int[] checkpoints;
        private int nextCheckpoint;

        public Mario Player {
            get { return player; }
            set
            {
                entities.Remove(player);
                this.player = value;
                entities.Add(this.player);
            }
        }

        public int Timer { get; set; }

        public World(MarioGame game)
        {
            background = Textures.Background;

            Regions = new List<WorldRegion>();
            tiles = new int[WORLD_WIDTH, WORLD_HEIGHT];
            entities = new List<IEntity>();

            camera = new Camera(this);
            hud = new HUD();
            collider = new WorldCollider(this, camera);

            toAdd = new List<IEntity>();
            toRemove = new List<IEntity>();

            coinsCollected = 0;
            points = 0;
            Lives = 3;
            isPaused = false;

            nextCheckpoint = 0;

            state = WorldState.NORMAL;
            this.game = game;
        }
        private void LoadFromSave(SavedGame save)
        {
            State = save.State;
            Regions = save.Regions;
            CurrentRegion = save.CurrentRegion;
            Lives = save.Lives;
            entities.Clear();
            entities.AddRange(save.Entities.Select(ent => ent.Clone()));
            player = entities.OfType<Mario>().First();
            tiles = save.Tiles.Clone() as int[,];

            coinsCollected = save.Coins;
            points = save.Points;
            Timer = save.Timer;
            transitionTimer = save.TransitionTimer;
        }

        public void LoadLastSave() { LoadFromSave(lastSave); }
        public void Save()
        {
            lastSave = new SavedGame(State, Regions, CurrentRegion, Lives, entities, tiles,
                coinsCollected, points, Timer, transitionTimer);
        }

        public void TogglePause()
        {
            isPaused = !isPaused;
            state = isPaused ? WorldState.PAUSED : WorldState.NORMAL;
            Sounds.SoundLoader.Pause(player.Position);
        }
        

        public void Update()
        {
            if(transitionTimer > 0) { transitionTimer--; }
            if(transitionTimer == 0 && state == WorldState.DYING) { LoadLastSave(); }

            if (!isPaused)
            {
                foreach (IEntity entity in entities)
                {
                    Rectangle cameraBounds = camera.GetEntityBounds(entity);
                    if (cameraBounds.Right < 0 || cameraBounds.Left > Camera.VIEWPORT.Right)
                    {
                        if (entity is Projectiles.AbstractProjectile) { RemoveEntity(entity); }
                        continue;
                    }

                    entity.Update();
                    if (entity.IsGravitable) { HandleGravity(entity); }
                }

                Block.Update();

                collider.HandleCollisions(entities, tiles);

                foreach (IEntity entity in entities)
                {
                    if (!entity.IsAlive && !entity.IsTransitioning) { RemoveEntity(entity); }
                }

                if (toAdd.Count > 0)
                {
                    foreach (IEntity entity in toAdd) { entities.Add(entity); }
                    toAdd.Clear();
                }
                if (toRemove.Count > 0)
                {
                    foreach (IEntity entity in toRemove) { entities.Remove(entity); }
                    toRemove.Clear();
                }
                if(checkpoints != null && checkpoints.Length > nextCheckpoint && Player.Position.X > checkpoints[nextCheckpoint])
                {
                    Save();
                    if (checkpoints.Length > nextCheckpoint)
                    {
                        nextCheckpoint++;
                    }
                }

                if (state != WorldState.ENDING && state != WorldState.DYING)
                {
                    Timer--;
                }
                if (Timer == 0)
                {
                    Player.Kill();
                }
                


            }
            else if (this.state == WorldState.ENDING)
            {
                EndingAnimation.Update(this, player, FindFlag(entities), coinsCollected);
            }

        }

        private Flag FindFlag(List<IEntity> entities)
        {
            Flag flag = null;

            foreach (IEntity entity in entities)
            {
                if (entity is Flag)
                {
                    flag = entity as Flag;
                }
            }

            return flag; 
        }

        private void HandleGravity(IEntity entity)
        {
            if (entity.IsGrounded)
            {
                int leftX = (int)Math.Floor(entity.Position.X);
                int rightX = (int)Math.Floor(entity.Position.X + entity.Width);
                int yCoord = (int)Math.Floor(entity.Position.Y - 1);

                if (yCoord >= 0 && yCoord < tiles.GetLength(1) && (leftX >= 0 && tiles[leftX, yCoord] == 0)
                    && (rightX < tiles.GetLength(0) && tiles[rightX, yCoord] == 0))
                {
                    entity.IsGrounded = false;
                }
            }

            if (entity.Velocity.Y <= TERMINAL_VELOCITY && !entity.IsGrounded)
            {
                entity.Accelerate(0, GRAVITY);
            }
        }

        

        public void SetTileAt(int x, int y, int tile)
        {
            if (GetTileEntityAt<ITileEntity>(x, y) != null) { GetTileEntityAt<ITileEntity>(x, y).Kill(); }
            tiles[x, y] = tile;
        }
        public void SetTileAt(int x, int y, Block block) { SetTileAt(x, y, block.ID); }
        public Block GetBlockAt(int x, int y)
        {
            if(x < 0 || x >= tiles.GetLength(0) || y < 0 || y >= tiles.GetLength(1)) { return null; }
            return Block.GetByID(tiles[x, y]);
        }
        public List<T> GetEntitiesAt<T>(int x, int y) where T: IEntity
        {
            List<T> ret = new List<T>();

            foreach(T ent in entities.OfType<T>())
            {
                int leftCoord = (int)Math.Floor(ent.Position.X);
                int rightCoord = (int)Math.Ceiling(ent.Position.X + ent.Width);
                int bottomCoord = (int)Math.Floor(ent.Position.Y);
                if(leftCoord <= x && rightCoord >= x && bottomCoord == y) { ret.Add(ent); }
            }

            return ret;
        }
        public T GetTileEntityAt<T>(int x, int y) where T: ITileEntity {
            IEnumerable<T> list = entities.OfType<T>();
            foreach(T tile in list)
            {
                if (tile.Position.X == x && tile.Position.Y == y) { return tile; }
            }
            return default(T);
        }

        public void AddEntity(IEntity entity, bool immediate = false)
        {
            if (immediate) { entities.Add(entity); }
            else { toAdd.Add(entity); }
        }
        private void RemoveEntity(IEntity entity) { toRemove.Add(entity);}
     
        public void CollectCoin(Coin coin)
        {
            coin.Kill();
            coinsCollected++;
            Sounds.SoundLoader.Coin(coin.Position);
        }

        public void ConvertCoinIntoPoints()
        {
            coinsCollected--;
            EarnPoints(100);
        }
        public void EarnPoints(int p) { points += p; }

        public void Draw(SpriteBatch spriteBatch)
        {
            camera.Draw(spriteBatch, background, entities, tiles);
            hud.Draw(spriteBatch, points, coinsCollected, 1, 1, Timer, state);
        }

        public void EndLevel(Mario mario)
        {
            isPaused = true;
            state = WorldState.ENDING;
            mario.Move(0.2f, 0);
            Sounds.SoundLoader.WorldClear(mario.Position);
            Sounds.SoundLoader.Flagpole(mario.Position);
        }

        public void LoadCheckpoints(List<int> checkpnts)
        {
            checkpoints = new int[checkpnts.Count];
            checkpoints = checkpnts.ToArray();
        }

        public void ExitLevel()
        {
            game.State = GameState.WorldSelector;
        }
    }

    public struct WorldRegion
    {
        public int start, end, height;

        public WorldRegion(int start, int end, int height)
        {
            this.start = start;
            this.end = end;
            this.height = height;
        }
    }
}
