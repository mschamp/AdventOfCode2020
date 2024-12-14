using System;
using System.Collections.Generic;
using System.Linq;

namespace _2019
{
	public class Day8 : General.IAoC
    {
        public int Day => 8;
		public int Year => 2019;

		public string SolvePart1(string input = null)
        {
            List<Layer> layers = []; 
            for (int i = 0; i < input.Length; i+=25*6)
            {
                layers.Add(new Layer(25,6,input.Substring(i, 25 * 6)));
            }

            Dictionary<int, int> ZeroCounts = [];
            for (int i = 0; i < layers.Count; i++)
            {
                ZeroCounts[i] = layers[i].GetDigitCount(0);
            }
            Layer layer = layers[ZeroCounts.OrderBy(x => x.Value).First().Key];
            return "" + (layer.GetDigitCount(1)*layer.GetDigitCount(2));
         }

        public string SolvePart2(string input = null)
        {
            List<Layer> layers = [];
            for (int i = 0; i < input.Length; i += 25 * 6)
            {
                layers.Add(new Layer(25, 6, input.Substring(i, 25 * 6)));
            }

            string result = "";
            for (int i = 0; i < layers[0].Height; i++)
            {
                for (int j = 0; j < layers[0].Width; j++)
                {
                    for (int l = 0; l < layers.Count(); l++)
                    {
                        if (layers[l].GetValue(j, i) == 1)
                        {
                            result += General.Constants.charConstants.White;
                            break;
                        }
                        else if (layers[l].GetValue(j, i) == 0)
                        {
                            result += " ";
                            break;
                        }
                    }
                }
                result += Environment.NewLine;
            }
            return result;
        }

        public void Tests()
        {
            
        }
    }

    public class Layer
    {
        public Layer(int width, int height, string data)
            :this(width, height)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Data[i % width, i / width]=int.Parse(data[i].ToString());
            }
        }

        public Layer(int width, int height)
        {
            Width = width;
            Height = height;
            Data = new int[width, height];
        }

        public int[,] Data { get; set; }
        public int GetDigitCount(int Digit)
        {
            int amount = 0;
            foreach (int digit in Data)
            {
                if (digit==Digit)
                {
                    amount++;
                }
            }
            return amount;
        }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int GetValue(int column, int row)
        {
            return Data[column, row];
        }
    }
}
