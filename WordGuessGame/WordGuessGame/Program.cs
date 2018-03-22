using System;
using System.IO;
using System.Text;

namespace WordGuessGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To TheWord Guess Game!");
        }

        public static void MainMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Play A New Game");
            Console.WriteLine("2. Add a new word");
            Console.WriteLine("3. Edit word list");
            Console.WriteLine("4. View word list");
            Console.WriteLine("5. Exit the game");
            byte option = Convert.ToByte(Console.ReadLine());
        }

        public static void CreateFile(string words)
        {
            char[] seperators = { ' ', ',', ';', '.', '\t', ':' };
            string[] wordArray = words.Split(seperators);
            try
            {
                using (StreamWriter sw = new StreamWriter("..//wordList.txt"))
                {
                    foreach (string s in wordArray)
                    {
                        
                        sw.Write(s);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// displays the list of words in the console for the user
        /// </summary>
        public static void ViewWordList()
        {
            try
            {
                using (StreamReader sr = File.OpenText("..//wordList.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string CreateStringOfWords()
        {
            try
            {
                using (StreamReader sr = File.OpenText("..//wordList.txt"))
                {
                    string s = "";
                    string words = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        words = s + " ";   
                    }
                    return words;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
        public static Array[] CreateArrayOfWords()
        {
            string words = CreateStringOfWords();
            char[] seperators = { ' ', ',', ';', '.', '\t', ':' };
            string[] wordArray = words.Split(seperators);
            return wordArray;
        }
        */
        public static void GetRemoveWord()
        {
            Console.WriteLine("What word do you want to remove?");
            string word = Console.ReadLine().ToLower();
            if (word.Contains(" "))
            {
                Console.WriteLine("Please enter a single word.");
                GetRemoveWord();
            }
            EditList(word);
        }

        public static void EditList(string word)
        {
            string wordList = CreateStringOfWords();
            if (wordList.Contains(word))
            {
                
            }
            else
            {
                Console.WriteLine("The word does not exist!");
            }
            
        }

        /// <summary>
        /// prompts the user about what word they want to add, sends input to AddNewWord method
        /// </summary>
        public static void GetNewWord()
        {
            Console.WriteLine("What word would you like to add?");
            string newWord = Console.ReadLine().ToLower();
            if (newWord.Contains(" "))
            {
                Console.WriteLine("Please enter a single word.");
                GetNewWord();
            }
            AddNewWord(newWord);
        }

        /// <summary>
        /// takes the users new word and appends it to the word list
        /// </summary>
        /// <param name="word">string type of the users new word</param>
        /// <returns>a string letting the user know the word has been added</returns>
        public static string AddNewWord(string word)
        {
            using (StreamWriter sw = File.AppendText("..//wordList.txt"))
            {
                sw.WriteLine(word);
            }
            return $"Your new word, {word}, has been added to the list.";
        }

        /// <summary>
        /// creates a random number within the length of the word list
        /// </summary>
        /// <param name="length">int type of the length of the word list array</param>
        /// <returns>int type of the random index to be selected</returns>
        public static int RandomNumber(int length)
        {
            Random rand = new Random();
            int randNum = rand.Next(0, length);
            return randNum;
        }
    }
}
