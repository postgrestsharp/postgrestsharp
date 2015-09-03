using System;
using System.IO;

namespace PostgRESTSharp.Commands
{
	public abstract class CommandProcessor
	{
		public CommandProcessor ()
		{
		}

		protected void WriteFileContents(string fileName, string contents)
		{
			using (var sw = new StreamWriter(fileName))
			{
				sw.Write(contents);
				sw.Flush();
			}
		}
	}
}

