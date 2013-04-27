using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanA
{
    /// <summary>
    /// Represents a single note on the track
    /// </summary>
    public class Note
    {
        //buffer, in milliseconds on 
        public static const int TIMEBUFFER = 500;
        //enumeration that represents which button to associate with this note
        public enum BUTTONS {GREEEN, RED, YELLOW, BLUE, ORANGE};

        //time where the note should "start", in milliseconds
        public int timeStart;
        //which button is associated with this
        public BUTTONS button;

        public Note(int timeStart, BUTTONS button)
        {
            this.
            this.button = button;
        }
    }
}
