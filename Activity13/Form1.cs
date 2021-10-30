using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Activity13
{
    public partial class Form1 : Form
    {

        private char[,] board; // A two-dimensional array of characters that will represent the board. Made with help from the textbook (Gaddis, 445)
        private char result; // A string that will contain the result of the game

        public Form1()
        {
            InitializeComponent();
        }

        // This code runs when the NewGame button is clicked
        private async void NewGame_Click(object sender, EventArgs e)
        {
            ClearBoard(); // The ClearBoard method is called
            await Task.Delay(1500); // The code is delayed for 1.5 seconds. Made with help from Microsoft documentation (n.d.)

            while (!GameOver(out result)) // This loop runs until the game is over. The letters X and O are played in turn until the game is over
            {
                if(!GameOver(out result)) // If the game is not over, the letter X is played. If the game is over, the result variable is set to the winning letter
                {
                    ResultLabel.Text = "X playing..."; // The text of the ResultLabel is set to notify the user that X is playing
                    PlayLetter('X'); // The PlayLetter method is called with the letter X as its parameter
                }

                await Task.Delay(1500); // The code is delayed for 1.5 seconds. Made with help from Microsoft documentation (n.d.)
                DisplayBoard(); // Displays the board in the GUI

                if (!GameOver(out result)) // If the game is not over, the letter O is played. If the game is over, the result variable is set to the winning letter
                {
                    ResultLabel.Text = "O playing..."; // The text of the ResultLabel is set to notify the user that O is playing
                    PlayLetter('O'); // The PlayLetter method is called with the letter O as its parameter
                }

                await Task.Delay(1500); // The code is delayed for 1.5 seconds. Made with help from Microsoft documentation (n.d.)
                DisplayBoard(); // Displays the board in the GUI
            }

            if (result != ' ') // If the result variable does not contain an empty space the text of the ResultLabel is set to to a string that contains the winning letter
            {
                ResultLabel.Text = result.ToString() + " wins.";
            }
            else // If the result variable does contain an empty space the text of the ResultLabel is set to a string telling the user that the result was a tie
            {
                ResultLabel.Text = "The game is a tie.";
            }

            DisplayBoard(); // Displays the board in the GUI
        }

        // This method displays the board in the GUI
        private void DisplayBoard()
        {
            // The text of the labels that represent the upper row are set to the corresponding items in the board array
            UpperLeft.Text = board[0,0].ToString();
            UpperCenter.Text = board[0, 1].ToString();
            UpperRight.Text = board[0, 2].ToString();

            // The text of the labels that represent the center row are set to the corresponding items in the board array
            CenterLeft.Text = board[1, 0].ToString();
            Center.Text = board[1, 1].ToString();
            CenterRight.Text = board[1, 2].ToString();

            // The text of the labels that represent the lower row are set to the corresponding items in the board array
            LowerLeft.Text = board[2, 0].ToString();
            LowerCenter.Text = board[2, 1].ToString();
            LowerRight.Text = board[2, 2].ToString();
        }

        // This method determines whether or not the game is over. Made with help from the textbook (Gaddis, 367)
        private bool GameOver(out char letter)
        {
            
            if (board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2] && board[0,2] != ' ') // Checks if the the upper row contains all the same characters and if that character is not a space
            {
                letter = board[0,0]; // The output parameter is set to the winning character
                return true;
            }
            else if (board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2] && board[1, 2] != ' ') // Checks if the the middle row contains all the same characters and if that character is not a space
            {
                letter = board[1, 0]; // The output parameter is set to the winning character
                return true;
            }
            else if (board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2] && board[2, 2] != ' ') // Checks if the the lower row contains all the same characters and if that character is not a space
            {
                letter = board[2, 0]; // The output parameter is set to the winning character
                return true;
            }
            else if (board[0, 0] == board[1, 0] && board[1, 0] == board[2, 0] && board[2, 0] != ' ') // Checks if the the left column contains all the same characters and if that character is not a space
            {
                letter = board[0, 0]; // The output parameter is set to the winning character
                return true;
            }
            else if (board[0, 1] == board[1, 1] && board[1, 1] == board[2, 1] && board[2, 1] != ' ') // Checks if the the center column contains all the same characters and if that character is not a space
            {
                letter = board[0, 1]; // The output parameter is set to the winning character
                return true;
            }
            else if (board[0, 2] == board[1, 2] && board[1, 2] == board[2, 2] && board[2, 2] != ' ') // Checks if the the right column contains all the same characters and if that character is not a space
            {
                letter = board[0, 2]; // The output parameter is set to the winning character
                return true;
            }
            else if(board[0,0] == board[1,1] && board[1,1] == board[2,2] && board[2, 2] != ' ') // Checks if the the diagonal from the upper-left corner to the lower-right contains all the same characters and if that character is not a space
            {
                letter = board[0, 0]; // The output parameter is set to the winning character
                return true;
            }
            else if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[2, 0] != ' ') // Checks if the the diagonal from the upper-right corner to the lower-left contains all the same characters and if that character is not a space
            {
                letter = board[0, 2]; // The output parameter is set to the winning character
                return true;
            }
            else if(IsTie()) // Checks if the result is a tie. The logic of determining a tie is much different than that for determining a win. Therefore, a method is dedicated to it
            {
                letter = ' '; // The output parameter is set to a space
                return true;
            }
            else // If the result is not a tie or win, then the game is not over
            {
                letter = ' '; // The output parameter is set to a space
                return false;
            }

        }

        // This method checks if the game is tied
        private bool IsTie()
        {
            for (int i = 0; i < 3; i++) // This nested loop checks every item in the board array. If any item is an empty space then the board is not full and the game is not tied
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }

            return true; // If none of the items in the array are an empty space then the board is full and the game is a tie
        }

        // This method adds a letter (an X or an O) to the board
        private void PlayLetter(char letter)
        {
            Random randomGenerator1 = new Random(); // Creates and initializes a new Random object
            Random randomGenerator2 = new Random(randomGenerator1.Next()); // Creates and initializes a new Random object. By default, Random objects have as their seed the time at which they are created. To prevent the two Random objects from having the same seed, the second uses a number generated by the first as a seed
            int[] cell = new int[2] { randomGenerator1.Next(3), randomGenerator2.Next(3) }; // Creates and initializes an array of integers with two items. This array will represent a random cell in the board. It is initialized with two random values generated by the above Random objects 

            while(board[cell[0], cell[1]] != ' ') // This loop randomly selects cells in the board until an empty cell is found
            {
                cell[0] = randomGenerator1.Next(3);
                cell[1] = randomGenerator2.Next(3);
            }

            board[cell[0], cell[1]] = letter; // Adds the letter to the cell

        }

        // This method sets the board array to contain empty spaces and then displays the board, effectively clearing the board
        private void ClearBoard()
        {
            board = new char[,]
            {
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' }
            }; // Initializes the board array as a three by three array of empty spaces. Made with help from the textbook (Gaddis, 447)
            ResultLabel.Text = " "; // Clears the text of the ResultLabel

            DisplayBoard(); // Displays the board in the GUI
        }
    }
}

// References:
// Gaddis, T. (2020). Starting Out With Visual C#. Pearson.
// Microsoft. (n.d.). Task.Delay Method. https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.delay?view=net-5.0