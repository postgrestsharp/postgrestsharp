using System;

namespace PostgrestSharp
{
	public class ViewMetaModelBuilderResult
	{
		public ViewMetaModelBuilderResult ( bool wasHandled, IViewMetaModel result)
		{
			this.WasHandled = wasHandled;
			this.Result = result;
		}

		public bool WasHandled { get; protected set; }

		public IViewMetaModel Result { get; protected set; }
	}
}

