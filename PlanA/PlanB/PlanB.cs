using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace PlanB
{
    public partial class PlanB : Form
    {
        public PlanB()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.PARKOUR);
            simpleSound.Play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.PARKOUR_YEAH);
            simpleSound.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Wee);
            simpleSound.Play();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.YEAH_LOUDER);
            simpleSound.Play();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.SAVED);
            simpleSound.Play();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.BIBLESPLOSION);
            simpleSound.Play();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.HOLYWARsound);
            simpleSound.Play();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.CONDEMNED);
            simpleSound.Play();
        }
    }
}
