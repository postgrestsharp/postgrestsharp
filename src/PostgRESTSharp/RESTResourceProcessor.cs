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

                // create a response schema for the collection get

                methods.Add(getCollectionMethod);

                // GET on Collection Item
                ViewMetaModelColumn pkCol = null;
                if (view.Columns.Where(x => x.IsPrimaryKeyColumn).Any())
                {
                    pkCol = view.Columns.Single(x => x.IsPrimaryKeyColumn);
                }
                else
                {
                    pkCol = view.Columns.FirstOrDefault(x => x.IsUniqueColumn);
                }

                var getItemMethod = new RESTMethod(RESTVerbEnum.GET, RESTVerbDetailEnum.Item, new RESTParameter[] { new RESTParameter(pkCol.ColumnName.ToLower(), pkCol.ModelDataType, true) }, new RESTParameter[] { });

                // create a response schema for the collection item get

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