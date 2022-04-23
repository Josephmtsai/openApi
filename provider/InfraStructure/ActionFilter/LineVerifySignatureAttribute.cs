
using Microsoft.AspNetCore.Mvc;

namespace provider.InfraStructure.ActionFilter
{
    public class LineVerifySignatureAttribute : TypeFilterAttribute
    {
        public LineVerifySignatureAttribute() : base(typeof(LineVerifySignatureFilter))
        {
        }
    }
}
