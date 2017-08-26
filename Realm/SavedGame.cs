using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarioYeah.Entities;

namespace SuperMarioYeah.Realm
{
    public class SavedGame
    {
        public WorldState State { get; private set; }

        public List<WorldRegion> Regions { get; private set; }
        public WorldRegion CurrentRegion { get; private set; }

        public int Lives { get; private set; }

        public List<IEntity> Entities { get; private set; }
        public int[,] Tiles { get; private set; }

        public int Coins { get; private set; }
        public int Points { get; private set; }
        public int Timer { get; private set; }
        public int TransitionTimer { get; private set; }

        public SavedGame(WorldState state, List<WorldRegion> regions, WorldRegion region, int lives, List<IEntity> entities, int[,] tiles,
            int coins, int points, int timer, int transitionTimer)
        {
            State = state;
            Regions = regions;
            CurrentRegion = region;
            Lives = lives;

            Entities = entities.ConvertAll(ent => ent.Clone());
            Tiles = tiles.Clone() as int[,];

            Coins = coins;
            Points = points;
            Timer = timer;
            TransitionTimer = transitionTimer;
        }
    }
}
