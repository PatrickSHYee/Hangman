using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        // Constant variables
        const int SPEED = 100;

        // global variables
        static string name_Player = null;
        static string[] dictionary = { "gruesome", "bloosom", "processor", "bacon", "enlightenment" };
        static string[] hangingMan = { "Head", "Torso", "Left Arm", "Right Arm", "Left Leg", "Right Leg", "Freebie" };
        static Random randGen = new Random();
        static string WordToGuess = null;
        static string user_PlayingField = null;  // this is going to be where the masked word stored
        static string wrongLetters = null;
        public static string letterGuessed { get; set; }

        static void Main(string[] args)
        {
            string GuessedLetter = null;
            bool quitter = true;
            //bool guessing = true;
            List<string> manHanging = new List<string>();  // an instance of the hanging man

            WordToGuess = dictionary[randGen.Next(0, dictionary.Length)];
            PrintScreen_Title();
            GetUserName();
            PrintScreen_instructions();

            initializer(ref manHanging); // setup the game

            while (quitter)// || manHanging.Count >= 0)
            {
                Console.Clear();
                //title screen
                PrintScreen_Title();
                PrintHangingMan(manHanging);
                GuessedLetter = GetUserInput();
                // see if the guessedLetter is in the wordToGuess

                // Quit prompt
                if (manHanging.Count == 0)
                {
                    quitter = printQuit();
                    if (quitter)
                    {
                        initializer(ref manHanging);
                    }
                }
            }
        }

        // this will set up or reset the game variables
        static void initializer(ref List<string> man)
        {
            WordToGuess = dictionary[randGen.Next(0, dictionary.Length)];

            // setting up the blank underscored spaces
            for (int i = 0; i < WordToGuess.Length; i++)
            {
                user_PlayingField += "_";
            }

            // setting up the hanging man
            if (man.Count != hangingMan.Length)
            {
                man = hangingMan.ToList<string>();
            }
        }

        // if the user guesses the word return true and the game is won
        // if the user enters in a number or some kind of sysmbol return false
        // if the user puts a letter and search the wordToSearch, where ever the index of that letter is, copy the letter and place it at the
        //   same index in 
        static bool isInWord(string guess)
        {

        }

        // print letter guessed and guesses left (got my playing field)
        static void PrintHangingMan(List<string> DeadMan)
        {
            // the dead man
            Console.Write("{0} these are the parts left are ", name_Player);
            for (int i = 0; i < DeadMan.Count; i++)
            {
                Console.Write(DeadMan[i] + " ");
                System.Threading.Thread.Sleep(SPEED);
            }
            Console.WriteLine();
            // the correct letters
            Console.Write("{0}, please enter a letter: ", name_Player);

            // the wrong letters
            Console.WriteLine("{0}'s letters guessed: ", name_Player);
            if (wrongLetters != null)
            {
                for (int i = 0; i < wrongLetters.Length; i++)
                {
                    Console.Write(wrongLetters[i] + " ");
                    System.Threading.Thread.Sleep(SPEED);
                }
            }
        }

        // user input and returning a string for that input (got my user's input)
        static string GetUserInput()
        {
            string message = name_Player + "Please enter a letter: ";

            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(SPEED);
            }
            return Console.ReadLine();
        }

        // prompts the user to quit (got quit screen)
        static bool printQuit()
        {
            Console.Write("Quitter? (yes or no) ");
            string inputUser = Console.ReadLine().ToLower();

            if (inputUser == "yes")
            {
                return false;
            }
            return true;
        }
        
        // ask for the user's name and store it in name_Player
        static void GetUserName()
        {
            string message = "HI, HOW ARE YOU DOING? WHAT'S YOUR NAME? ";
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(SPEED);
            }
            name_Player = Console.ReadLine();
        }


        // print title screen
        static void PrintScreen_Title()
        {
            string title = "THE HANGING MAN";

            for (int i = 0; i < title.Length; i++)
            {
                Console.Write(title[i]);
                System.Threading.Thread.Sleep(SPEED);
            }
            Console.WriteLine();
        }

        // print instructions
        static void PrintScreen_instructions()
        {
            string instructions = "This version of Hangman, we are going to take our hanging man off his death pole.\n" + name_Player.ToUpper() +"'s task is to guess letters individually or guess the whole word. This will grant the man a safe way off his death pole. If you mess up and guess wrong, I'll take off one of the body parts off.  If I decide to take the head, it means you loose the game.";
            string instructions2 = "To Play:\n1) I'll think of a word.\n2) " + name_Player.ToUpper() + ", the player, will guess a letter\n3) If you guess correctly, I'll give you that letter to help you figure out the word";

            for (int i = 0; i < instructions.Length; i++)
            {
                Console.Write(instructions[i]);
                System.Threading.Thread.Sleep(SPEED);
            }
            Console.WriteLine();
            for (int i = 0; i < instructions2.Length; i++){
                Console.Write(instructions2[i]);
                System.Threading.Thread.Sleep(SPEED);
            }
            Console.WriteLine();
            System.Threading.Thread.Sleep(1000);
        }
    }
}
