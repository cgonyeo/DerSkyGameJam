using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

//using System.Threading;

//Author: Schuyler
namespace PlanA
{
    public class TimerClass
    {
        //set up as C# "Properties"
        //increment length in milliseconds
        public float IncrementLength { get; set; }
        //time; in seconds or milliseconds-> essentially the time that has elapsed
        public float Seconds { get; set; }
        public float MilliSeconds { get; set; }
        //timer class object
        public Timer MyTimer;
        //two time stamp variables...for use eventually; use time stamp method to actually give it a value
        public float TimeStamp1 { get; set; }
        public float TimeStamp2 { get; set; }
        public Boolean IsTiming { get; set; }

        //added for this app
        //public List<GuitarButtonClass> ListOfBtn;
        //public Form1 ThisForm;
        //private System.Threading.Thread SafeText;
        //private bool OldState;

        //my class has a Timer object-> my class is better because it can actually increment time
        public TimerClass()
        {
            MilliSeconds = 0;
            Seconds = 0;
            //defaults by incrementing in whole seconds
            IncrementLength = 1000;
            MyTimer = new Timer(IncrementLength);
            //fortunately the Milliseconds is =0 right now
            SetTimeStamp1();
            SetTimeStamp2();
            this.IsTiming = false;
            MyTimer.Elapsed += new ElapsedEventHandler(IncrementEvent);
        }
        public TimerClass(float IncrementArg)
        {
            //this.ListOfBtn = ListOfBtnArg;
            //this.ThisForm = ThisFormArg;
            MilliSeconds = 0;
            Seconds = 0;
            //defaults by incrementing in whole seconds
            IncrementLength = IncrementArg;
            MyTimer = new Timer(IncrementLength);
            //fortunately the Milliseconds is =0 right now
            SetTimeStamp1();
            SetTimeStamp2();
            this.IsTiming = false;
            MyTimer.Elapsed += new ElapsedEventHandler(IncrementEvent);
            //OldState = !DetectConnection();
        }
        //start, stop and reset methods
        public void Start()
        {
            //starts the event
            //MyTimer.Elapsed+=new ElapsedEventHandler(IncrementEvent);
            this.IsTiming = true;
            MyTimer.Start();
        }
        public void Stop()
        {
            this.IsTiming = false;
            MyTimer.Stop();
        }
        //reset actually sets time back to 0
        public void Reset()
        {
            //calls the stop method
            this.IsTiming = false;
            Stop();
            //time actually reset to 0
            Seconds = 0;
            MilliSeconds = 0;
        }
        public virtual void IncrementEvent(object source, ElapsedEventArgs e)
        {
            //just increments milliseconds by the amount when 
            MilliSeconds += IncrementLength;
            //increments seconds; every 1000 milliseconds
            Seconds = MilliSeconds / 1000f;
            //checks to be done
            //RunTime();
            //closes out of app by hitting start button
            /*
            if (GamePad.GetState(ThisForm.PlayerIndexValue).IsButtonDown(Buttons.Start))
            {
                this.Stop();
            }*/
            /*
            this.SafeText=new System.Threading.Thread(new System.Threading.ThreadStart(DisplayConnection));
            try
            {
                this.SafeText.Start();
            }
            catch(Exception)
            {
            }*/
        }
        //sets the timestamps to the millisecond time (use this...don't just assign it a value)
        public void SetTimeStamp1()
        {
            TimeStamp1 = MilliSeconds;
        }
        public void SetTimeStamp2()
        {
            TimeStamp2 = MilliSeconds;
        }
    }
}
