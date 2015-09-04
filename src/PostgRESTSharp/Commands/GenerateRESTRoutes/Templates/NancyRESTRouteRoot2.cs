using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRESTRoutes.Templates
{
    public partial class NancyRESTRouteRoot
    {
        public NancyRESTRouteRoot(IEnumerable<IViewMetaModel> views)
        {
            this.Views = new List<IViewMetaModel>(views);
        }

        public IEnumerable<IViewMetaModel> Views { get; protected set; }
    }
}
