using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2015
{
    public class Day12 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            return FindNumbers(input).Sum().ToString();
        }

        private List<int> FindNumbers(string input)
        {
            List<int> result = new List<int>();
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"-?\d+");
            System.Text.RegularExpressions.MatchCollection matches = regex.Matches(input);

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                result.Add(int.Parse(match.Value));
            }
            return result;
        }

        private string RemoveInvalid(string input)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"{[^\[]*""red"".*}");
            input=regex.Replace(input, "0");
            return input;
        }

        public string SolvePart2(string input = null)
        {
            dynamic json = JsonConvert.DeserializeObject(input);

            return GetSum(json, "red").ToString();
        }

        private long GetSum(JObject jsonObject, string avoid)
        {
            bool avoidObject = jsonObject.Properties().Select(a => a.Value).OfType<JValue>()
                .Select(v => v.Value).Contains(avoid);
            if (avoidObject) return 0;
            return jsonObject.Properties().Sum((dynamic a) => (long)GetSum(a.Value, avoid));
        }

        long GetSum(JArray arr, string avoid) => arr.Sum((dynamic a) => (long)GetSum(a, avoid));

        long GetSum(JValue val, string avoid) => val.Type == JTokenType.Integer ? (long)val.Value : 0;

        public void Tests()
        {
            System.Diagnostics.Debug.Assert(SolvePart1("[1,2,3]") == "6");
            System.Diagnostics.Debug.Assert(SolvePart1("{\"a\":2,\"b\":4}") == "6");
            System.Diagnostics.Debug.Assert(SolvePart1("[[[3]]]") == "3");
            System.Diagnostics.Debug.Assert(SolvePart1("{\"a\":{\"b\":4},\"c\":-1}") == "3");
            System.Diagnostics.Debug.Assert(SolvePart2("[1,2,3]") == "6");
            System.Diagnostics.Debug.Assert(SolvePart2("[1,{\"c\":\"red\",\"b\":2},3]") == "4");
            System.Diagnostics.Debug.Assert(SolvePart2("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}") == "0");
            System.Diagnostics.Debug.Assert(SolvePart2("[1,\"red\",5]") == "6");
        }
    }
}
