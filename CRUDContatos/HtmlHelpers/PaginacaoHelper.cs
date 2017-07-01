using CRUDContatos.Models;
using System;
using System.Text;
using System.Web.Mvc;

namespace CRUDContatos.HtmlHelpers
{
    /// <summary>
    /// Classe utilizada como helper de paginação
    /// </summary>
    public static class PaginacaoHelper
    {
        /// <summary>
        /// Gera os comandos helper responsável pela paginação
        /// </summary>
        public static MvcHtmlString PageLinks(this HtmlHelper html, Paginacao paginacao, Func<int, string> paginaUrl)
        {
            StringBuilder sbResultado = new StringBuilder();
            TagBuilder tag = null;

            for (int count = 1; count <= paginacao.TotalPaginas; count++)
            {
                tag = new TagBuilder("a");
                tag.MergeAttribute("href", paginaUrl(count));
                tag.InnerHtml = count.ToString();
                tag.AddCssClass(count == paginacao.PaginaAtual ? "btn btn-default btn-primary selected" : "btn btn-default");
                sbResultado.Append(tag);
            }

            return MvcHtmlString.Create(sbResultado.ToString());
        }
    }
}