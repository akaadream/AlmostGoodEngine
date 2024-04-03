namespace AlmostGoodEngine.Core.Utils
{
	public static class Helper
	{
		#region Strings
		public static char[] Majs = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
		public static char[] Mins = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];

		/// <summary>
		/// Separate - _ of the given text and reconstruct a string with first letter upper
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string Headline(this string text)
		{
			string final = "";
			for (int i = 0; i < text.Length; i++)
			{
				var letter = text[i];

				if (letter == '-' ||
					letter == '_')
				{
					if (final.Length > 0 && final[final.Length - 1] == ' ')
					{
						continue;
					}
					final += ' ';
				}
				else if (IsMaj(letter))
				{
					if (final.Length > 0 && final[final.Length - 1] != ' ')
					{
						final += " ";
					}
					final += letter.ToString().ToUpper();
				}
				else
				{
					if (i == 0 || (final.Length > 0 && final[final.Length - 1] == ' '))
					{
						final += letter.ToString().ToUpper();
					}
					else
					{
						final += letter;
					}
				}
			}
			return final;
		}
		
		/// <summary>
		/// Upper the first letter of the given text
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string UpperFirst(this string text)
		{
			if (text.Length == 0)
			{
				return text;
			}

			return text[0].ToString().ToUpper() + text.Substring(1);
		}

		/// <summary>
		/// Lower the first letter of the given text
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string LowerFirst(this string text)
		{
			if (text.Length == 0)
			{
				return text;
			}

			return text[0].ToString().ToLower() + text.Substring(1);
		}

		/// <summary>
		/// Mask the characters of the given text using the given mask character
		/// </summary>
		/// <param name="text"></param>
		/// <param name="character"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public static string Mask(this string text, char character, int start, int end)
		{
			if (end < start)
			{
				end = start;
			}

			string final = "";
			for (int i = 0; i < text.Length; i++)
			{
				if (i >= start && i <= end)
				{
					final += character.ToString();
				}
				else
				{
					final += text[i];
				}
			}
			return final;
		}

		/// <summary>
		/// Mask the characters of the given text using the given mask character
		/// </summary>
		/// <param name="text"></param>
		/// <param name="character"></param>
		/// <param name="start"></param>
		/// <returns></returns>
		public static string Mask(this string text, char character, int start)
		{
			return Mask(text, character, start, text.Length - 1);
		}

		private static bool IsMaj(char letter)
		{
			return letter >= 'A' && letter <= 'Z';
		}

		private static bool IsMin(char letter)
		{
			return letter >= 'a' && letter <= 'z';
		}
		#endregion

		public static void Test()
		{
			//Logger.Log(test, "Helper", ConsoleColor.DarkBlue);
		}
	}
}
