using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteForceExample
{
    public class SafetyMeasure
    {
        public int Count { get; private set; }

        private string Password;

        public SafetyMeasure(string password) 
        { 
            Count = 0;
            Password = password;
        }
        public bool ConfirmCaptcha(string captchaAnswer)
        {
            String[] alphabet = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "å", "ä", "ö" };

            Random rn = new Random();

            while (true)
            {
                string captcha = alphabet[rn.Next(1, alphabet.Length)] + alphabet[rn.Next(1, alphabet.Length)] + alphabet[rn.Next(1, alphabet.Length)] + alphabet[rn.Next(1, alphabet.Length)];
                Console.WriteLine("Simulated captcha");
                Console.WriteLine("Write this word backwards: " + captcha);

                char[] reply = Console.ReadLine().ToCharArray();
                Array.Reverse(reply);
                string revReply = new string(reply);
                if (captcha == revReply)
                    break;
            }
            return true;
        }


        public bool TestLogin(string password, int delayTime)
        {
            if (Count > 3)
                Thread.Sleep(delayTime);    //Adjustable delaytime for testing purposes
            if (password == this.Password)
                return true;
            Count++;
            return false;
        }
    }
}
