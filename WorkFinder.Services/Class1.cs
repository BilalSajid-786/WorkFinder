using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Services
{
    public class Program
    {
        //madam/*
        /*
         * aba
         * aabaa
         */
        public static void Main(string[] args)
        {
            string input = "aabaa";
            bool result = Program.CheckPalindrome(input);
            Console.WriteLine($"{input} is {result}");   
        }

        public static bool CheckPalindrome(string input)
        {
            int sIndex = 0;
            for(int i = input.Length - 1; i>=0; i--)
            {
                if (input[i] != input[sIndex])
                {
                    return false;
                }
                sIndex++;
            }
            return true;
        }
    }
}
