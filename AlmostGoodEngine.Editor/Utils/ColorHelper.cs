using System.Numerics;

using Cl = Microsoft.Xna.Framework.Color;

namespace AlmostGoodEngine.Editor.Utils
{
	public static class ColorsHelper
	{
		/// <summary>
		/// Transparent Cl (R:0,G:0,B:0,A:0).
		/// </summary>
		public static Vector4 Transparent
		{
			get;
			private set;
		}

		/// <summary>
		/// AliceBlue Cl (R:240,G:248,B:255,A:255).
		/// </summary>
		public static Vector4 AliceBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// AntiqueWhite Cl (R:250,G:235,B:215,A:255).
		/// </summary>
		public static Vector4 AntiqueWhite
		{
			get;
			private set;
		}

		/// <summary>
		/// Aqua Cl (R:0,G:255,B:255,A:255).
		/// </summary>
		public static Vector4 Aqua
		{
			get;
			private set;
		}

		/// <summary>
		/// Aquamarine Cl (R:127,G:255,B:212,A:255).
		/// </summary>
		public static Vector4 Aquamarine
		{
			get;
			private set;
		}

		/// <summary>
		/// Azure Cl (R:240,G:255,B:255,A:255).
		/// </summary>
		public static Vector4 Azure
		{
			get;
			private set;
		}

		/// <summary>
		/// Beige Cl (R:245,G:245,B:220,A:255).
		/// </summary>
		public static Vector4 Beige
		{
			get;
			private set;
		}

		/// <summary>
		/// Bisque Cl (R:255,G:228,B:196,A:255).
		/// </summary>
		public static Vector4 Bisque
		{
			get;
			private set;
		}

		/// <summary>
		/// Black Cl (R:0,G:0,B:0,A:255).
		/// </summary>
		public static Vector4 Black
		{
			get;
			private set;
		}

		/// <summary>
		/// BlanchedAlmond Cl (R:255,G:235,B:205,A:255).
		/// </summary>
		public static Vector4 BlanchedAlmond
		{
			get;
			private set;
		}

		/// <summary>
		/// Blue Cl (R:0,G:0,B:255,A:255).
		/// </summary>
		public static Vector4 Blue
		{
			get;
			private set;
		}

		/// <summary>
		/// BlueViolet Cl (R:138,G:43,B:226,A:255).
		/// </summary>
		public static Vector4 BlueViolet
		{
			get;
			private set;
		}

		/// <summary>
		/// Brown Cl (R:165,G:42,B:42,A:255).
		/// </summary>
		public static Vector4 Brown
		{
			get;
			private set;
		}

		/// <summary>
		/// BurlyWood Cl (R:222,G:184,B:135,A:255).
		/// </summary>
		public static Vector4 BurlyWood
		{
			get;
			private set;
		}

		/// <summary>
		/// CadetBlue Cl (R:95,G:158,B:160,A:255).
		/// </summary>
		public static Vector4 CadetBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// Chartreuse Cl (R:127,G:255,B:0,A:255).
		/// </summary>
		public static Vector4 Chartreuse
		{
			get;
			private set;
		}

		/// <summary>
		/// Chocolate Cl (R:210,G:105,B:30,A:255).
		/// </summary>
		public static Vector4 Chocolate
		{
			get;
			private set;
		}

		/// <summary>
		/// Coral Cl (R:255,G:127,B:80,A:255).
		/// </summary>
		public static Vector4 Coral
		{
			get;
			private set;
		}

		/// <summary>
		/// CornflowerBlue Cl (R:100,G:149,B:237,A:255).
		/// </summary>
		public static Vector4 CornflowerBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// Cornsilk Cl (R:255,G:248,B:220,A:255).
		/// </summary>
		public static Vector4 Cornsilk
		{
			get;
			private set;
		}

		/// <summary>
		/// Crimson Cl (R:220,G:20,B:60,A:255).
		/// </summary>
		public static Vector4 Crimson
		{
			get;
			private set;
		}

