using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        //random object to choose random icons

        List<string> icons = new List<string>();
        

        public void GenerateList()
        {
            List<string> myList = new List<string>
        {
            "q","Q","W","w","r","R","t","T","u","U","i","I","o","O","!","@",
            "#","$","*","d","H"
        };
            for (int i=1;i<=8;i++)
            {
                Random r = new Random();
                int index = r.Next(myList.Count);
                string randomString = myList[index];
                icons.Add(randomString);
                icons.Add(randomString);
                myList.RemoveAt(index);
            }
            
        }

        private void AssignIconsToSquares()
        {
            GenerateList();
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if(iconLabel!=null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor=iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        Label firstClicked = null;
        Label secondClicked = null;


        private void label_Click(object sender, EventArgs e)
        {
            // The timer is only on after two non-matching 
            // icons have been shown to the player, 
            // so ignore any clicks if the timer is running

            if (Timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;
            if(clickedLabel!=null)
            {
                //if the label was clicked before, ignore the click
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // If firstClicked is null, this is the first icon 
                // in the pair that the player clicked,
                // so set firstClicked to the label that the player 
                // clicked, change its color to black, and return
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                // If the player gets this far, the timer isn't
                // running and firstClicked isn't null,
                // so this must be the second icon the player clicked
                // Set its color to black
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                //check if the player won
                CheckForWinner();

                // If the player clicked two matching icons, keep them 
                // black and reset firstClicked and secondClicked 
                // so the player can click another icon
                if(firstClicked.Text==secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }


                // If the player gets this far, the player 
                // clicked two different icons, so start the 
                // timer (which will wait three quarters of 
                // a second, and then hide the icons)
                Timer1.Start();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //this timer is started when the player clicks two icons that
            //don't match
            Timer1.Stop();
            //stop timer
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            //hide both icons
            firstClicked = null;
            secondClicked = null;
            //reset firstClicked and secondClicked
        }

        private void CheckForWinner()
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if(iconLabel!=null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            //if the loop didn't return=> the player has won
            MessageBox.Show("You matched all the icons!", "Congratulations!");
            if (MessageBox.Show("Do you want to play a new game?", "Matching Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                /*icons.Add("a");
                icons.Add("a");
                icons.Add("B");
                icons.Add("B");
                icons.Add("$");
                icons.Add("$");
                icons.Add("@");
                icons.Add("@");
                icons.Add(",");
                icons.Add(",");
                icons.Add("-");
                icons.Add("-");
                icons.Add("k");
                icons.Add("k");
                icons.Add("8");
                icons.Add("8");*/
                AssignIconsToSquares();

            }
            else
                Close();
        }
    }
}
