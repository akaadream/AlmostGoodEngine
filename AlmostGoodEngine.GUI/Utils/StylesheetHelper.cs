using ExCSS;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MColor = Microsoft.Xna.Framework.Color;

namespace AlmostGoodEngine.GUI.Utils
{
	internal static class StylesheetHelper
	{
		public static MColor ToMonoGameColor(this Color color)
		{
			return new MColor(color.R, color.G, color.B, color.A);
		}

		public static MColor FromCssString(string cssColor)
		{
			if (string.IsNullOrWhiteSpace(cssColor))
			{
				return MColor.Transparent;
			}

			cssColor = cssColor.Trim();

			if (cssColor.StartsWith("#"))
			{
				return FromHex(cssColor);
			}
			else if (cssColor.StartsWith("rgb(") && cssColor.EndsWith(")"))
			{
				return FromRGBA(cssColor);
			}

			return MColor.Transparent;
		}

		private static MColor FromHex(string hexColor)
		{
			hexColor = hexColor.TrimStart('#');

			if (hexColor.Length != 6 && hexColor.Length != 8)
			{
				return MColor.Transparent;
			}

			int red = Convert.ToInt32(hexColor.Substring(0, 2), 16);
			int green = Convert.ToInt32(hexColor.Substring(2, 2), 16);
			int blue = Convert.ToInt32(hexColor.Substring(4, 2), 16);
			
			if (hexColor.Length == 8)
			{
				return new MColor(red, green, blue, Convert.ToInt32(hexColor.Substring(6, 2), 16));
			}

			return new MColor(red, green, blue);
		}

		private static MColor FromRGBA(string rgbaColor)
		{
			rgbaColor = rgbaColor.Trim().TrimStart("rgb(".ToCharArray()).TrimEnd(')');
			string[] colorValues = rgbaColor.Split(',');

			if (colorValues.Length != 3 && colorValues.Length != 4)
			{
				return MColor.Transparent;
			}

			int red = int.Parse(colorValues[0]);
			int green = int.Parse(colorValues[1]);
			int blue = int.Parse(colorValues[2]);
			if (colorValues.Length == 4)
			{
				return new MColor(red, green, blue, int.Parse(colorValues[3]));
			}

			return new MColor(red, green, blue);
		}

		public static int FromCssCalcToSize(string cssValue)
		{
			string pattern = @"calc\((.*)\)";
			Match match = Regex.Match(cssValue, pattern);
			if (match.Success)
			{
				string innerExpression = match.Groups[1].Value;
				return ComputeCalc(innerExpression);
			}

			// Not able to compute the css calc
			return 0;
		}

		private static int ComputeCalc(string expression)
		{
			string pattern = @"\d+\.?\d*|\+|\-|\*|\/";
			MatchCollection matches = Regex.Matches(expression, pattern);

			List<int> numbers = [];
			List<string> operators = [];

			foreach (Match match in matches)
			{
				string matchValue = match.Value;
				if (int.TryParse(matchValue, out int number))
				{
					numbers.Add(number);
				}
				else
				{
					operators.Add(matchValue);
				}
			}

			// Priorities (* and /)
			for (int i = 0; i < operators.Count; i++)
			{
				if (operators[i] == "*" || operators[i] == "/")
				{
					int left = numbers[i];
					int right = numbers[i + 1];
					int result = operators[i] switch
					{
						"*" => left * right,
						"/" => left / right,
						_ => 0
					};

					numbers[i] = result;
					numbers.RemoveAt(i + 1);
					operators.RemoveAt(i);
					i--;
				}
			}

			//int total = numbers[0]
			return 0;
		}

		public static int FromCssToSize(string cssValue)
		{
			if (cssValue.EndsWith("px") ||
				cssValue.EndsWith("vw") ||
				cssValue.EndsWith("vh") ||
				cssValue.EndsWith("em"))
			{
				return int.Parse(cssValue.Substring(0, cssValue.Length - 2));
			}

			if (cssValue.EndsWith("%"))
			{
				return int.Parse(cssValue.Substring(0, cssValue.Length - 1));
			}

			return 0;
		}

		public static float FromCssToSeconds(string cssValue)
		{
			if (cssValue.EndsWith("ms"))
			{
				return float.Parse(cssValue.Substring(0, cssValue.Length - 2)) / 1000f;
			}

			if (cssValue.EndsWith("s"))
			{
				return (float)double.Parse(cssValue.Substring(0, cssValue.Length - 1));
			}

			return 0f;
		}
	}
}