		/// <summary>
		/// Cyan Cl (R:0,G:255,B:255,A:255).
		/// </summary>
		public static Vector4 Cyan
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkBlue Cl (R:0,G:0,B:139,A:255).
		/// </summary>
		public static Vector4 DarkBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkCyan Cl (R:0,G:139,B:139,A:255).
		/// </summary>
		public static Vector4 DarkCyan
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkGoldenrod Cl (R:184,G:134,B:11,A:255).
		/// </summary>
		public static Vector4 DarkGoldenrod
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkGray Cl (R:169,G:169,B:169,A:255).
		/// </summary>
		public static Vector4 DarkGray
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkGreen Cl (R:0,G:100,B:0,A:255).
		/// </summary>
		public static Vector4 DarkGreen
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkKhaki Cl (R:189,G:183,B:107,A:255).
		/// </summary>
		public static Vector4 DarkKhaki
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkMagenta Cl (R:139,G:0,B:139,A:255).
		/// </summary>
		public static Vector4 DarkMagenta
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkOliveGreen Cl (R:85,G:107,B:47,A:255).
		/// </summary>
		public static Vector4 DarkOliveGreen
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkOrange Cl (R:255,G:140,B:0,A:255).
		/// </summary>
		public static Vector4 DarkOrange
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkOrchid Cl (R:153,G:50,B:204,A:255).
		/// </summary>
		public static Vector4 DarkOrchid
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkRed Cl (R:139,G:0,B:0,A:255).
		/// </summary>
		public static Vector4 DarkRed
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkSalmon Cl (R:233,G:150,B:122,A:255).
		/// </summary>
		public static Vector4 DarkSalmon
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkSeaGreen Cl (R:143,G:188,B:139,A:255).
		/// </summary>
		public static Vector4 DarkSeaGreen
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkSlateBlue Cl (R:72,G:61,B:139,A:255).
		/// </summary>
		public static Vector4 DarkSlateBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkSlateGray Cl (R:47,G:79,B:79,A:255).
		/// </summary>
		public static Vector4 DarkSlateGray
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkTurquoise Cl (R:0,G:206,B:209,A:255).
		/// </summary>
		public static Vector4 DarkTurquoise
		{
			get;
			private set;
		}

		/// <summary>
		/// DarkViolet Cl (R:148,G:0,B:211,A:255).
		/// </summary>
		public static Vector4 DarkViolet
		{
			get;
			private set;
		}

		/// <summary>
		/// DeepPink Cl (R:255,G:20,B:147,A:255).
		/// </summary>
		public static Vector4 DeepPink
		{
			get;
			private set;
		}

		/// <summary>
		/// DeepSkyBlue Cl (R:0,G:191,B:255,A:255).
		/// </summary>
		public static Vector4 DeepSkyBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// DimGray Cl (R:105,G:105,B:105,A:255).
		/// </summary>
		public static Vector4 DimGray
		{
			get;
			private set;
		}

		/// <summary>
		/// DodgerBlue Cl (R:30,G:144,B:255,A:255).
		/// </summary>
		public static Vector4 DodgerBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// Firebrick Cl (R:178,G:34,B:34,A:255).
		/// </summary>
		public static Vector4 Firebrick
		{
			get;
			private set;
		}

		/// <summary>
		/// FloralWhite Cl (R:255,G:250,B:240,A:255).
		/// </summary>
		public static Vector4 FloralWhite
		{
			get;
			private set;
		}

		/// <summary>
		/// ForestGreen Cl (R:34,G:139,B:34,A:255).
		/// </summary>
		public static Vector4 ForestGreen
		{
			get;
			private set;
		}

		/// <summary>
		/// Fuchsia Cl (R:255,G:0,B:255,A:255).
		/// </summary>
		public static Vector4 Fuchsia
		{
			get;
			private set;
		}

		/// <summary>
		/// Gainsboro Cl (R:220,G:220,B:220,A:255).
		/// </summary>
		public static Vector4 Gainsboro
		{
			get;
			private set;
		}

		/// <summary>
		/// GhostWhite Cl (R:248,G:248,B:255,A:255).
		/// </summary>
		public static Vector4 GhostWhite
		{
			get;
			private set;
		}
		/// <summary>
		/// Gold Cl (R:255,G:215,B:0,A:255).
		/// </summary>
		public static Vector4 Gold
		{
			get;
			private set;
		}

		/// <summary>
		/// Goldenrod Cl (R:218,G:165,B:32,A:255).
		/// </summary>
		public static Vector4 Goldenrod
		{
			get;
			private set;
		}

