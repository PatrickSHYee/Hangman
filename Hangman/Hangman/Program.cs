using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        static string name_Player = null;

        static void Main(string[] args)
        {
            string[] dictionary = {"gruesome", "bloosom", "processor", "bacon", };
            string[] hangingMan = { "Head", "Torso", "Left Arm", "Right Arm", "Left Leg", "Right Leg" };

        }
        
        // ask for the user's name and store it in name_Player
        static void GetUserName()
        {
            Console.WriteLine("HI, HOW ARE YOU DOING? WHAT'S YOUR NAME? ");
            name_Player = Console.ReadLine();
        }


        // print title screen
        static void PrintScreen_Title()
        {
            string title = "Getting Hangman";
        }

        // print instructions
        static void PrintScreen_instructions()
        {
            string instructions = "This version of Hangman, we are going to take our hanging man off his thingy.  You have the same amount of turns to guess your. To kick up the notch, if you guesss wrong and the head is removed before you guessed the word. You still loose.";
            string instructions2 = "To play the game:\nI'll think of a word and you guess it by giving me letters.  If that letter is that word, I'll something for it.";
        }

        // print letter guessed and guesses left

        // user input and validate if a letter was put in.

    }
}
