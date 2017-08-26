using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SuperMarioYeah.Entities;

namespace SuperMarioYeah.Sounds
{
    public static class SoundLoader
    {
        private static MarioGame game;
        private static AudioListener listener;
        private static AudioEmitter emitter;

        private static Random rng;

        private static Dictionary<String, SoundEffectInstance> Songs;
        private static Dictionary<String, SoundEffect> Effects;

        private static string CurrentSong;
        public static void LoadSounds(MarioGame game)
        {
            SoundLoader.game = game;
            rng = new Random();

            Songs = new Dictionary<string, SoundEffectInstance>();
            Effects = new Dictionary<string, SoundEffect>();

            listener = new AudioListener();
            emitter = new AudioEmitter();

            Effects.Add("1-up", game.Content.Load<SoundEffect>("Sound/smb_1-up"));
            Effects.Add("bowserFall", game.Content.Load<SoundEffect>("Sound/smb_bowserfall"));
            Effects.Add("bowserFire", game.Content.Load<SoundEffect>("Sound/smb_bowserfire"));
            Effects.Add("breakBlock", game.Content.Load<SoundEffect>("Sound/smb_breakblock"));
            Effects.Add("bump", game.Content.Load<SoundEffect>("Sound/smb_bump"));
            Effects.Add("coin", game.Content.Load<SoundEffect>("Sound/smb_coin"));
            Effects.Add("fireball", game.Content.Load<SoundEffect>("Sound/smb_fireball"));
            Effects.Add("fireworks", game.Content.Load<SoundEffect>("Sound/smb_fireworks"));
            Effects.Add("flagpole", game.Content.Load<SoundEffect>("Sound/smb_flagpole"));
            Effects.Add("gameOver", game.Content.Load<SoundEffect>("Sound/smb_gameover"));
            Effects.Add("jumpSmall", game.Content.Load<SoundEffect>("Sound/smb_jumpsmall"));
            Effects.Add("jumpBig", game.Content.Load<SoundEffect>("Sound/smb_jump-super"));
            Effects.Add("kick", game.Content.Load<SoundEffect>("Sound/smb_kick"));
            Effects.Add("marioDie", game.Content.Load<SoundEffect>("Sound/smb_mariodie"));
            Effects.Add("pause", game.Content.Load<SoundEffect>("Sound/smb_pause"));
            Effects.Add("pipe", game.Content.Load<SoundEffect>("Sound/smb_pipe"));
            Effects.Add("powerupAppears", game.Content.Load<SoundEffect>("Sound/smb_powerup_appears"));
            Effects.Add("stageClear", game.Content.Load<SoundEffect>("Sound/smb_stage_clear"));
            Effects.Add("stomp", game.Content.Load<SoundEffect>("Sound/smb_stomp"));
            Effects.Add("vine", game.Content.Load<SoundEffect>("Sound/smb_vine"));
            Effects.Add("warning", game.Content.Load<SoundEffect>("Sound/smb_warning"));
            Effects.Add("worldClear", game.Content.Load<SoundEffect>("Sound/smb_world_clear"));
            
            Effects.Add("explode1", game.Content.Load<SoundEffect>("Sound/Boom/explode1"));
            Effects.Add("explode2", game.Content.Load<SoundEffect>("Sound/Boom/explode2"));
            Effects.Add("explode3", game.Content.Load<SoundEffect>("Sound/Boom/explode3"));
            Effects.Add("explode4", game.Content.Load<SoundEffect>("Sound/Boom/explode4"));


            Songs.Add("starTheme", game.Content.Load<SoundEffect>("Sound/starTheme").CreateInstance());
        }
        // Wrapping to avoid magic strings
        public static void OneUp(Vector2 position) { PlayEffect("1-up", position); }
        public static void BreakBlock(Vector2 position) { PlayEffect("breakBlock", position); }
        public static void Bump(Vector2 position) { PlayEffect("bump", position); }
        public static void Coin(Vector2 position) { PlayEffect("coin", position); }
        public static void Fireball(Vector2 position) { PlayEffect("fireball", position); }
        public static void Fireworks(Vector2 position) { PlayEffect("fireworks", position); }
        public static void Flagpole(Vector2 position) { PlayEffect("flagpole", position); }
        public static void GameOver(Vector2 position) { PlayEffect("gameOver", position); }
        public static void JumpSmall(Vector2 position) { PlayEffect("jumpSmall", position); }
        public static void JumpBig(Vector2 position) { PlayEffect("jumpBig", position); }
        public static void Kick(Vector2 position) { PlayEffect("kick", position); }
        public static void MarioDie(Vector2 position) { PlayEffect("marioDie", position); }
        public static void Pause(Vector2 position) { PlayEffect("pause", position); }
        public static void Pipe(Vector2 position) { PlayEffect("pipe", position); }
        public static void PowerupAppears(Vector2 position) { PlayEffect("powerupAppears", position); }
        public static void StageClear(Vector2 position) { PlayEffect("stageClear", position); }
        public static void Stomp(Vector2 position) { PlayEffect("stomp", position); }
        public static void Vine(Vector2 position) { PlayEffect("vine", position); }
        public static void Warning(Vector2 position) { PlayEffect("warning", position); }
        public static void WorldClear(Vector2 position) { PlayEffect("worldClear", position); }
        public static void Explode(Vector2 position)
        {
            int which = rng.Next() % 4 + 1;
            PlayEffect("explode" + which, position);
        }

        private static void PlayEffect(String effect, Vector2 position)
        {
            SoundEffectInstance sound = Effects[effect].CreateInstance();
            
            Mario mario = SoundLoader.game.GameWorld.Player;
            listener.Position = new Vector3(mario.Position, 0);
            if ((position - mario.Position).Length() <= 1) { emitter.Position = new Vector3(position, 0); }
            else { emitter.Position = new Vector3(mario.Position + Vector2.Normalize(position - mario.Position), 0); }
            sound.Apply3D(listener, emitter);
            sound.Play();
        }

        private static void PlaySong(String song)
        {
            Songs[song].Play();
            CurrentSong = song;
        }

        private static void StopCurrentSong()
        {
            Songs[CurrentSong].Stop();
        }

    }
}