		/// <summary>
		/// Gray Cl (R:128,G:128,B:128,A:255).
		/// </summary>
		public static Vector4 Gray
		{
			get;
			private set;
		}

		/// <summary>
		/// Green Cl (R:0,G:128,B:0,A:255).
		/// </summary>
		public static Vector4 Green
		{
			get;
			private set;
		}

		/// <summary>
		/// GreenYellow Cl (R:173,G:255,B:47,A:255).
		/// </summary>
		public static Vector4 GreenYellow
		{
			get;
			private set;
		}

		/// <summary>
		/// Honeydew Cl (R:240,G:255,B:240,A:255).
		/// </summary>
		public static Vector4 Honeydew
		{
			get;
			private set;
		}

		/// <summary>
		/// HotPink Cl (R:255,G:105,B:180,A:255).
		/// </summary>
		public static Vector4 HotPink
		{
			get;
			private set;
		}

		/// <summary>
		/// IndianRed Cl (R:205,G:92,B:92,A:255).
		/// </summary>
		public static Vector4 IndianRed
		{
			get;
			private set;
		}

		/// <summary>
		/// Indigo Cl (R:75,G:0,B:130,A:255).
		/// </summary>
		public static Vector4 Indigo
		{
			get;
			private set;
		}

		/// <summary>
		/// Ivory Cl (R:255,G:255,B:240,A:255).
		/// </summary>
		public static Vector4 Ivory
		{
			get;
			private set;
		}

		/// <summary>
		/// Khaki Cl (R:240,G:230,B:140,A:255).
		/// </summary>
		public static Vector4 Khaki
		{
			get;
			private set;
		}

		/// <summary>
		/// Lavender Cl (R:230,G:230,B:250,A:255).
		/// </summary>
		public static Vector4 Lavender
		{
			get;
			private set;
		}

		/// <summary>
		/// LavenderBlush Cl (R:255,G:240,B:245,A:255).
		/// </summary>
		public static Vector4 LavenderBlush
		{
			get;
			private set;
		}

		/// <summary>
		/// LawnGreen Cl (R:124,G:252,B:0,A:255).
		/// </summary>
		public static Vector4 LawnGreen
		{
			get;
			private set;
		}

		/// <summary>
		/// LemonChiffon Cl (R:255,G:250,B:205,A:255).
		/// </summary>
		public static Vector4 LemonChiffon
		{
			get;
			private set;
		}

		/// <summary>
		/// LightBlue Cl (R:173,G:216,B:230,A:255).
		/// </summary>
		public static Vector4 LightBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// LightCoral Cl (R:240,G:128,B:128,A:255).
		/// </summary>
		public static Vector4 LightCoral
		{
			get;
			private set;
		}

		/// <summary>
		/// LightCyan Cl (R:224,G:255,B:255,A:255).
		/// </summary>
		public static Vector4 LightCyan
		{
			get;
			private set;
		}

		/// <summary>
		/// LightGoldenrodYellow Cl (R:250,G:250,B:210,A:255).
		/// </summary>
		public static Vector4 LightGoldenrodYellow
		{
			get;
			private set;
		}

		/// <summary>
		/// LightGray Cl (R:211,G:211,B:211,A:255).
		/// </summary>
		public static Vector4 LightGray
		{
			get;
			private set;
		}

		/// <summary>
		/// LightGreen Cl (R:144,G:238,B:144,A:255).
		/// </summary>
		public static Vector4 LightGreen
		{
			get;
			private set;
		}

		/// <summary>
		/// LightPink Cl (R:255,G:182,B:193,A:255).
		/// </summary>
		public static Vector4 LightPink
		{
			get;
			private set;
		}

		/// <summary>
		/// LightSalmon Cl (R:255,G:160,B:122,A:255).
		/// </summary>
		public static Vector4 LightSalmon
		{
			get;
			private set;
		}

		/// <summary>
		/// LightSeaGreen Cl (R:32,G:178,B:170,A:255).
		/// </summary>
		public static Vector4 LightSeaGreen
		{
			get;
			private set;
		}

