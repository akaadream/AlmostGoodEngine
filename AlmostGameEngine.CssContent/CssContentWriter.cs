using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using TInput = AlmostGameEngine.CssContent.CssContentPropertyResult;

namespace AlmostGameEngine.CssContent
{
	[ContentTypeWriter]
	internal class CssContentWriter : ContentTypeWriter<TInput>
	{
		protected override void Write(ContentWriter output, TInput value)
		{
			output.Write(value.Css);
		}

		public override string GetRuntimeReader(TargetPlatform targetPlatform)
		{
			return "AlmostGoodEngine.CSS.CssContentTypeReader, AlmostGoodEngine.CSS";
		}
	}
}
