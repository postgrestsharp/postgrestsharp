namespace PostgRESTSharp
{
    public class ViewMetaModelRelation
    {
        public ViewMetaModelRelation(IViewMetaModel relatedView, RelationDirectionEnum direction, MetaModelViewColumn relationColumn)
        {
            this.RelatedView = relatedView;
            this.Direction = direction;
            this.RelationColumn = relationColumn;
        }

        public IViewMetaModel RelatedView { get; protected set; }

        public RelationDirectionEnum Direction { get; protected set; }

        public MetaModelViewColumn RelationColumn { get; protected set; }
    }
}