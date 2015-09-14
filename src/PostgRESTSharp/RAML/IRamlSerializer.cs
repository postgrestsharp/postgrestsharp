using Raml.Parser.Expressions;

namespace PostgRESTSharp.RAML
{
    public interface IRamlSerializer
    {
        string Serialize(RamlDocument ramlDocument);
    }
}
