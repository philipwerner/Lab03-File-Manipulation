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
            MainMenu();
        }

        /// <summary>
        /// the initial user interface of the program
        /// </summary>
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

        /// <summary>
        /// deletes existing wordList.txt file, then creates a new version of
        /// the file.
        /// </summary>
        /// <param name="words">string type of the words to be added to the new file</param>
        public static void CreateFile(string words)
        {
            DeleteWordList();
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

        /// <summary>
        /// creates a string of all the words in the wordList.txt file to be used
        /// by other methods
        /// </summary>
        /// <returns>string type of the wordList.txt file</returns>
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

        /// <summary>
        /// prompts and stores the word the user wants to delete, then
        /// passes it to the EditList method
        /// </summary>
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

        /// <summary>
        /// converts string of word list into an array, checks that the word is in the string,
        /// if word is in the string, creates a new array without the deleted word and passes it
        /// to the CreateFile method. If word is not in the list, it informs the user.
        /// </summary>
        /// <param name="word">string type of all the words in the list</param>
        public static void EditList(string word)
        {
            string wordList = CreateStringOfWords();
            char[] seperators = { ' ', ',', ';', '.', '\t', ':' };
            string[] wordArray = wordList.Split(seperators);
            string[] newArray = new string[wordArray.Length - 1];
            int arrayLength = newArray.Length;
            int counter = arrayLength;
            if (wordList.Contains(word))
            {
                // adds all non deleted words to a new array
                for (int i = 0; i < wordArray.Length; i++)
                {
                    if (wordArray[i] == word)
                    {
                        // skips over the word to be deleted
                        i = i + 1;
                    }
                    if (wordArray[i] != word)
                    {
                        // counter is depricated to give proper index
                        newArray[arrayLength - counter] = wordArray[i];
                        counter--;
                    }
                }
            }
            else
            {
                Console.WriteLine("The word does not exist!");
            }
            string words = string.Join(" ", newArray);
            CreateFile(words);
            
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
        /// deletes the word list txt file
        /// </summary>
        public static void DeleteWordList()
        {
            File.Delete("..//wordList.txt");
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

        public static int FindLengthOfList()
        {
            string wordList = CreateStringOfWords();
            char[] seperators = { ' ', ',', ';', '.', '\t', ':' };
            string[] wordArray = wordList.Split(seperators);
            int length = wordArray.Length;
            return length;
        }

        public static string PickTheWord()
        {
            int length = FindLengthOfList();
            int index = RandomNumber(length);
            string wordList = CreateStringOfWords();
            char[] seperators = { ' ', ',', ';', '.', '\t', ':' };
            string[] wordArray = wordList.Split(seperators);
            string gameWord = wordArray[index];
            return gameWord;
        }

        public static void StartGame()
        {
            string mysteryWord = PickTheWord();
            GuessLetter(mysteryWord);
        }

        public static void GuessLetter(string word)
        {
            string mysteryWord = word;
            Console.WriteLine("Would you like to guess 1 or 2 words?");
            string amount = Console.ReadLine();
            if (amount == "1")
            {
                Console.WriteLine("OK, guessing 1 letter");
                Console.WriteLine("What letter are you guessing?");
                char oneLetter = Convert.ToChar(Console.ReadLine().ToLower());
                AddGuessedLetters(oneLetter);
                OneLetterGuess(oneLetter, mysteryWord);
            }
            if (amount == "2")
            {
                Console.WriteLine("OK, guessing 2 letters");
                Console.WriteLine("Enter the first letter to guess");
                char firstLetter = Convert.ToChar(Console.ReadLine().ToLower());
                AddGuessedLetters(firstLetter);
                Console.WriteLine("What is the second letter?");
                char secondLetter = Convert.ToChar(Console.ReadLine().ToLower());
                AddGuessedLetters(secondLetter);
                TwoLetterGuess(firstLetter, secondLetter, mysteryWord);
            }
            if (amount != "1" || amount != "2")
            {
                Console.WriteLine("Please respond with 1 or 2.");
                GuessLetter(mysteryWord);
            }
        }

        public static void CreateGuessedLetterList()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("..//guessedList.txt"))
                {
                    sw.Write("Guessed Letters");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void AddGuessedLetters(char letter)
        {
            try
            {
                using (StreamWriter sw = File.AppendText("..//guessedList.txt"))
                {
                    sw.WriteLine(letter);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void OneLetterGuess(char letter, string word)
        {
            string guessed = Convert.ToString(letter);
            if (word.Contains(guessed))
            {
                Console.WriteLine("The mystery word does contain that letter");
            }
            else
            {
                Console.WriteLine("Nope");
            }
        }

        public static void TwoLetterGuess(char letterOne, char letterTwo, string word)
        {
            string guessOne = Convert.ToString(letterOne);
            string guessTwo = Convert.ToString(letterTwo);
            if (word.Contains(guessOne) && word.Contains(guessTwo))
            {
                Console.WriteLine($"The mystery word contains both {guessOne} and {guessTwo}.");
            }
            else if (word.Contains(guessOne))
            {
                Console.WriteLine($"The mystery word does contain {guessOne}.");
            }
            else if (word.Contains(guessTwo))
            {
                Console.WriteLine($"The mystery word does contain {guessTwo}.");
            }
            else
            {
                Console.WriteLine($"The mystery word does not contain {guessOne} or {guessTwo}.");
            }
        }

        public static void WordStatus(string word, string letter, string correct)
        {
            string[] letters = word.Split(' ');
            string[] correctLetters = correct.Split(' ');
            for (int i = 0; i < correctLetters.Length; i++)
            {
                if (letters[i] == letter)
                {
                    correctLetters[i] = letter;
                }
                if (letters[i] != letter)
                {
                    correctLetters[i] = "_";
                }
                foreach (string s in correctLetters)
                {
                    Console.Write(s);
                }
                correct = String.Join("", correctLetters);
                
            }
            
        }
    }
}
