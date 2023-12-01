using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2015
{
    public class Day11 : General.abstractPuzzleClass
    {
        public Day11():base(11, 2015)
        {

        }
        static char[] forbidden = new char[] { 'i', 'o', 'l' };
        
        List<Func<string, bool>> requirements = new() { FirstRequirement(),SecondRequirement(),ThirdRequirement() };
        public override string SolvePart1(string input = null)
        {
            char[] characters = input.ToCharArray();
            while (!IsVallid(new string(characters)) || input== new string(characters))
            {
                characters[characters.Length-1]++;
                for (int i = characters.Length - 1; i>0; i--)
                {
                    if (characters[i]>'z')
                    {
                        characters[i] = 'a';
                        characters[i - 1]++;
                        if (forbidden.Contains(characters[i - 1]))
                        {
                            characters[i - 1]++;
                        }
                    }
                }
            }
            return new string(characters);
        }

        public override string SolvePart2(string input = null)
        {
            for (int i = 0; i < 2; i++)
            {
                input = SolvePart1(input);
            }
            return input;
        }

        private bool IsVallid(string password)
        {
            return requirements.All(x => x(password));
        }

        private static Func<string,bool> ThirdRequirement()
        {
            return input =>
            {
                int Count = 0;
                for (int i = 1; i < input.Length; i++)
                {
                    if (input[i] == input[i - 1])
                    {
                        Count++;
                        i++;
                    }
                }
                return Count==2;
            };
        }

        private static Func<string, bool> SecondRequirement()
        {
            return input =>
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (forbidden.Contains(input[i])) return false;
                }
                return true;
            };
        }

        private static Func<string, bool> FirstRequirement()
        {
            return input =>
            {
                for (int i = 2; i < input.Length; i++)
                {
                    if (input[i]-1==input[i-1]&& input[i] - 2 == input[i - 2]) return true;
                }
                return false;
            };
        }

        public override void Tests()
        {
            System.Diagnostics.Debug.Assert(IsVallid("hijklmmn") == false);
            System.Diagnostics.Debug.Assert(IsVallid("abbceffg") == false);
            System.Diagnostics.Debug.Assert(IsVallid("abbcegjk") == false);
            System.Diagnostics.Debug.Assert(SolvePart1("abcdefgh") == "abcdffaa");
            System.Diagnostics.Debug.Assert(SolvePart1("ghijklmn") == "ghjaabcc");
        }
    }
}
