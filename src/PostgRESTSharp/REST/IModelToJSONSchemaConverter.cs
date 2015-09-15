namespace PostgRESTSharp.REST
{
    public interface IModelToJSONSchemaConverter
    {
        string Convert(RESTModel model);

        string ConvertCollection(RESTModel getRestModel);
    }
}