using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Web.Routing;

namespace CMajor.Helpers {
    public static class CheckListBoxExtensions {
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, MultiSelectList multiSelectList, object htmlAttributes = null) {
            //Convert selected value list to a List<string> for easy manipulation
            List<string> selectedValues = new List<string>();

            //Create div
            TagBuilder divTag = new TagBuilder("div");
            divTag.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

            //Add checkboxes
            foreach (SelectListItem item in multiSelectList) {
                divTag.InnerHtml += String.Format("<div><input type=\"checkbox\" name=\"{0}\" id=\"{0}_{1}\" " +
                                                    "value=\"{1}\" {2} /><label for=\"{0}_{1}\">{3}</label></div>",
                                                    name,
                                                    item.Value,
                                                    item.Selected ? "checked=\"checked\"" : "",
                                                    item.Text);
            }

            return MvcHtmlString.Create(divTag.ToString());
        }
    }
}