using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chronometer
{
    public partial class Form1 : Form
    {
        int seconds, minutes, hours, centseconds;
        int last = 1, lastseconds = 0, lastminutes = 0, lasthours = 0, lastcentseconds=0;

        public Form1()
        {
            InitializeComponent();
        }

        private void resetButton_Click(object sender, EventArgs e){
            timer.Enabled = false;
            reset(); // reset the time variables
            label_update(); //update the label
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {   listView1.Items[i].Remove();    } //remove all the items
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reset();
        }

        private void startstopButton_Click(object sender, EventArgs e)
        {
            timer.Enabled=!timer.Enabled;
        }

        private void lapButton_Click(object sender, EventArgs e){
            if (timer.Enabled){
                ListViewItem list = new ListViewItem(last.ToString());
                list.SubItems.Add(timeLabel.Text);
                list.SubItems.Add(Time_difference());
                last++;
                lastcentseconds = centseconds;
                lasthours = hours;
                lastminutes = minutes;
                lastseconds = seconds;
                listView1.Items.Add(list);
            }
        }

        private string Time_difference()
        {
            string time = "+";
            int calcseconds, calcminutes, calchours, calccentseconds;
            if (centseconds < lastcentseconds)
            {
                seconds--;
                if(seconds<0)
                {
                    seconds += 60;
                    minutes--;
                    if(minutes<0)
                    {
                        minutes += 60;
                        hours--;
                    }
                }
                calccentseconds = (centseconds+100) - lastcentseconds;
            }
            else
            {
                calccentseconds = centseconds - lastcentseconds;
            }

            if (seconds < lastseconds)
            {
                minutes--;
                if (minutes < 0)
                {
                    minutes += 60;
                    hours--;
                }
                calcseconds = (seconds+60) - lastseconds;
            }
            else
            {
                calcseconds = seconds - lastseconds;
            }

            if (minutes < lastminutes)
            {
                hours--;
                calcminutes = (minutes + 60) - lastminutes;
            }
            else
            {
                calcminutes = minutes - lastminutes;
            }

            calchours = hours - lasthours;

            //build the string
            if (calchours < 10)
                time = time + "0" + calchours.ToString() + ":";
            else
                time = time + calchours.ToString() + ":";
            if (calcminutes < 10)
                time = time + "0" + calcminutes.ToString() + ":";
            else
                time = time + calcminutes.ToString() + ":";
            if (calcseconds < 10)
                time = time + "0" + calcseconds.ToString() + ":";
            else
                time = time + calcseconds.ToString() + ":";
            if (calccentseconds < 10)
                time = time + "0" + calccentseconds.ToString();
            else
                time = time + calccentseconds.ToString();

            return time;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            centseconds++;
            if (centseconds >= 100){
                centseconds = 0;
                seconds++;
                if(seconds>=60){
                    seconds = 0;
                    minutes++;
                    if (minutes >= 60){
                        minutes = 0;
                        hours = 0;
                    }
                }
            }
            label_update(); //just create the string to display
        }
        void reset()
        {
            seconds = 0; minutes = 0; hours = 0; centseconds = 0;
            last = 1; lastcentseconds = 0; lasthours = 0; lastminutes = 0; lastseconds = 0;
        }
        void label_update()
        {
            String time = "";
            if (hours < 10)
                time = time + "0" + hours.ToString() + ":";
            else
                time = time + hours.ToString() + ":";
            if (minutes < 10)
                time = time + "0" + minutes.ToString() + ":";
            else
                time = time + minutes.ToString() + ":";
            if (seconds < 10)
                time = time + "0" + seconds.ToString() + ":";
            else
                time = time + seconds.ToString() + ":";
            if (centseconds < 10)
                time = time + "0" + centseconds.ToString();
            else
                time = time + centseconds.ToString();
            timeLabel.Text = time;
        }
    }
}
