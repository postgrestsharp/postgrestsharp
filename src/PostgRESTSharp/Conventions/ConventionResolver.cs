using System;
using System.Linq;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public class ConventionResolver : IConventionResolver
	{
        private IList<Type> rootConventions;
        private IList<string> rootConventionExclusions;
        private IDictionary<Type, IDictionary<Type, ConventionHolder>> conventionsByRoot;

		public void Initialise (IEnumerable<IConvention> conventions)
		{
            this.conventionsByRoot = new Dictionary<Type, IDictionary<Type, ConventionHolder>>();
            this.rootConventions = new List<Type>() { typeof(ITableConvention), typeof(IViewConvention) };
            this.rootConventionExclusions = new List<string>() {"IConvention", "IDefaultConvention", "IImplicitConvention", "IExplicitConvention" };
            foreach(var rootConvention in this.rootConventions)
            {
                // add the root convention itself
                this.rootConventionExclusions.Add(rootConvention.Name);
                // add the default version
                this.rootConventionExclusions.Add("IDefault" + rootConvention.Name.Substring(1, rootConvention.Name.Length - 1));
                // add the implcit version
                this.rootConventionExclusions.Add("IImplicit" + rootConvention.Name.Substring(1, rootConvention.Name.Length - 1));
                // add the explicit version
                this.rootConventionExclusions.Add("IExplicit" + rootConvention.Name.Substring(1, rootConvention.Name.Length - 1));
            }

            // look through the conventions and categorise them
            foreach(var convention in conventions)
            {
                Type conventionType = convention.GetType();
                // what is the root convention for this convention
                var rootConvention = conventionType.GetInterfaces().Single(x => this.rootConventions.Contains(x));
                if (!this.conventionsByRoot.ContainsKey(rootConvention))
                {
                    this.conventionsByRoot[rootConvention] = new Dictionary<Type, ConventionHolder>();
                }

                var conventionInterface = conventionType.GetInterfaces().Single(x => !this.rootConventionExclusions.Contains(x.Name));
                if (!this.conventionsByRoot[rootConvention].ContainsKey(conventionInterface))
                {
                    var defaultImplementation = conventions.OfType<IDefaultConvention>().SingleOrDefault(x => conventionInterface.IsAssignableFrom(x.GetType()));
                    // check that defaults are never null

                    var implicitImplementations = conventions.OfType<IImplicitConvention>().Where(x => conventionInterface.IsAssignableFrom(x.GetType()));

                    var explicitImplementations = conventions.OfType<IExplicitConvention>().Where(x => conventionInterface.IsAssignableFrom(x.GetType()));
                    // we should check that there are no identical explicits

                    var conventionHolder = new ConventionHolder(conventionInterface, defaultImplementation, implicitImplementations, explicitImplementations);

                    this.conventionsByRoot[rootConvention].Add(conventionInterface, conventionHolder);
                }
            }
        }
        
		public T ResolveTableConvention<T>(ITableMetaModel metaModel) 
            where T : class, ITableConvention
		{
            if (this.conventionsByRoot.ContainsKey(typeof(ITableConvention)))
            {
                if(this.conventionsByRoot[typeof(ITableConvention)].ContainsKey(typeof(T)))
                {
                    var conventionHolder = this.conventionsByRoot[typeof(ITableConvention)][typeof(T)];
                    // do the explicits
                    foreach (var explicitConvention in conventionHolder.Explicits.OfType<IExplicitTableConvention>())
                    {
                        if(explicitConvention.DatabaseName == metaModel.DatabaseName &&
                            explicitConvention.SchemaName == metaModel.SchemaName &&
                            explicitConvention.TableName == metaModel.TableName)
                        {
                            var exResult = (T)explicitConvention;
                            if(exResult == null)
                            {
                                throw new Exception("this shouldn't happen");
                            }
                            return exResult;
                        }
                    }

                    // do the implicits
                    foreach (var implicitConvention in conventionHolder.Implicits.OfType<IImplicitTableConvention>())
                    {
                        if(implicitConvention.IsMatch(metaModel))
                        {
                            var imResult = (T)implicitConvention;
                            return imResult;
                        }
                    }

                    // do the default
                    var defResult = (T)conventionHolder.Default;
                    return defResult;
                }
            }
            return default(T);
		}

        public IEnumerable<T> ResolveViewConventions<T>(IViewMetaModel metaModel)
            where T : class, IViewConvention
        {
            if (this.conventionsByRoot.ContainsKey(typeof(IViewConvention)))
            {
                if (this.conventionsByRoot[typeof(IViewConvention)].ContainsKey(typeof(T)))
                {
                    var conventionHolder = this.conventionsByRoot[typeof(IViewConvention)][typeof(T)];
                    // do the explicits
                    foreach (var explicitConvention in conventionHolder.Explicits.OfType<IExplicitViewConvention>())
                    {

                        IList<T> exResults = new List<T>();

                        if (explicitConvention.DatabaseName == metaModel.DatabaseName &&
                            explicitConvention.SchemaName == metaModel.SchemaName &&
                            explicitConvention.ViewName == metaModel.ViewName)
                        {
                            var exResult = (T)explicitConvention;
                            if (exResult == null)
                            {
                                throw new Exception("this shouldn't happen");
                            }
                            exResults.Add(exResult);
                            return exResults;
                        }
                    }

                    // do the implicits
                    IList<T> imResults = new List<T>();
                    foreach (var implicitConvention in conventionHolder.Implicits.OfType<IImplicitViewConvention>())
                    {
                        if (implicitConvention.IsMatch(metaModel))
                        {
                            imResults.Add((T) implicitConvention);
                        }
                    }
                    
                    if (imResults.Count > 0)
                    {
                        return imResults;    
                    }
                    

                    // do the default
                    var defResults = new List<T>() { (T)conventionHolder.Default };
                    return defResults;
                }
            }
            return default(List<T>);
        }

	}
}

