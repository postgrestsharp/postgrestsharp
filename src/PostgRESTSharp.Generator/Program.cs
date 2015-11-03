using System;
using PostgRESTSharp.Commands.GenerateViewScripts;
﻿using PostgRESTSharp.Commands.GenerateRAML;
﻿using System.Diagnostics;
using PostgRESTSharp.Configuration;
using PostgRESTSharp.Conventions;
using PostgRESTSharp.Data;
using PostgRESTSharp.Pgsql;
using StructureMap;
using StructureMap.Graph;
using Synoptic;

namespace PostgRESTSharp.Generator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var generator = new Generator(new Container(), new CommandRunner());
            generator.Process(args);


#if DEBUG
            Console.WriteLine("Done");
#endif
        }
    }
}