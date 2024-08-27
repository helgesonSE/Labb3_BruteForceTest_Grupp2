using BruteForceExample;
using System.Diagnostics;

namespace BruteForceProtectionTesting
{
    public class BruteForceProtectionTesting
    {
        public SafetyMeasure safetyMeasure;

        int[] Pos;

        String[] Alphabet;

        public BruteForceProtectionTesting() 
        {
            safetyMeasure = new SafetyMeasure("sam");
            Pos = new int[] { 0, 0, 0, 0, 0, 0 };
            Alphabet = new string[] { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "å", "ä", "ö" };
        }

        [Theory]
        [InlineData("Bert", false)]
        [InlineData("sam", true)]
        public void PasswordUserTest(string password, bool expected)
        {
            Assert.Equal(expected, safetyMeasure.TestLogin(password, 30000));
        }

        [Theory]
        [InlineData(10, 3000, true)]   // Expect timeout due to delay
        [InlineData(6, 0, false)]      // Expect no timeout, password should be found quickly
        public void PasswordBruteForcingTest(int timeLimit, int delayTime, bool expected)
        {
            bool timeOut = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                // Check for timeout
                if (stopwatch.Elapsed.TotalSeconds > timeLimit)
                {
                    timeOut = true;
                    break;
                }

                // Construct the current guess from the Pos array
                string currentGuess = string.Concat(Enumerable.Range(0, Pos.Length).Select(i => Alphabet[Pos[i]]));

                // Check if the guessed password is correct
                if (safetyMeasure.TestLogin(currentGuess, delayTime))
                {
                    break;  // Correct password found
                }

                // Increment the positions for the next guess
                Pos[0]++;
                for (int i = 0; i < Pos.Length - 1; i++)
                {
                    if (Pos[i] >= Alphabet.Length)
                    {
                        Pos[i] = 0;
                        Pos[i + 1]++;
                    }
                }

                // If all positions overflow, break the loop (end of search space)
                if (Pos[Pos.Length - 1] >= Alphabet.Length)
                {
                    break;
                }
            }

            stopwatch.Stop();

            // Assert that the test meets the expected timeout behavior
            Assert.Equal(expected, timeOut);
        }
    }
}