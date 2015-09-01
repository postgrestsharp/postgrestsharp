using StructureMap;
using StructureMap.Graph;
using Synoptic;
using PostgrestSharp;
using PostgRESTSharp.Commands;

namespace PostgRESTSharp.Generator
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			IContainer container = new Container(x =>
				{
					x.Scan(y =>
						{
							y.LookForRegistries();
							y.WithDefaultConventions();
							y.AssembliesFromApplicationBaseDirectory();
							y.AddAllTypesOf<IViewMetaModelBuilderConvention>();
						});
				});

			var resolver = new StructureMapCommandDependencyResolver(container);

			// register the available commands
			new CommandRunner()
				.WithDependencyResolver(resolver)
				.WithCommandsFromAssembly(typeof(GenerateViewScriptsCommand).Assembly)
				.Run(args);

			System.Console.WriteLine("Press any key to continue...");

			System.Console.ReadKey();
		}
	}
}
