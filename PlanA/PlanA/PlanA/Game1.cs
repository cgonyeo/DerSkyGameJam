using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PlanA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;        
        
        //stores all the sound blocks/notes
        public List<Note> noteList;
        //time stuff
        public TimerClass Timer;
        //sound stuff
        public List<SoundEffect> SoundHandler;
        public List<SoundEffectInstance> SoundHandlerInst;
        //goes to 11
        public float GuitarVolume;
        //points!
        public int score;
        //Multiplier and star power shit
        public const int SCOREMULTIPLIER = 2;
        //how many notes in a row to hit until star power?
        public const int CHARGEDSTARPOWER = 10;
        //charge star power
        public int starPowerScore;
        //font to draw shit to the screen
        public SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //set the initial score to 0
            this.score = 0;
            //initialize the list
            this.noteList = new List<Note>();
            this.fillSong();
            base.Initialize();
        }

        /// <summary>
        /// Fill the list with all the notes in our bastarized song
        /// </summary>
        public void fillSong()
        {
            //please add the notes in order 
            //remember that all time is done in MILLISECONDS
            //so 100 = 1 second
            this.noteList.Add(new Note(100,Note.BUTTONS.GREEEN));
            this.noteList.Add(new Note(200, Note.BUTTONS.RED));
            this.noteList.Add(new Note(300, Note.BUTTONS.YELLOW));
            this.noteList.Add(new Note(400, Note.BUTTONS.BLUE));
            this.noteList.Add(new Note(500, Note.BUTTONS.ORANGE));
            this.noteList.Add(new Note(600, Note.BUTTONS.GREEEN));
        }

        /// <summary>
        /// Check against the list and play sound blocks
        /// </summary>
        public void checkNotes()
        {
            List<int> indexes = new List<int>();
            //loop over everything; should work for chords
            for (int cntr = 0; cntr < this.noteList.Count; cntr++)
            {
                if (this.noteList[cntr].isHitConfirmed((int)this.Timer.MilliSeconds))
                {
                    //play the mapped sound
                    this.SoundHandlerInst[(int)this.noteList[cntr].button].Play();
                    this.score += this.noteList[cntr].points;
                    indexes.Add(cntr);
                }
                //tag to remove once time has passed by
                else if ((this.noteList[cntr].timeStart + Note.TIMEBUFFER) < (int)this.Timer.MilliSeconds)
                {
                    indexes.Add(cntr);
                }
                //indexes.Add(cntr);
                //Console.WriteLine(cntr);
            }
            //at the end of checking, remove any used Notes, shorten the shit
            //and prevent duplicate sounds from playing because OH GOD OH GOD
            foreach (int sacredIndex in indexes)
            {
                //High Charity has fallen! Wort wort wort!
                //Console.WriteLine("Sacred Index: "+sacredIndex);
                this.noteList.RemoveAt(sacredIndex);
            }
            //Console.WriteLine(this.Timer.MilliSeconds);
        }
        public void whammyBar()
        {
            for (int cntr = 0; cntr < SoundHandlerInst.Count; cntr++)
            {
                if (SoundHandlerInst[cntr].State == SoundState.Playing)
                {
                    float WhammyValue = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
                    //lower whammy, lower pitch
                    //now on a 1 to 2 range
                    WhammyValue += 1f;
                    //now on a 0 to 1 range
                    WhammyValue = WhammyValue / 30f;
                    SoundHandlerInst[cntr].Pitch = -1f * WhammyValue;
                }
                //change volume based on the d-pad input changes/changes to volume
                SoundHandlerInst[cntr].Volume = this.GuitarVolume;
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //load that font!
            this.font = Content.Load<SpriteFont>("SpriteFont1"); //Lucida Console
            //my stuff to handle sound and timing; set to trigger every millisecond
            //this may be a bad idea and we may need to reduce percision later
            Timer = new TimerClass(1f);
            SoundHandler = new List<SoundEffect>();
            SoundHandlerInst = new List<SoundEffectInstance>();
            //GuitarState = GamePad.GetState(PlayerIndexValue);
            //flip loading around...apparently sounds don't actually go this way in pitch
            SoundEffect GreenSound = Content.Load<SoundEffect>("SAVED");
            SoundEffect RedSound = Content.Load<SoundEffect>("CONDEMNED");
            SoundEffect YellowSound = Content.Load<SoundEffect>("PARKOUR");
            SoundEffect BlueSound = Content.Load<SoundEffect>("YEAH_LOUDER");
            SoundEffect OrangeSound = Content.Load<SoundEffect>("HOLYWARsound");
            SoundHandler.Add(GreenSound);
            SoundHandler.Add(RedSound);
            SoundHandler.Add(YellowSound);
            SoundHandler.Add(BlueSound);
            SoundHandler.Add(OrangeSound);
            //fill the sound instances
            for (int cntr = 0; cntr < SoundHandler.Count; cntr++)
            {
                SoundHandlerInst.Add(SoundHandler[cntr].CreateInstance());
            }
            //intial volume is the volume value of the first sound file
            this.GuitarVolume = this.SoundHandlerInst[0].Volume;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //start the timer once, ver first thing once the looping begins
            if(this.Timer.IsTiming == false)
                this.Timer.Start();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //check for shit here 
            this.checkNotes();
            //update all sounds based on whammy bar info
            this.whammyBar();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            //draw the time to the screen
            //this took me longer than it should have
            String timeStr = ((((int)this.Timer.MilliSeconds) / 100) / 60) + ":" + ((((int)this.Timer.MilliSeconds) / 100) % 60) 
                + ":" + (((int)this.Timer.MilliSeconds) % 100);
            //formatting
            if (((((int)this.Timer.MilliSeconds) / 100) % 60) < 10)
            {
                timeStr = ((((int)this.Timer.MilliSeconds) / 100) / 60) + ":0" + ((((int)this.Timer.MilliSeconds) / 100) % 60) 
                    + ":" + (((int)this.Timer.MilliSeconds) % 100);
            }
            spriteBatch.DrawString(font, "Time: " + timeStr, new Vector2(300,50), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
