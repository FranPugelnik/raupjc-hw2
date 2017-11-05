using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }

        public static async Task<int> FactorialDigitSum(int number)
        {
            return await Task.Run(() =>
            {
                int factorial = 1;
                while (number != 1)
                {
                    factorial *= number;
                    number--;
                }
                int result = 0;
                while (factorial > 0)
                {
                    result += factorial % 10;
                    factorial = factorial / 10;
                }
                return result;
            });


        }

        private static async Task LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result =await GetTheMagicNumber();
            Console.WriteLine(result);
        }
        private static async Task<int> GetTheMagicNumber()
        {
            return await IKnowIGuyWhoKnowsAGuy();
        }
        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            return await IKnowWhoKnowsThis(10) + await IKnowWhoKnowsThis(5);
        }
        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            return await FactorialDigitSum(n);
        }

    }
}
