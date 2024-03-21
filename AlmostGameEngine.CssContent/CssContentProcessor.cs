using ExCSS;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.ComponentModel;
using TInput = System.String;
using TOutput = AlmostGameEngine.CssContent.CssContentPropertyResult;

namespace AlmostGameEngine.CssContent
{
	[ContentProcessor(DisplayName = "CSS Processor - Almost Good Engine")]
	internal class CssContentProcessor : ContentProcessor<TInput, TOutput>
	{
		[DisplayName("Minify CSS")]
		public bool Minify { get; set; } = true;

		[DisplayName("Runtime Type")]
		public string RuntimeType { get; set; } = string.Empty;

		public override TOutput Process(TInput input, ContentProcessorContext context)
		{
			if (string.IsNullOrEmpty(RuntimeType))
			{
				throw new System.Exception("No Runtime Type was specified for this content.");
			}

			if (Minify)
			{
				input = MinifyCSS(input);
			}

			var result = new CssContentPropertyResult
			{
				Css = input,
				RuntimeType = RuntimeType
			};
			return result;
		}

		private static string MinifyCSS(string css)
		{
			var parser = new StylesheetParser();
			var stylesheet = parser.Parse(css);

			return stylesheet.ToCss(new CompressedStyleFormatter());
		}
	}
}
