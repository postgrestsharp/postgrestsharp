namespace PostgRESTSharp.Templates
{
    public partial class ViewScript
    {
        public ViewScript(IViewMetaModel metaModel)
        {
            this.MetaModel = metaModel;
        }

        public IViewMetaModel MetaModel { get; protected set; }
    }
}