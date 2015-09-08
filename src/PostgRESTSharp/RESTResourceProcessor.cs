using PostgRESTSharp.REST;
using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp
{
    public class RESTResourceProcessor : IRESTResourceProcessor
    {
        public IEnumerable<RESTResource> Process(IEnumerable<IViewMetaModel> views, bool isReadOnly)
        {
            var resources = new List<RESTResource>();
            foreach (var view in views)
            {
                var methods = new List<RESTMethod>();

                // GET on Collection
                var getCollectionMethod = new RESTMethod(RESTVerbEnum.GET, RESTVerbDetailEnum.Collection, new RESTParameter[] { }, new RESTParameter[] { });
                methods.Add(getCollectionMethod);

                // GET on Collection Item
                MetaModelViewColumn pkCol = null;
                if (view.Columns.Where(x => x.IsPrimaryKeyColumn).Any())
                {
                    pkCol = view.Columns.Single(x => x.IsPrimaryKeyColumn);
                }
                else
                {
                    if (view.Columns.Where(x => x.IsUniqueColumn).Count() == 1)
                    {
                        pkCol = view.Columns.First(x => x.IsUniqueColumn);
                    }
                }

                var getItemMethod = new RESTMethod(RESTVerbEnum.GET, RESTVerbDetailEnum.Item, new RESTParameter[] { new RESTParameter(pkCol.ColumnName.ToLower(), pkCol.ModelDataType, true) }, new RESTParameter[] { });
                methods.Add(getItemMethod);

                // WRITE ONLY

                // POST Collection

                // PUT Item

                // Patch Item

                // Delete Item

                // Options (SOMETIME)

                var resource = new RESTResource(view.ModelNamePluralised.ToLower(), view.ViewName, view.ModelName, pkCol.ColumnName.ToLower(), methods);
                resources.Add(resource);
            }

            return resources;
        }
    }
}