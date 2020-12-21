using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _2020
{
    public class Day21 : General.IAoC
    {
        public string SolvePart1(string input = null)
        {
            List<Food> foods = new List<Food>();
            foreach (string item in input.Split(Environment.NewLine))
            {
                foods.Add(new Food(item));
            }

            Dictionary<string, string> AllergenIngredient = MatchAllergensIngredients(foods);

            int Counter = 0;
            foreach (Food food in foods)
            {
                foreach (string ingred in food.Ingredients)
                {
                    if (!AllergenIngredient.Values.Contains(ingred))
                    {
                        Counter++;
                    }
                }
            }

            return "" + Counter;
        }

        public string SolvePart2(string input = null)
        {
            List<Food> foods = new List<Food>();
            foreach (string item in input.Split(Environment.NewLine))
            {
                foods.Add(new Food(item));
            }

            Dictionary<string, string> AllergenIngredient = MatchAllergensIngredients(foods);


            List<string> Ordered = new List<string>();
            foreach (var item in AllergenIngredient.Keys.OrderBy(x =>x))
            {
                Ordered.Add(AllergenIngredient[item]);
            }

            return string.Join(",", Ordered);
        }

        private Dictionary<string, string> MatchAllergensIngredients(List<Food> foods)
        {
            Dictionary<string, string> AllergenIngredient = new Dictionary<string, string>();
            HashSet<string> ingredients = new HashSet<string>();
            foreach (Food food in foods)
            {
                foreach (string allergen in food.Allergens)
                {
                    AllergenIngredient[allergen] = null;
                }
                foreach (string ingred in food.Ingredients)
                {
                    ingredients.Add(ingred);
                }

            }

            List<String> Allergens = AllergenIngredient.Keys.ToList();
            while (AllergenIngredient.Values.Contains(null))
            {
                foreach (string Allerg in Allergens)
                {
                    if (AllergenIngredient[Allerg] == null)
                    {
                        List<string> options = foods.Where(x => x.Allergens.Contains(Allerg)).First().Ingredients;
                        foreach (var Assigned in AllergenIngredient.Values)
                        {
                            options.Remove(Assigned);
                        }

                        foreach (var recept in foods.Where(x => x.Allergens.Contains(Allerg)))
                        {
                            options = options.Intersect(recept.Ingredients).ToList();
                        }
                        if (options.Count == 1)
                        {
                            AllergenIngredient[Allerg] = options[0];
                            ingredients.Remove(options[0]);
                        }
                    }

                }
            }
            return AllergenIngredient;
        }

        public void Tests()
        {
            Debug.Assert(SolvePart1(@"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)") == "5");

            Debug.Assert(SolvePart2(@"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)") == "mxmxvkd,sqjhc,fvjkl");
        }
    }

    public class Food
    {
        public Food(string Input)
        {
            Regex rgx = new Regex(@"((?:\w+\s)+)\(contains\s((?:\w+(?:,\s)?)+)\)");
            Match mtch = rgx.Match(Input);
            Ingredients = mtch.Groups[1].Value.Split(" ",StringSplitOptions.RemoveEmptyEntries).ToList();
            Allergens = mtch.Groups[2].Value.Split(", ").ToList();
        }

        public List<String> Ingredients { get; set; }
        public List<String> Allergens { get; set; }
    }
}
