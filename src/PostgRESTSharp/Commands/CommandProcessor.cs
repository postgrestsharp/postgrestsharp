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
			using (var sw = new StreamWriter(fileName,false, new System.Text.UTF8Encoding(false)))
			{
				sw.Write(contents);
				sw.Flush();
			}
		}
	}
}

