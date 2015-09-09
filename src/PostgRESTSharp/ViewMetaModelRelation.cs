namespace PostgRESTSharp
{
    public class ViewMetaModelRelation
    {
        public ViewMetaModelRelation(IViewMetaModel relatedView, RelationDirectionEnum direction, ViewMetaModelColumn relationColumn)
        {
            this.RelatedView = relatedView;
            this.Direction = direction;
            this.RelationColumn = relationColumn;
        }

        public IViewMetaModel RelatedView { get; protected set; }

        public RelationDirectionEnum Direction { get; protected set; }

        public ViewMetaModelColumn RelationColumn { get; protected set; }
    }
}