		/// <summary>
		/// LightSkyBlue Cl (R:135,G:206,B:250,A:255).
		/// </summary>
		public static Vector4 LightSkyBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// LightSlateGray Cl (R:119,G:136,B:153,A:255).
		/// </summary>
		public static Vector4 LightSlateGray
		{
			get;
			private set;
		}

		/// <summary>
		/// LightSteelBlue Cl (R:176,G:196,B:222,A:255).
		/// </summary>
		public static Vector4 LightSteelBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// LightYellow Cl (R:255,G:255,B:224,A:255).
		/// </summary>
		public static Vector4 LightYellow
		{
			get;
			private set;
		}

		/// <summary>
		/// Lime Cl (R:0,G:255,B:0,A:255).
		/// </summary>
		public static Vector4 Lime
		{
			get;
			private set;
		}

		/// <summary>
		/// LimeGreen Cl (R:50,G:205,B:50,A:255).
		/// </summary>
		public static Vector4 LimeGreen
		{
			get;
			private set;
		}

		/// <summary>
		/// Linen Cl (R:250,G:240,B:230,A:255).
		/// </summary>
		public static Vector4 Linen
		{
			get;
			private set;
		}

		/// <summary>
		/// Magenta Cl (R:255,G:0,B:255,A:255).
		/// </summary>
		public static Vector4 Magenta
		{
			get;
			private set;
		}

		/// <summary>
		/// Maroon Cl (R:128,G:0,B:0,A:255).
		/// </summary>
		public static Vector4 Maroon
		{
			get;
			private set;
		}

		/// <summary>
		/// MediumAquamarine Cl (R:102,G:205,B:170,A:255).
		/// </summary>
		public static Vector4 MediumAquamarine
		{
			get;
			private set;
		}

		/// <summary>
		/// MediumBlue Cl (R:0,G:0,B:205,A:255).
		/// </summary>
		public static Vector4 MediumBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// MediumOrchid Cl (R:186,G:85,B:211,A:255).
		/// </summary>
		public static Vector4 MediumOrchid
		{
			get;
			private set;
		}

		/// <summary>
		/// MediumPurple Cl (R:147,G:112,B:219,A:255).
		/// </summary>
		public static Vector4 MediumPurple
		{
			get;
			private set;
		}

		/// <summary>
		/// MediumSeaGreen Cl (R:60,G:179,B:113,A:255).
		/// </summary>
		public static Vector4 MediumSeaGreen
		{
			get;
			private set;
		}

		/// <summary>
		/// MediumSlateBlue Cl (R:123,G:104,B:238,A:255).
		/// </summary>
		public static Vector4 MediumSlateBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// MediumSpringGreen Cl (R:0,G:250,B:154,A:255).
		/// </summary>
		public static Vector4 MediumSpringGreen
		{
			get;
			private set;
		}

		/// <summary>
		/// MediumTurquoise Cl (R:72,G:209,B:204,A:255).
		/// </summary>
		public static Vector4 MediumTurquoise
		{
			get;
			private set;
		}

		/// <summary>
		/// MediumVioletRed Cl (R:199,G:21,B:133,A:255).
		/// </summary>
		public static Vector4 MediumVioletRed
		{
			get;
			private set;
		}

		/// <summary>
		/// MidnightBlue Cl (R:25,G:25,B:112,A:255).
		/// </summary>
		public static Vector4 MidnightBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// MintCream Cl (R:245,G:255,B:250,A:255).
		/// </summary>
		public static Vector4 MintCream
		{
			get;
			private set;
		}

		/// <summary>
		/// MistyRose Cl (R:255,G:228,B:225,A:255).
		/// </summary>
		public static Vector4 MistyRose
		{
			get;
			private set;
		}

		/// <summary>
		/// Moccasin Cl (R:255,G:228,B:181,A:255).
		/// </summary>
		public static Vector4 Moccasin
		{
			get;
			private set;
		}

		/// <summary>
		/// MonoGame orange theme Cl (R:231,G:60,B:0,A:255).
		/// </summary>
		public static Vector4 MonoGameOrange
		{
			get;
			private set;
		}

		/// <summary>
		/// NavajoWhite Cl (R:255,G:222,B:173,A:255).
		/// </summary>
		public static Vector4 NavajoWhite
		{
			get;
			private set;
		}

