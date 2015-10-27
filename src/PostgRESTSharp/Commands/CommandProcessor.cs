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
			using (var sw = new StreamWriter(fileName,false,System.Text.Encoding.ASCII))
			{
				sw.Write(contents);
				sw.Flush();
			}
		}
	}
}

