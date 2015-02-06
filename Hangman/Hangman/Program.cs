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
        static string hangingMan = "::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::\n" +
"::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::\n" +
"::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::\n" +
"::::::::::::::::::::::::::::::-------:::::::::::::::::::::::::::::::::\n" +
":::::::::::::::::::::::::::::::+:::o::::::::::::::::::::::::::::::::::\n" +
":::::::::::::::::::::::::::::::+::/|\\::::::::::::::::::::::::::::::::\n" +
":::::::::::::::::::::::::::::::+:::|::::::::::::::::::::::::::::::::::\n" +
":::::::::::::::::::::::::::::::+::/:\\::::::::::::::::::::::::::::::::\n" +
"::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::\n" +
"::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::\n" +
"::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::";
        static Random randGen = new Random();
        static string WordToGuess = null;
        static int turns = 0;
        public static string user_PlayingField { get; set; }  // this is going to be where the masked word stored
        static string wrongLetters = null;
        public static string letterGuessed { get; set; }

        static void Main(string[] args)
        {
            string GuessedLetter = null;
            bool quitter = true;
            string manHanging = new string(hangingMan.ToArray());  // an instance of the hanging man

            WordToGuess = dictionary[randGen.Next(0, dictionary.Length)];

            initializer(ref manHanging); // setup the game
            PrintScreen_Title();
            GetUserName();
            PrintScreen_instructions();

            while (quitter)
            {
                // title screen
                PrintScreen_Title();
                // Prints everything that user eyes
                PrintGame(manHanging);
                GuessedLetter = GetUserInput();
                // see if the guessedLetter is in the wordToGuess
                // the game is done
                if (IsInWord(GuessedLetter, ref manHanging) || turns== 0)
                {
                    quitter = printQuit();
                    if (quitter)
                    {
                        initializer(ref manHanging);
                    }
                }
            }
        }

        /// <summary>
        /// this will set up or reset the game variables
        /// </summary>
        /// <param name="man">I want to be able to change the current hang man</param>
        static void initializer(ref string man)
        {
            string tempString = null;
            turns = 7;
            wrongLetters = null;
            Console.SetWindowSize(Console.WindowWidth, Console.LargestWindowHeight);
            WordToGuess = dictionary[randGen.Next(0, dictionary.Length)];
            man = new string(hangingMan.ToArray());

            // setting up the blank underscored spaces
            for (int i = 0; i < WordToGuess.Length; i++)
            {
                tempString += "_";
            }

            user_PlayingField = new string(tempString.ToArray());
        }

        
         
        /// <summary>
        /// if the user guesses the word return true and the game is won
        /// if the user enters in a number or some kind of sysmbol return false
        /// if the user puts a letter and search the wordToSearch, where ever the index of that letter is, copy the letter and place it at the
        /// </summary>
        /// <param name="guess">user input</param>
        /// <param name="Deadman">ref to the hang man</param>
        /// <returns></returns>
        static bool IsInWord(string guess, ref string Deadman)
        {
            int part = 0;  // this does nothing for this function
            char[] fieldHolder = user_PlayingField.ToArray();
            string message_Wrong = "'##:::::'##:'########:::'#######::'##::: ##::'######:::\n" +
" ##:'##: ##: ##.... ##:'##.... ##: ###:: ##:'##... ##::\n" +
" ##: ##: ##: ##:::: ##: ##:::: ##: ####: ##: ##:::..:::\n" +
" ##: ##: ##: ########:: ##:::: ##: ## ## ##: ##::'####:\n" +
" ##: ##: ##: ##.. ##::: ##:::: ##: ##. ####: ##::: ##::\n" +
" ##: ##: ##: ##::. ##:: ##:::: ##: ##:. ###: ##::: ##::\n" +
". ###. ###:: ##:::. ##:. #######:: ##::. ##:. ######:::\n" +
":...::...:::..:::::..:::.......:::..::::..:::......::::";
            char[] newDeadman = Deadman.ToArray();

            // it's a number
            if (int.TryParse(guess, out part) )
            {
                PrintASCII(message_Wrong);
                return false;
            }
            // connect match
            if (guess == WordToGuess || user_PlayingField == WordToGuess)
            {
                Console.WriteLine("You win!!");
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
                                fieldHolder[i] = guess[0];
                            }
                        }
                        user_PlayingField = new string(fieldHolder);
                    } else 
                    {
                        part = randGen.Next(1, turns);
                        wrongLetters += guess;
                        // messed up logic but it makes sense
                        // do something
                        // take head if temp
                        if (part == 1)
                        {
                            for (int i = 0; i < Deadman.Length; i++)
                            {
                                if (Deadman[i] == 'o')
                                {
                                    Deadman.ToArray()[i] = ':';
                                }
                            }
                            part++;
                        }
                        // left leg and left arm
                        if (part == 2 || part == 4)
                        {
                            for (int i = 0; i < Deadman.Length; i++)
                            {
                                if (Deadman[i] == '/')
                                {
                                    newDeadman[i] = ':';
                                }
                            }
                            turns--;
                        }
                        // torso
                        if (part == 3)
                        {
                            for (int i = 0; i < Deadman.Length; i++)
                            {
                                if (Deadman[i] == '/')
                                {
                                    newDeadman[i] = ':';
                                }
                            }
                            turns--;
                        }
                        // right arm and right leg
                        if (part == 5 || part == 6)
                        {
                            for (int i = 0; i < Deadman.Length; i++)
                            {
                                if (Deadman[i] == '\\')
                                {
                                    newDeadman[i] = ':';
                                }
                            }
                            turns--;
                        }
                    }
                }
            }

            Deadman = new string(newDeadman);
            return false;
        }

        /// <summary>
        ///  print letter guessed and guesses left (got my playing field)
        /// </summary>
        /// <param name="DeadMan">ref the hang man</param>

        static void PrintGame(string DeadMan)
        {
            // the dead man
            PrintASCII(DeadMan);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
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

        /// <summary>
        /// user input and returning a string for that input (got my user's input)
        /// </summary>
        /// <returns>The user's input</returns>
 
        static string GetUserInput()
        {
            string message = name_Player + ", please enter a letter or word: ";

            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(SPEED);
            }
            return Console.ReadLine();
        }

        /// <summary>
        /// prompts the user to quit (got quit screen)
        /// </summary>
        /// <returns>quit or not</returns>
 
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
        
        /// <summary>
        /// ask for the user's name and store it in name_Player
        /// </summary>
        static void GetUserName()
        {
            string message = "HI, HOW ARE YOU DOING? WHAT'S YOUR NAME? ";
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(SPEED);
            }
            name_Player = Console.ReadLine();
            if (name_Player == "")
            {
                name_Player = "You";
            }
        }


        /// <summary>
        /// print title screen
        /// </summary>
        static void PrintScreen_Title()
        {
            //string title = "THE HANGING MAN";
            string niceTilte =
"'##::::'##::::'###::::'##::: ##::'######:::'####:'##::: ##::'######:::\n" +
" ##:::: ##:::'## ##::: ###:: ##:'##... ##::. ##:: ###:: ##:'##... ##::\n" +
" ##:::: ##::'##:. ##:: ####: ##: ##:::..:::: ##:: ####: ##: ##:::..:::\n" +
" #########:'##:::. ##: ## ## ##: ##::'####:: ##:: ## ## ##: ##::'####:\n" +
" ##.... ##: #########: ##. ####: ##::: ##::: ##:: ##. ####: ##::: ##::\n" +
" ##:::: ##: ##.... ##: ##:. ###: ##::: ##::: ##:: ##:. ###: ##::: ##::\n" +
" ##:::: ##: ##:::: ##: ##::. ##:. ######:::'####: ##::. ##:. ######:::\n" +
"..:::::..::..:::::..::..::::..:::......::::....::..::::..:::......::::\n";

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("THE");
            PrintASCII(niceTilte);
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Console.WindowWidth - 12, 9);
            Console.WriteLine("MAN");
        }

        /// <summary>
        /// Fill in a solid space in the string
        /// </summary>
        /// <param name="color"></param>
        static void FillInColor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.ForegroundColor = color;
        }

        /// <summary>
        /// Prints the ASCII characters
        /// </summary>
        /// <param name="text"></param>
        static void PrintASCII(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                // if ' ' shadow
                if (text[i] == ' ')
                {
                    FillInColor(ConsoleColor.DarkGray);
                }
                // if  ' transistion color to the shadow color
                if (text[i] == '\'' || text[i] == '.')
                {
                    FillInColor(ConsoleColor.Gray);
                }
                // if # color it in
                if (text[i] == '#')
                {
                    FillInColor(ConsoleColor.DarkRed);
                }
                // if : color in with the background color
                if (text[i] == ':')
                {
                    FillInColor(ConsoleColor.DarkBlue);
                }
                if (text[i] == '-' || text[i] == '+')
                {
                    FillInColor(ConsoleColor.DarkYellow);
                }
                // this is the color for the hanging man
                if (text[i] == '|' || text[i] == 'o' || text[i] == '/'|| text[i] =='\\')
                {
                    FillInColor(ConsoleColor.DarkBlue);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write(text[i]);
            }
        }

        /// <summary>
        /// print instructions
        /// </summary>
        static void PrintScreen_instructions()
        {
            string instructions = "To Play:\n1) I'll think of a word.\n2) " + name_Player.ToUpper() + 
                " will guess a letter\n3) If "+ name_Player.ToUpper() + " guess correctly, I'll give "+ name_Player +
                "\nthat letter to help you figure out the word\nIf " + name_Player + " guess wrong, I'll take a body part";

            Console.WriteLine();
            for (int i = 0; i < instructions.Length; i++){
                Console.Write(instructions[i]);
                System.Threading.Thread.Sleep(50);
            }
            Console.WriteLine();
            System.Threading.Thread.Sleep(3000);
        }
    }
}
