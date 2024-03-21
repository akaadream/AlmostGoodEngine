using ExCSS;
using Microsoft.Xna.Framework.Content;

namespace AlmostGoodEngine.CSS
{
	public class CssContentTypeReader : ContentTypeReader<Stylesheet>
	{
		protected override Stylesheet Read(ContentReader input, Stylesheet existingInstance)
		{
			string css = input.ReadString();
			var parser = new StylesheetParser();
			Stylesheet result = parser.Parse(css);
			return result;
		}
	}
}
