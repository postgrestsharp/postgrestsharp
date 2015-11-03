using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PostgRESTSharp.Generator.Tests.ProgramTests
{
    [TestFixture]
    public class TestProgram
    {
        [Test]
        public void Main_GivenNoArguments_ShouldNotPerformAnyCommands()
        {
            Assert.DoesNotThrow(() => Program.Main(new string[0]));
        }
    }
}
