namespace BruteForceExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Password (max 6 letters, recommended 4): ");
            String password = Console.ReadLine();

            String current = "";

            int[] pos = { 0, 0, 0, 0, 0, 0 };

            String[] alphabet = {"", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "å", "ä", "ö" };
            
            int count = 0;

            while (!current.Equals(password))
            {
                //Brute force protection
                if (count > 3)  
                {
                    Thread.Sleep(30000);
                }

                //Captcha
                while (true)
                {
                    Random rn = new Random();
                    string captcha = alphabet[rn.Next(1, alphabet.Length)] + alphabet[rn.Next(1, alphabet.Length)] + alphabet[rn.Next(1, alphabet.Length)] + alphabet[rn.Next(1, alphabet.Length)];
                    Console.WriteLine("Simulated captcha");
                    Console.WriteLine("Write this word backwards: " + captcha);

                    char[] reply = Console.ReadLine().ToCharArray();
                    Array.Reverse(reply);
                    string revReply = new string(reply);
                    if (captcha == revReply)
                        break;
                    count++;
                }
                
                for (int i = 0; i < pos.Length; i++)
                {
                    if (pos[i] == alphabet.Length)
                    {
                        pos[i] = 0;
                        pos[i + 1]++;
                    }
                }

                current = (alphabet[pos[5]] + alphabet[pos[4]] + alphabet[pos[3]] + alphabet[pos[2]] + alphabet[pos[1]] + alphabet[pos[0]]).ToString();

                Console.WriteLine("-" + current);
                pos[0]++;
                count++;

            }

            Console.WriteLine($"Hittat password: {current} med {count} iterationer");




        }
    }
}