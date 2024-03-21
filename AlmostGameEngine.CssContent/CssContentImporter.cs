using ExCSS;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.IO;
using TImport = System.String;

namespace AlmostGameEngine.CssContent
{
	[ContentImporter(".css", DisplayName = "CSS Importer - Almost Good Engine", DefaultProcessor = nameof(CssContentProcessor))]
	public class CssContentImporter : ContentImporter<TImport>
	{
		public override TImport Import(string filename, ContentImporterContext context)
		{
			string css = File.ReadAllText(filename);
			ThrowIfInvalidCss(css);
			return css;
		}

		private static void ThrowIfInvalidCss(string css)
		{
			if (string.IsNullOrEmpty(css))
			{
				throw new InvalidContentException("The CSS file is empty");
			}

			try
			{
				var parser = new StylesheetParser();
				parser.Parse(css);
			}
			catch (Exception ex)
			{
				throw new InvalidContentException("This does not appear to be valid CSS. See inner exception for details", ex);
			}
		}
	}
}
