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
        //firstClicked points to the first Label control
        //that the player clicks, but it will be null 
        //if the player hasn't clicked a label yet.  
        Label firstClicked = null;

        //secondClicked points to the second Label control
        //that a player clicks.  
        Label secondClicked = null;

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }
        //Use this random object to choose random icons for the squares.
        Random random = new Random();

        //Each of these letters is an interesting icon
        //in the Webdings font,
        //and each icon appears twice in the list.
        List<string> icons = new List<string>()
        {
            "!","!","N","N",",",",","k","k","b","b","v","v","w","w","z","z"
        };
        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                //The statements that you want to execute
                //for each label go here 
                //The statements use iconLabel to access
                //each label's properties and methods.
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        //Every label's click event is handled by this event handler
        private void label_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                //If the click label is black, the player clicked 
                //an icon that's already been revealed --
                //ignor the click.
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                //If firstClicked is null, this is the first icon 
                //in the pair that the player clicked, 
                //so set the firstClicked to the label that the player
                //clicked, chnage its color to black, and return
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                //If the player gets this far, the timer isn't
                //and the firstClicked isn't null,
                //so this must be the second icon that the player 
                //clicked .  Set its color to black 
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                //Check to see if the player won.
                CheckForWinner();

                //If the player clicked two matching icons, keep them
                //black and reset firstClicked and secondlclicked 
                //so that player can click another icon.  
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                //If the player gets this far, the player; 
                //clicked two different icons, so start the
                //timer (which will wait three quarters of 
                //a second, and then hide the icons)
                timer1.Start();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            //Stop the timer
            timer1.Stop();

            //Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            //Reset firstClicked and secondClicked
            //so that the next time a label is
            //clicked, the program know it's the first click
            firstClicked = null;
            secondClicked = null;


        }
        //Check every icon to see if it is matched, by
        //compairing its foreground color to its background color.
        //If all the icons are matched, then the player wins
        private void CheckForWinner()
        {
            //Go through all of the labels in tableLayoutPanel1,
            //checking each one to see if its item icon is matched
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                    {
                        return;
                    }
                }
                //If the loop didn't return, it didn't find
                //any unmatched icons
                //That means the user won.  Show a message and close the form
            }
            MessageBox.Show("You matched all the icons!", "Congratulations!");
            Close();
        }
    }
}
