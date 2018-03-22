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

        public static int RandomNumber(int length)
        {
            Random rand = new Random();
            int randNum = rand.Next(0, length);
            return randNum;
        }
    }
}
