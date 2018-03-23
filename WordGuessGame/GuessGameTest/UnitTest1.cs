using System;
using Xunit;
using WordGuessGame;
using static WordGuessGame.Program;

namespace GuessGameTest
{
    public class UnitTest1
    {
        /*
        [Fact]
        public void CanMakeStringOfWords()
        {
            Assert.Equal("dragon, cat, snake, airplane, monkey, video, computer, magician, dog, house, umbrella, phone, guitar, horse", Creat
                ());
        }
        */
        [Theory]
        [InlineData("Your new word, alpha, has been added to the list.", "alpha")]
        [InlineData("Your new word, beta, has been added to the list.", "beta")]
        [InlineData("Your new word, charlie, has been added to the list.", "charlie")]
        [InlineData("Your new word, delta, has been added to the list.", "delta")]
        public void CanAddWord(string testResult, string testInput)
        {
            Assert.Equal(testResult, AddNewWord(testInput));
        }

        [Theory]
        [InlineData(9)]
    }
}