		/// <summary>
		/// Navy Cl (R:0,G:0,B:128,A:255).
		/// </summary>
		public static Vector4 Navy
		{
			get;
			private set;
		}

		/// <summary>
		/// OldLace Cl (R:253,G:245,B:230,A:255).
		/// </summary>
		public static Vector4 OldLace
		{
			get;
			private set;
		}

		/// <summary>
		/// Olive Cl (R:128,G:128,B:0,A:255).
		/// </summary>
		public static Vector4 Olive
		{
			get;
			private set;
		}

		/// <summary>
		/// OliveDrab Cl (R:107,G:142,B:35,A:255).
		/// </summary>
		public static Vector4 OliveDrab
		{
			get;
			private set;
		}

		/// <summary>
		/// Orange Cl (R:255,G:165,B:0,A:255).
		/// </summary>
		public static Vector4 Orange
		{
			get;
			private set;
		}

		/// <summary>
		/// OrangeRed Cl (R:255,G:69,B:0,A:255).
		/// </summary>
		public static Vector4 OrangeRed
		{
			get;
			private set;
		}

		/// <summary>
		/// Orchid Cl (R:218,G:112,B:214,A:255).
		/// </summary>
		public static Vector4 Orchid
		{
			get;
			private set;
		}

		/// <summary>
		/// PaleGoldenrod Cl (R:238,G:232,B:170,A:255).
		/// </summary>
		public static Vector4 PaleGoldenrod
		{
			get;
			private set;
		}

		/// <summary>
		/// PaleGreen Cl (R:152,G:251,B:152,A:255).
		/// </summary>
		public static Vector4 PaleGreen
		{
			get;
			private set;
		}

		/// <summary>
		/// PaleTurquoise Cl (R:175,G:238,B:238,A:255).
		/// </summary>
		public static Vector4 PaleTurquoise
		{
			get;
			private set;
		}
		/// <summary>
		/// PaleVioletRed Cl (R:219,G:112,B:147,A:255).
		/// </summary>
		public static Vector4 PaleVioletRed
		{
			get;
			private set;
		}

		/// <summary>
		/// PapayaWhip Cl (R:255,G:239,B:213,A:255).
		/// </summary>
		public static Vector4 PapayaWhip
		{
			get;
			private set;
		}

		/// <summary>
		/// PeachPuff Cl (R:255,G:218,B:185,A:255).
		/// </summary>
		public static Vector4 PeachPuff
		{
			get;
			private set;
		}

		/// <summary>
		/// Peru Cl (R:205,G:133,B:63,A:255).
		/// </summary>
		public static Vector4 Peru
		{
			get;
			private set;
		}

		/// <summary>
		/// Pink Cl (R:255,G:192,B:203,A:255).
		/// </summary>
		public static Vector4 Pink
		{
			get;
			private set;
		}

		/// <summary>
		/// Plum Cl (R:221,G:160,B:221,A:255).
		/// </summary>
		public static Vector4 Plum
		{
			get;
			private set;
		}

		/// <summary>
		/// PowderBlue Cl (R:176,G:224,B:230,A:255).
		/// </summary>
		public static Vector4 PowderBlue
		{
			get;
			private set;
		}

		/// <summary>
		///  Purple Cl (R:128,G:0,B:128,A:255).
		/// </summary>
		public static Vector4 Purple
		{
			get;
			private set;
		}

		/// <summary>
		/// Red Cl (R:255,G:0,B:0,A:255).
		/// </summary>
		public static Vector4 Red
		{
			get;
			private set;
		}

		/// <summary>
		/// RosyBrown Cl (R:188,G:143,B:143,A:255).
		/// </summary>
		public static Vector4 RosyBrown
		{
			get;
			private set;
		}

		/// <summary>
		/// RoyalBlue Cl (R:65,G:105,B:225,A:255).
		/// </summary>
		public static Vector4 RoyalBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// SaddleBrown Cl (R:139,G:69,B:19,A:255).
		/// </summary>
		public static Vector4 SaddleBrown
		{
			get;
			private set;
		}

		/// <summary>
		/// Salmon Cl (R:250,G:128,B:114,A:255).
		/// </summary>
		public static Vector4 Salmon
		{
			get;
			private set;
		}

