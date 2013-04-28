using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PlanA
{
    /// <summary>
    /// Represents a single note on the track
    /// </summary>
    public class Note
    {
        //buffer, in milliseconds on; 1 second = 100
        public const int TIMEBUFFER = 25;
        //enumeration that represents which button to associate with this note
        public enum BUTTONS {GREEEN = 0, RED = 1, YELLOW = 2, BLUE = 3, ORANGE = 4};

        //time where the note should "start", in milliseconds
        public int timeStart;
        //which button is associated with this
        public BUTTONS button;
        //how many points this thing is worth
        public int points;

        public Buttons[] padButtons;
        public Keys[] keyButtons;

        public Note(int timeStart, BUTTONS button)
        {
            this.timeStart = timeStart;
            this.button = button;
            //point value based on button enumeration...CAUSE FUCK YOUR SYSTEM THAT"S WHY
            this.points = (int)button * 5;
            this.mapButtons();
        }

        /// <summary>
        /// checks whether or not this note has been hit
        /// </summary>
        /// <returns></returns>
        public bool isHitConfirmed(int currentTime)
        {
            //first check for timing on either end of the buffer
            if(((this.timeStart + TIMEBUFFER) >= currentTime) && ((this.timeStart - TIMEBUFFER) <= currentTime))
            {
                //if the button/chord is held down
                if ((GamePad.GetState(PlayerIndex.One).IsButtonDown(this.padButtons[(int)button])) || (Keyboard.GetState().IsKeyDown(this.keyButtons[(int)button])))
                {
                    //then check for strumming
                    if(isStrum() == true)
                    {
                        return(true);
                    }
                }
            }
            return(false);
        }
        /// <summary>
        /// Determine if there was a strum
        /// </summary>
        /// <returns></returns>
        public bool isStrum()
        {
            if((GamePad.GetState(PlayerIndex.One).IsButtonDown(padButtons[5])) || (GamePad.GetState(PlayerIndex.One).IsButtonDown(padButtons[6])))
                return(true);
            if(Keyboard.GetState().IsKeyDown(keyButtons[5]))
                return(true);
            return (false);
        }
        /// <summary>
        /// Method that maps buttons
        /// </summary>
        public void mapButtons()
        {
            this.padButtons = new Buttons[8];
            this.keyButtons = new Keys[8];
            //green
            this.padButtons[0] = Buttons.A;
            this.keyButtons[0] = Keys.A;
            //red
            this.padButtons[1] = Buttons.B;
            this.keyButtons[1] = Keys.S;
            //yellow
            this.padButtons[2] = Buttons.Y;
            this.keyButtons[2] = Keys.D;
            //blue
            this.padButtons[3] = Buttons.X;
            this.keyButtons[3] = Keys.F;
            //orange
            this.padButtons[4] = Buttons.LeftShoulder;
            this.keyButtons[4] = Keys.G;
            //strum
            this.padButtons[5] = Buttons.DPadUp;
            this.keyButtons[5] = Keys.Space;
            this.padButtons[6] = Buttons.DPadDown;
            this.keyButtons[6] = Keys.Space;
            //whammy bar?; right stick X-axis
            this.padButtons[7] = Buttons.RightStick;
            this.keyButtons[7] = Keys.LeftShift;
        }
    }
}
