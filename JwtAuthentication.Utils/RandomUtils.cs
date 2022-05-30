using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthentication.Utils
{
    public static class RandomUtils
    {
        private static Random Randomize()
        {
            return new Random();
        }

        public static int RandomNumber(int upperLimit)
        {
            try
            {
                Random randomNumber = Randomize();

                return randomNumber.Next(1, upperLimit);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            try
            {
                Random randomNumber = Randomize();

                return new string(Enumerable.Repeat(chars, length).Select(s => s[randomNumber.Next(s.Length)]).ToArray());
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