		/// <summary>
		/// SandyBrown Cl (R:244,G:164,B:96,A:255).
		/// </summary>
		public static Vector4 SandyBrown
		{
			get;
			private set;
		}

		/// <summary>
		/// SeaGreen Cl (R:46,G:139,B:87,A:255).
		/// </summary>
		public static Vector4 SeaGreen
		{
			get;
			private set;
		}

		/// <summary>
		/// SeaShell Cl (R:255,G:245,B:238,A:255).
		/// </summary>
		public static Vector4 SeaShell
		{
			get;
			private set;
		}

		/// <summary>
		/// Sienna Cl (R:160,G:82,B:45,A:255).
		/// </summary>
		public static Vector4 Sienna
		{
			get;
			private set;
		}

		/// <summary>
		/// Silver Cl (R:192,G:192,B:192,A:255).
		/// </summary>
		public static Vector4 Silver
		{
			get;
			private set;
		}

		/// <summary>
		/// SkyBlue Cl (R:135,G:206,B:235,A:255).
		/// </summary>
		public static Vector4 SkyBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// SlateBlue Cl (R:106,G:90,B:205,A:255).
		/// </summary>
		public static Vector4 SlateBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// SlateGray Cl (R:112,G:128,B:144,A:255).
		/// </summary>
		public static Vector4 SlateGray
		{
			get;
			private set;
		}

		/// <summary>
		/// Snow Cl (R:255,G:250,B:250,A:255).
		/// </summary>
		public static Vector4 Snow
		{
			get;
			private set;
		}

		/// <summary>
		/// SpringGreen Cl (R:0,G:255,B:127,A:255).
		/// </summary>
		public static Vector4 SpringGreen
		{
			get;
			private set;
		}

		/// <summary>
		/// SteelBlue Cl (R:70,G:130,B:180,A:255).
		/// </summary>
		public static Vector4 SteelBlue
		{
			get;
			private set;
		}

		/// <summary>
		/// Tan Cl (R:210,G:180,B:140,A:255).
		/// </summary>
		public static Vector4 Tan
		{
			get;
			private set;
		}

		/// <summary>
		/// Teal Cl (R:0,G:128,B:128,A:255).
		/// </summary>
		public static Vector4 Teal
		{
			get;
			private set;
		}

		/// <summary>
		/// Thistle Cl (R:216,G:191,B:216,A:255).
		/// </summary>
		public static Vector4 Thistle
		{
			get;
			private set;
		}

		/// <summary>
		/// Tomato Cl (R:255,G:99,B:71,A:255).
		/// </summary>
		public static Vector4 Tomato
		{
			get;
			private set;
		}

		/// <summary>
		/// Turquoise Cl (R:64,G:224,B:208,A:255).
		/// </summary>
		public static Vector4 Turquoise
		{
			get;
			private set;
		}

		/// <summary>
		/// Violet Cl (R:238,G:130,B:238,A:255).
		/// </summary>
		public static Vector4 Violet
		{
			get;
			private set;
		}

		/// <summary>
		/// Wheat Cl (R:245,G:222,B:179,A:255).
		/// </summary>
		public static Vector4 Wheat
		{
			get;
			private set;
		}

		/// <summary>
		/// White Cl (R:255,G:255,B:255,A:255).
		/// </summary>
		public static Vector4 White
		{
			get;
			private set;
		}

		/// <summary>
		/// WhiteSmoke Cl (R:245,G:245,B:245,A:255).
		/// </summary>
		public static Vector4 WhiteSmoke
		{
			get;
			private set;
		}

		/// <summary>
		/// Yellow Cl (R:255,G:255,B:0,A:255).
		/// </summary>
		public static Vector4 Yellow
		{
			get;
			private set;
		}

		/// <summary>
		/// YellowGreen Cl (R:154,G:205,B:50,A:255).
		/// </summary>
		public static Vector4 YellowGreen
		{
			get;
			private set;
		}

