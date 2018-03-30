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
            string myPath = "wordList.txt";
            MainMenu(myPath);
        }
        
        /// <summary>
        /// the initial user interface of the program
        /// </summary>
        public static void MainMenu(string path)
        {
            try
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Play A New Game");
                Console.WriteLine("2. Add a new word");
                Console.WriteLine("3. Remove a word from list");
                Console.WriteLine("4. View word list");
                Console.WriteLine("5. Exit the game");
                int option = Convert.ToInt32(Console.ReadLine());
                HandleOption(option, path);
            }
            catch (Exception e)
            {
                Console.WriteLine("Please eneter a number between 1 and 5.");
                MainMenu(path);
            }
        }

        /// <summary>
        /// Takes the user selection and runs the corresponding method
        /// </summary>
        /// <param name="option">byte type of user selection</param>
        public static void HandleOption(int option, string path)
        {
            if (option == 1)
            {
                StartGame(path);
            }
            if (option == 2)
            {
                GetNewWord(path);
            }
            if (option == 3)
            {
                GetRemoveWord(path);
            }
            if (option == 4)
            {
                ViewWordList(path);
            }
            if (option == 5)
            {
                Environment.Exit(0);
            }
            if (option > 5)
            {
                Console.WriteLine("Please choose an option 1-5");
                MainMenu(path);
            }
        }

        /// <summary>
        /// deletes existing wordList.txt file, then creates a new version of
        /// the file.
        /// </summary>
        /// <param name="words">string type of the words to be added to the new file</param>
        public static void CreateFile(string words, string path)
        {
            DeleteWordList(path);
            char[] seperators = { ' ', ',', ';', '.', '\t', ':' };
            string[] wordArray = words.Split(seperators);
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
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
        public static void ViewWordList(string path)
        {
            try
            {
                using (StreamReader sr = File.OpenText(path))
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
            Console.WriteLine("Press any key to continue to Main Menu.....");
            Console.ReadKey();
            MainMenu(path);
        }

        /// <summary>
        /// creates a string of all the words in the wordList.txt file to be used
        /// by other methods
        /// </summary>
        /// <returns>string type of the wordList.txt file</returns>
        public static string CreateStringOfWords(string path)
        {
            try
            {
                using (StreamReader sr = File.OpenText(path))
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
        public static void GetRemoveWord(string path)
        {
            Console.WriteLine("What word do you want to remove?");
            string word = Console.ReadLine().ToLower();
            if (word.Contains(" "))
            {
                Console.WriteLine("Please enter a single word.");
                GetRemoveWord(path);
            }
            EditList(word, path);
        }

        /// <summary>
        /// converts string of word list into an array, checks that the word is in the string,
        /// if word is in the string, creates a new array without the deleted word and passes it
        /// to the CreateFile method. If word is not in the list, it informs the user.
        /// </summary>
        /// <param name="word">string type of all the words in the list</param>
        public static void EditList(string word, string path)
        {
            string wordList = CreateStringOfWords(path);
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
            CreateFile(words, path);
            Console.WriteLine("Press any key to continue to Main Menu.....");
            Console.ReadKey();
            MainMenu(path);

        }

        /// <summary>
        /// prompts the user about what word they want to add, sends input to AddNewWord method
        /// </summary>
        public static void GetNewWord(string path)
        {
            Console.WriteLine("What word would you like to add?");
            string newWord = Console.ReadLine().ToLower();
            if (newWord.Contains(" "))
            {
                Console.WriteLine("Please enter a single word.");
                GetNewWord(path);
            }
            AddNewWord(newWord, path);
        }
        
        /// <summary>
        /// takes the users new word and appends it to the word list
        /// </summary>
        /// <param name="word">string type of the users new word</param>
        /// <returns>a string letting the user know the word has been added</returns>
        public static string AddNewWord(string word, string path)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(word);
            }
            return $"Your new word, {word}, has been added to the list.";
        }
        
        /// <summary>
        /// deletes the word list txt file
        /// </summary>
        public static void DeleteWordList(string path)
        {
            File.Delete(path);
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

        /// <summary>
        /// finds the number of words in the words list
        /// </summary>
        /// <returns>returns the int type of the number of words</returns>
        public static int FindLengthOfList(string path)
        {
            string wordList = CreateStringOfWords(path);
            char[] seperators = { ' ', ',', ';', '.', '\t', ':' };
            string[] wordArray = wordList.Split(seperators);
            int length = wordArray.Length;
            return length;
        }

        /// <summary>
        /// uses the return values of the FindLengthOfList and RandomNumber
        /// methods to pick a word from the word list
        /// </summary>
        /// <returns>string type of the random word for the game</returns>
        public static string PickTheWord(string path)
        {
            int length = FindLengthOfList(path);
            int index = RandomNumber(length);
            string wordList = CreateStringOfWords(path);
            char[] seperators = { ' ', ',', ';', '.', '\t', ':' };
            string[] wordArray = wordList.Split(seperators);
            string gameWord = wordArray[index];
            return gameWord;
        }

        /// <summary>
        /// starts the guessing game process
        /// </summary>
        public static void StartGame(string path)
        {
            string mysteryWord = PickTheWord(path);
            string[] letters = mysteryWord.Split(' ');
            for (int i = 0; i < letters.Length; i++)
            {
                letters[i] = "_";
            }
            GuessLetter(mysteryWord, path, letters);
        }

        /// <summary>
        /// asks the user if they want to guess one or two letters
        /// of the mystery word, sends the letter(s) to the appropriate
        /// methods to handle one or two letter guesses
        /// </summary>
        /// <param name="word">string type of the current mystery word</param>
        public static void GuessLetter(string word, string path, string[] status)
        {
            string mysteryWord = word;
            Console.WriteLine("Would you like to guess 1 or 2 letters?");
            string amount = Console.ReadLine();
            if (amount == "1")
            {
                Console.WriteLine("OK, guessing 1 letter");
                Console.WriteLine("What letter are you guessing?");
                char oneLetter = Convert.ToChar(Console.ReadLine().ToLower());
                AddGuessedLetters(oneLetter);
                OneLetterGuess(oneLetter, mysteryWord, status, path);
            }
            else if (amount == "2")
            {
                Console.WriteLine("OK, guessing 2 letters");
                Console.WriteLine("Enter the first letter to guess");
                char firstLetter = Convert.ToChar(Console.ReadLine().ToLower());
                AddGuessedLetters(firstLetter);
                Console.WriteLine("What is the second letter?");
                char secondLetter = Convert.ToChar(Console.ReadLine().ToLower());
                AddGuessedLetters(secondLetter);
                TwoLetterGuess(firstLetter, secondLetter, mysteryWord, status, path);
            }
            else
            {
                Console.WriteLine("Please respond with 1 or 2.");
                GuessLetter(mysteryWord, path, status);
            }
        }

        /// <summary>
        /// creates a file that stores the users guessed letters
        /// </summary>
        public static void CreateGuessedLetterList()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("guessedList.txt"))
                {
                    sw.Write("Guessed Letters");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// adds the letter(s) guessed by the user one at a time to the
        /// txt file created to store user guessed letters
        /// </summary>
        /// <param name="letter">char type of the users guessed letter</param>
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

        /// <summary>
        /// handles when a user guesses only one letter, lets the
        /// user know if the letter is in the mystery word or not
        /// </summary>
        /// <param name="letter">char type of guessed letter</param>
        /// <param name="word">string type of the current mystery word</param>
        public static void OneLetterGuess(char letter, string word, string[] status, string path)
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
            WordStatus(word, letter, status);
            GuessLetter(word, path, status);
            
        }

        /// <summary>
        /// handles when a user guessed two letters, lets the user know if any
        /// of the letters guessed are in the mystery word or not
        /// </summary>
        /// <param name="letterOne">char type of first letter guessed</param>
        /// <param name="letterTwo">char type of second letter guessed</param>
        /// <param name="word">string type of the current mystery word</param>
        public static void TwoLetterGuess(char letterOne, char letterTwo, string word, string[] status, string path)
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
            WordStatus(word, letterOne, status);
            WordStatus(word, letterTwo, status);
            GuessLetter(word, path, status);
        }

        /// <summary>
        /// eventually will show the user which letters and where in the word
        /// they are at.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="letter"></param>
        /// <param name="correct"></param>
        public static string[] WordStatus(string word, char letterGuess, string[] status)
        {
            string[] letters = word.Split(' ');
            string letter = Convert.ToString(letterGuess);
            Console.WriteLine("Current Status");
            Console.WriteLine();
            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] == letter)
                {
                    status[i] = letter;
                }
                Console.Write(status[i]);               
                
            }
            Console.WriteLine();
            return status;
        }
        
    }
}
