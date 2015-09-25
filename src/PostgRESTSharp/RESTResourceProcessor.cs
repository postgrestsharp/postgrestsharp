using PostgRESTSharp.REST;
using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp
{
    public class RESTResourceProcessor : IRESTResourceProcessor
    {
        private IRESTModelBuilder restModelBuilder;
        private IModelToJSONSchemaConverter schemaConverter;
        public RESTResourceProcessor(IRESTModelBuilder restModelBuilder, IModelToJSONSchemaConverter schemaConverter)
        {
            this.restModelBuilder = restModelBuilder;
            this.schemaConverter = schemaConverter;
        }

        public IEnumerable<RESTResource> Process(IEnumerable<IViewMetaModel> views, bool isReadOnly)
        {
            var resources = new List<RESTResource>();
            foreach (var view in views)
            {
                var methods = new List<RESTMethod>();
                var getRestModel = this.restModelBuilder.BuildRESTModel(view, RESTVerbEnum.GET);

                // create a response schema for the collection get
                string collectionGetSchemaDef = this.schemaConverter.ConvertCollection(getRestModel);

                // GET on Collection
                var getCollectionMethod = new RESTMethod(RESTVerbEnum.GET, RESTVerbDetailEnum.Collection, new RESTParameter[] { }, new RESTParameter[] { },
                    new List<RESTResponseDefinition>() { new RESTResponseDefinition(System.Net.HttpStatusCode.OK, collectionGetSchemaDef, "Some Example") }
                );

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

                // create a response schema for the collection item get

                string itemGetSchemaDef = this.schemaConverter.Convert(getRestModel);

                

                var getItemMethod = new RESTMethod(RESTVerbEnum.GET, RESTVerbDetailEnum.Item, new RESTParameter[] {
                    new RESTParameter(pkCol.ColumnName.ToLower(), pkCol.ModelDataType, true)
                }, new RESTParameter[] { },
                new List<RESTResponseDefinition>() { new RESTResponseDefinition(System.Net.HttpStatusCode.OK, itemGetSchemaDef, "Some Example") }
                );


                methods.Add(getItemMethod);

                // WRITE ONLY

                // POST Collection

                // PUT Item

                // Patch Item

                // Delete Item

                // Options (SOMETIME)


                //prepare claims 
                var roleClaims = new List<string>();
                IEnumerable<Grantee> grantees = GetAllSelectGrantees(view);
                foreach (var grantee in grantees)
                {
                    roleClaims.Add(grantee.Name);
                }

                var resource = new RESTResource(view.ModelNamePluralised.ToLower(), view.ViewName, view.ModelName, pkCol.ColumnName.ToLower(), methods, roleClaims, view.SchemaName);
                resources.Add(resource);
            }

            return resources;
        }

        private static IEnumerable<Grantee> GetAllSelectGrantees(IViewMetaModel view)
        {
            return view.PrimarySource.Privileges
                .Where(x => x.Type.ToLower().Equals("select"))
                .GroupBy(x => x.Grantee)
                .Select(group => new Grantee(@group.Key));
        }

    }

    public class Grantee
    {
        public Grantee(string name)
        {
            Name = name;
        }
        public string Name { get; protected set; }
    }
}