using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SuperMarioYeah.Collectable;
using SuperMarioYeah.Enemies;
using SuperMarioYeah.Entities;
using SuperMarioYeah.Realm.Blocks;
using SuperMarioYeah.Realm.TileEntity;

namespace SuperMarioYeah.Realm
{
    public static class WorldLoader
    {
        public static World LoadWorld(String filename, MarioGame game)
        {
            World world = new World(game);

            string[] lines = System.IO.File.ReadAllLines(filename);

            List<int> checkpoints = new List<int>(); 

            for (int x = 0; x < World.WORLD_WIDTH; x++)
            {
                for (int y = 0; y < World.WORLD_HEIGHT; y++)
                {
                    world.SetTileAt(x, y, 0);

                    if(World.WORLD_HEIGHT - 1 - y >= lines.Length) { continue; }
                    string[] row = lines[World.WORLD_HEIGHT - 1 - y].Split(',');

                    if (x < row.Length && row[x].Length > 0)
                    {
                        Vector2 pos = new Vector2(x, y);

                        if (row[x].StartsWith("#"))
                        {
                            ParseMetaData(row[x], world);
                            continue;
                        }

                        Match pipeMatch = Regex.Match(row[x], @"(.*Pipe[^-]*)-([0-9]+).([0-9]+).([0-9]+)");
                        if(pipeMatch.Success)
                        {
                            String pipeType = pipeMatch.Groups[1].Value;
                            int regionNum = int.Parse(pipeMatch.Groups[2].Value);
                            int tx = int.Parse(pipeMatch.Groups[3].Value);
                            int ty = int.Parse(pipeMatch.Groups[4].Value);
                            AddPipe(world, pipeType, x, y, regionNum, tx, ty);
                            continue;
                        }
                        

                        switch (row[x])
                        {
                            case "Mushroom": world.AddEntity(new Mushroom(pos),    true); break;
                            case "Fire":     world.AddEntity(new Fireflower(pos),  true); break;
                            case "1-up":     world.AddEntity(new OneUp(pos),       true); break;
                            case "Coin":     world.AddEntity(new Coin(pos),        true); break;
                            case "Star":     world.AddEntity(new Star(pos),        true); break;
                            case "Goomba":   world.AddEntity(new EnemyGoomba(pos), true); break;
                            case "Koopa":    world.AddEntity(new EnemyKoopa(pos),  true); break;



                            case "Brick":     world.SetTileAt(x, y, Block.brickBlock);          break;
                            case "Platform":  world.SetTileAt(x, y, Block.platformBlock);       break;
                            case "?":         world.SetTileAt(x, y, Block.mysteryCoinBlock);    break;
                            case "? Powerup": world.SetTileAt(x, y, Block.mysteryPowerUpBlock); break;
                            case "Used":      world.SetTileAt(x, y, Block.usedBlock);           break;
                            case "Cracked":   world.SetTileAt(x, y, Block.crackedBlock);        break;
                            case "Hidden":    world.SetTileAt(x, y, Block.hiddenBlock);         break;
                            case "10CoinBrick":
                                world.SetTileAt(x, y, Block.coinBrickBlock);
                                world.AddEntity(new CoinTileEntity(x, y, 10));
                                break;
                            case "StarBrick": world.SetTileAt(x, y, Block.starBrickBlock); break;
                                

                            case "LPipeTop": world.SetTileAt(x, y, Block.pipeUpLeftTop);  break;
                            case "RPipeTop": world.SetTileAt(x, y, Block.pipeUpRightTop); break;
                            case "LPipe":    world.SetTileAt(x, y, Block.pipeUpLeft);     break;
                            case "RPipe":    world.SetTileAt(x, y, Block.pipeUpRight);    break;
                            case "TPipe":    world.SetTileAt(x, y, Block.pipeSideTop);    break;
                            case "BPipe":    world.SetTileAt(x, y, Block.pipeSideBottom);    break;

                            case "Flag":
                                
                                Flagpole flagpole = new Flagpole(new Vector2(x, y));
                                flagpole.Move(.2f, 0);

                                world.AddEntity(flagpole);

                                Flag flag = new Flag(new Vector2(x - 1, y + 8));
                                flag.Move(0.3f, 0);
                                world.AddEntity(flag);
                                break;

                            case "Checkpoint": checkpoints.Add(x) ; break;

                            case "Mario":    world.Player = new Mario(pos, world); break;
                        }
                    }
                }
            }
            world.LoadCheckpoints(checkpoints);

            if (world.Regions.Count == 0)
            {
                world.Regions.Add(new WorldRegion(0, World.WORLD_WIDTH, World.WORLD_HEIGHT));
            }
            world.CurrentRegion = world.Regions[0];

            world.Save();
            return world;
        }

        private static void ParseMetaData(String data, World world)
        {
            String meta = data.Substring(1);
            int start, end, height;

            String[] info = meta.Split('$');
            int timer = int.Parse(info[0]);
            world.Timer = timer*30;

            String[] regions = info[1].Split(';');

            foreach(String region in regions)
            {
                String[] frags = region.Split('.');
                start  = int.Parse(frags[0]);
                end    = int.Parse(frags[1]);
                height = int.Parse(frags[2]);
                world.Regions.Add(new WorldRegion(start, end, height));
            }
        }

        private static void AddPipe(World world, String pipeType, int x, int y, int region, int tx, int ty)
        {
            switch(pipeType)
            {
                case "LPipeTop":
                    world.SetTileAt(x, y, Block.pipeUpLeftTop);
                    world.AddEntity(new PipeTileEntity(x, y, Collision.Direction.Up, region, tx, ty), true);
                    break;
                case "RPipeTop":
                    world.SetTileAt(x, y, Block.pipeUpRightTop);
                    world.AddEntity(new PipeTileEntity(x, y, Collision.Direction.Up, region, tx, ty), true);
                    break;
                case "PipeLeftTop":
                    world.SetTileAt(x, y, Block.pipeLeftTop);
                    world.AddEntity(new PipeTileEntity(x, y, Collision.Direction.Left, region, tx, ty), true);
                    break;
                case "PipeLeftBottom":
                    world.SetTileAt(x, y, Block.pipeLeftBottom);
                    world.AddEntity(new PipeTileEntity(x, y, Collision.Direction.Left, region, tx, ty), true);
                    break;
            }
        }
    }
}
