using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ?ƎƆɸƟΦʘ<>∆∕+∙■□○●";
            string message = "themoldisoneofmanycauses".ToUpper();

            bool running = true;
            while (running)
            {
                Console.WriteLine(symbols);
                var list = GenerateKey(message, symbols);

                Console.WriteLine("IN: " + message);

                Console.Write("Symbols: ");
                for (int i = 0; i < list.Count; ++i)
                    Console.Write(list[i].Key);
                Console.WriteLine();
                
                Console.Write("Key: ");
                for (int i = 0; i < list.Count; ++i)
                    Console.Write(list[i].Value);
                Console.WriteLine();

                string e = Encrypt(message, list);
                Console.WriteLine("OUT: " + e);
                Console.WriteLine("BLOCK: ");

                int w = (int)Math.Sqrt(e.Length);
                int y = 0;
                for(int x = 0; x < e.Length; ++x)
                {
                    Console.Write(e[x]);
                    y += 1;
                    if(y == w)
                    {
                        Console.WriteLine();
                        y = 0;
                    }
                }

                Console.WriteLine();

                Console.ReadLine();
            }
        }

        static string Encrypt(string message, List<KeyValuePair<char, char>> key)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < message.Length; ++i)
            {
                for(int j = 0; j < key.Count; ++j)
                {
                    if(message[i] == key[j].Key)
                    {
                        sb.Append(key[j].Value);
                        break;
                    }
                }


            }

            return sb.ToString();
        }

        static List<KeyValuePair<char, char>> GenerateKey(string message, string symbols)
        {
            // Find which letters are used in the message
            List<char> letters_in_message = new List<char>();
            for(int i = 0; i < message.Length; ++i)
            {
                if( !letters_in_message.Contains(message[i]))
                {
                    letters_in_message.Add(message[i]);
                }
            }
            letters_in_message.Sort();

            // Create pool
            List<char> symbols_pool = new List<char>();
            for (int i = 0; i < symbols.Length; ++i)
                symbols_pool.Add(symbols[i]);


            // Generate key
            var keys = new List<KeyValuePair<char, char>>();
            Random rnd = new Random();
            for(int i = 0; i < letters_in_message.Count; ++i)
            {
                // Randomize an index
                int index = rnd.Next(0, symbols_pool.Count);

                // Take one from message and one from key pool
                keys.Add(new KeyValuePair<char, char>(
                    letters_in_message[i], symbols_pool[index]));

                symbols_pool.RemoveAt(index);
            }

            return keys;
        }
    }
}
