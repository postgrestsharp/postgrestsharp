using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
    public class ConventionHolder
    {
        public ConventionHolder(Type conventionType, IDefaultConvention defaultConvention,
            IEnumerable<IImplicitConvention> implicits,
            IEnumerable<IExplicitConvention> explicits)
        {
            this.ConventionType = conventionType;
            this.Default = defaultConvention;
            this.Implicits = new List<IImplicitConvention>(implicits);
            this.Explicits = new List<IExplicitConvention>(explicits);
        }

        public Type ConventionType { get; protected set; }

        public IDefaultConvention Default { get; protected set; }

        public IEnumerable<IImplicitConvention> Implicits { get; protected set; }

        public IEnumerable<IExplicitConvention> Explicits { get; protected set; }
    }
}