		static ColorsHelper()
		{
			Transparent = Cl.Transparent.ToVec();
			AliceBlue = Cl.AliceBlue.ToVec();
			AntiqueWhite = Cl.AntiqueWhite.ToVec();
			Aqua = Cl.Aqua.ToVec();
			Aquamarine = Cl.Aquamarine.ToVec();
			Azure = Cl.Azure.ToVec();
			Beige = Cl.Beige.ToVec();
			Bisque = Cl.Bisque.ToVec();
			Black = Cl.Black.ToVec();
			BlanchedAlmond = Cl.BlanchedAlmond.ToVec();
			Blue = Cl.Blue.ToVec();
			BlueViolet = Cl.BlueViolet.ToVec();
			Brown = Cl.Brown.ToVec();
			BurlyWood = Cl.BurlyWood.ToVec();
			CadetBlue = Cl.CadetBlue.ToVec();
			Chartreuse = Cl.Chartreuse.ToVec();
			Chocolate = Cl.Chocolate.ToVec();
			Coral = Cl.Coral.ToVec();
			CornflowerBlue = Cl.CornflowerBlue.ToVec();
			Cornsilk = Cl.Cornsilk.ToVec();
			Crimson = Cl.Crimson.ToVec();
			Cyan = Cl.Cyan.ToVec();
			DarkBlue = Cl.DarkBlue.ToVec();
			DarkCyan = Cl.DarkCyan.ToVec();
			DarkGoldenrod = Cl.DarkGoldenrod.ToVec();
			DarkGray = Cl.DarkGray.ToVec();
			DarkGreen = Cl.DarkGreen.ToVec();
			DarkKhaki = Cl.DarkKhaki.ToVec();
			DarkMagenta = Cl.DarkMagenta.ToVec();
			DarkOliveGreen = Cl.DarkOliveGreen.ToVec();
			DarkOrange = Cl.DarkOrange.ToVec();
			DarkOrchid = Cl.DarkOrchid.ToVec();
			DarkRed = Cl.DarkRed.ToVec();
			DarkSalmon = Cl.DarkSalmon.ToVec();
			DarkSeaGreen = Cl.DarkSeaGreen.ToVec();
			DarkSlateBlue = Cl.DarkSlateBlue.ToVec();
			DarkSlateGray = Cl.DarkSlateGray.ToVec();
			DarkTurquoise = Cl.DarkTurquoise.ToVec();
			DarkViolet = Cl.DarkViolet.ToVec();
			DeepPink = Cl.DeepPink.ToVec();
			DeepSkyBlue = Cl.DeepSkyBlue.ToVec();
			DimGray = Cl.DimGray.ToVec();
			DodgerBlue = Cl.DodgerBlue.ToVec();
			Firebrick = Cl.Firebrick.ToVec();
			FloralWhite = Cl.FloralWhite.ToVec();
			ForestGreen = Cl.ForestGreen.ToVec();
			Fuchsia = Cl.Fuchsia.ToVec();
			Gainsboro = Cl.Gainsboro.ToVec();
			GhostWhite = Cl.GhostWhite.ToVec();
			Gold = Cl.Gold.ToVec();
			Goldenrod = Cl.Goldenrod.ToVec();
			Gray = Cl.Gray.ToVec();
			Green = Cl.Green.ToVec();
			GreenYellow = Cl.GreenYellow.ToVec();
			Honeydew = Cl.Honeydew.ToVec();
			HotPink = Cl.HotPink.ToVec();
			IndianRed = Cl.IndianRed.ToVec();
			Indigo = Cl.Indigo.ToVec();
			Ivory = Cl.Ivory.ToVec();
			Khaki = Cl.Khaki.ToVec();
			Lavender = Cl.Lavender.ToVec();
			LavenderBlush = Cl.LavenderBlush.ToVec();
			LawnGreen = Cl.LawnGreen.ToVec();
			LemonChiffon = Cl.LemonChiffon.ToVec();
			LightBlue = Cl.LightBlue.ToVec();
			LightCoral = Cl.LightCoral.ToVec();
			LightCyan = Cl.LightCyan.ToVec();
			LightGoldenrodYellow = Cl.LightGoldenrodYellow.ToVec();
			LightGray = Cl.LightGray.ToVec();
			LightGreen = Cl.LightGreen.ToVec();
			LightPink = Cl.LightPink.ToVec();
			LightSalmon = Cl.LightSalmon.ToVec();
			LightSeaGreen = Cl.LightSeaGreen.ToVec();
			LightSkyBlue = Cl.LightSkyBlue.ToVec();
			LightSlateGray = Cl.LightSlateGray.ToVec();
			LightSteelBlue = Cl.LightSteelBlue.ToVec();
			LightYellow = Cl.LightYellow.ToVec();
			Lime = Cl.Lime.ToVec();
			LimeGreen = Cl.LimeGreen.ToVec();
			Linen = Cl.Linen.ToVec();
			Magenta = Cl.Magenta.ToVec();
			Maroon = Cl.Maroon.ToVec();
			MediumAquamarine = Cl.MediumAquamarine.ToVec();
			MediumBlue = Cl.MediumBlue.ToVec();
			MediumOrchid = Cl.MediumOrchid.ToVec();
			MediumPurple = Cl.MediumPurple.ToVec();
			MediumSeaGreen = Cl.MediumSeaGreen.ToVec();
			MediumSlateBlue = Cl.MediumSlateBlue.ToVec();
			MediumSpringGreen = Cl.MediumSpringGreen.ToVec();
			MediumTurquoise = Cl.MediumTurquoise.ToVec();
			MediumVioletRed = Cl.MediumVioletRed.ToVec();
			MidnightBlue = Cl.MidnightBlue.ToVec();
			MintCream = Cl.MintCream.ToVec();
			MistyRose = Cl.MistyRose.ToVec();
			Moccasin = Cl.Moccasin.ToVec();
			MonoGameOrange = Cl.MonoGameOrange.ToVec();
			NavajoWhite = Cl.NavajoWhite.ToVec();
			Navy = Cl.Navy.ToVec();
			OldLace = Cl.OldLace.ToVec();
			Olive = Cl.Olive.ToVec();
			OliveDrab = Cl.OliveDrab.ToVec();
			Orange = Cl.Orange.ToVec();
			OrangeRed = Cl.OrangeRed.ToVec();
			Orchid = Cl.Orchid.ToVec();
			PaleGoldenrod = Cl.PaleGoldenrod.ToVec();
			PaleGreen = Cl.PaleGreen.ToVec();
			PaleTurquoise = Cl.PaleTurquoise.ToVec();
			PaleVioletRed = Cl.PaleVioletRed.ToVec();
			PapayaWhip = Cl.PapayaWhip.ToVec();
			PeachPuff = Cl.PeachPuff.ToVec();
			Peru = Cl.Peru.ToVec();
			Pink = Cl.Pink.ToVec();
			Plum = Cl.Plum.ToVec();
			PowderBlue = Cl.PowderBlue.ToVec();
			Purple = Cl.Purple.ToVec();
			Red = Cl.Red.ToVec();
			RosyBrown = Cl.RosyBrown.ToVec();
			RoyalBlue = Cl.RoyalBlue.ToVec();
			SaddleBrown = Cl.SaddleBrown.ToVec();
			Salmon = Cl.Salmon.ToVec();
			SandyBrown = Cl.SandyBrown.ToVec();
			SeaGreen = Cl.SeaGreen.ToVec();
			SeaShell = Cl.SeaShell.ToVec();
			Sienna = Cl.Sienna.ToVec();
			Silver = Cl.Silver.ToVec();
			SkyBlue = Cl.SkyBlue.ToVec();
			SlateBlue = Cl.SlateBlue.ToVec();
			SlateGray = Cl.SlateGray.ToVec();
			Snow = Cl.Snow.ToVec();
			SpringGreen = Cl.SpringGreen.ToVec();
			SteelBlue = Cl.SteelBlue.ToVec();
			Tan = Cl.Tan.ToVec();
			Teal = Cl.Teal.ToVec();
			Thistle = Cl.Thistle.ToVec();
			Tomato = Cl.Tomato.ToVec();
			Turquoise = Cl.Turquoise.ToVec();
			Violet = Cl.Violet.ToVec();
			Wheat = Cl.Wheat.ToVec();
			White = Cl.White.ToVec();
			WhiteSmoke = Cl.WhiteSmoke.ToVec();
			Yellow = Cl.Yellow.ToVec();
			YellowGreen = Cl.YellowGreen.ToVec();
		}

		public static Vector4 ToVec(this Cl Cl)
		{
			return new Vector4(Cl.R / 255f, Cl.G / 255f, Cl.B / 255f, Cl.A / 255f);
		}

	}
}
