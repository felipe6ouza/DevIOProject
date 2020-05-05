using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.Extensions
{
    [HtmlTargetElement ("*", Attributes = "supress-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "supress-by-claim-value")]

    public class ApagaElementoByClaimTagHelper : TagHelper
    {
        [HtmlAttributeName("supress-by-claim-name")]
        public string IdentityClaimName { get; set; }
        
        [HtmlAttributeName("supress-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public IHttpContextAccessor _contextAcessor;

        public ApagaElementoByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAcessor = contextAccessor;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentException(nameof(context));

            if (output == null)
                throw new ArgumentException(nameof(output));

            var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_contextAcessor.HttpContext, IdentityClaimName, IdentityClaimName);

            if (temAcesso == true) return;

            output.SuppressOutput();

        }

    }



    [HtmlTargetElement("a", Attributes = "disable-by-claim-name")]
    [HtmlTargetElement("a", Attributes = "disable-by-claim-value")]
    public class DesabilitaLinkByClaimTagHelper : TagHelper
    {
        [HtmlAttributeName("disable-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("disable-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public IHttpContextAccessor _contextAcessor;

        public DesabilitaLinkByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAcessor = contextAccessor;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentException(nameof(context));

            if (output == null)
                throw new ArgumentException(nameof(output));

            var temAcesso = CustomAuthorization.ValidarClaimsUsuario(_contextAcessor.HttpContext, IdentityClaimName, IdentityClaimName);

            if (temAcesso == true) return;

            output.Attributes.RemoveAll("href");
            output.Attributes.Add(new TagHelperAttribute("style", "cursor: not-allowed"));
            output.Attributes.Add(new TagHelperAttribute("title", "Você não tem permissão"));



        }

    }
}
