﻿using System;
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
        public static string user_PlayingField { get; set; }  // this is going to be where the masked word stored
        static string wrongLetters = null;
        public static string letterGuessed { get; set; }

        static void Main(string[] args)
        {
            string GuessedLetter = null;
            bool quitter = true;
            //bool guessing = true;
            List<string> manHanging = new List<string>();  // an instance of the hanging man

            WordToGuess = dictionary[randGen.Next(0, dictionary.Length)];

            initializer(ref manHanging); // setup the game
            PrintScreen_Title();
            GetUserName();
            PrintScreen_instructions();

            while (quitter)// || manHanging.Count >= 0)
            {
                Console.Clear();
                //title screen
                PrintScreen_Title();
                PrintGame(manHanging);
                GuessedLetter = GetUserInput();
                // see if the guessedLetter is in the wordToGuess
                isInWord(GuessedLetter);

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
            Console.SetWindowSize(Console.WindowWidth, Console.LargestWindowHeight);
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
            int temp = 0;  // this does nothing for this function
            string fieldHolder = null;

            // it's a number
            if (int.TryParse(guess, out temp))
            {
                return false;
            }
            // connect match
            if (guess == WordToGuess)
            {
                return true;
            }

            // checks the lengh of the guess
            if (guess.Length == 1 )
            {
                if (Char.IsLetter(guess[0]))
                {
                    if (WordToGuess.Contains(guess))
                    {
                        for (int i = 0; i < WordToGuess.Length; i++)
                        {
                            if (WordToGuess[i] == guess[0])
                            {
                                fieldHolder += guess[0];
                            }
                            else
                            {
                                fieldHolder += "_";
                            }
                        }
                        user_PlayingField = fieldHolder;
                    }
                    else
                    {
                        wrongLetters += guess;
                    }
                }
            }
            return false;
        }

        // print letter guessed and guesses left (got my playing field)
        static void PrintGame(List<string> DeadMan)
        {
            // the dead man
            Console.Write("{0} these are the parts left are\n", name_Player);
            for (int i = 0; i < DeadMan.Count; i++)
            {
                Console.Write(DeadMan[i] + " ");
                System.Threading.Thread.Sleep(SPEED);
            }
            Console.WriteLine();
            // the correct letters
            for (int i = 0; i < user_PlayingField.Length; i++)
            {
                Console.Write(user_PlayingField[i]);
                if (i != user_PlayingField.Length - 1)
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
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
            Console.WriteLine();
        }

        // user input and returning a string for that input (got my user's input)
        static string GetUserInput()
        {
            string message = name_Player + ", please enter a letter: ";

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
            string instructions = "This version of Hangman, we are going to take our hanging man off his death pole.  " + name_Player.ToUpper() +"'s task is to guess letters individually or guess the whole word. This will grant the man a safe way off his death pole. If you mess up and guess wrong, I'll take off one of the body parts off.  If I decide to take the head, it means you loose the game.";
            string instructions2 = "To Play:\n1) I'll think of a word.\n2) " + name_Player.ToUpper() + " will guess a letter\n3) If you guess correctly, I'll give you that letter to help you figure out the word";

            for (int i = 0; i < instructions.Length; i++)
            {
                Console.Write(instructions[i]);
                //System.Threading.Thread.Sleep(50);
            }
            Console.WriteLine();
            for (int i = 0; i < instructions2.Length; i++){
                Console.Write(instructions2[i]);
                //System.Threading.Thread.Sleep(50);
            }
            Console.WriteLine();
            System.Threading.Thread.Sleep(3000);
        }
    }
}
