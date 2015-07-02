﻿using System;
using Sdl.Web.Common.Configuration;

namespace Sdl.Web.Common.Models
{
    /// <summary>
    /// Represents the View Model for a Page
    /// </summary>
#pragma warning disable 618
    // TODO DXA 2.0: Should inherit directly from ViewModel, but for now we need the legacy classes inbetween for compatibility.
    public class PageModel : WebPage
#pragma warning restore 618
    {
        private const string _xpmPageSettingsMarkup = "<!-- Page Settings: {{\"PageID\":\"{0}\",\"PageModified\":\"{1}\",\"PageTemplateID\":\"{2}\",\"PageTemplateModified\":\"{3}\"}} -->";
        private const string _xpmPageScript = "<script type=\"text/javascript\" language=\"javascript\" defer=\"defer\" src=\"{0}/WebUI/Editors/SiteEdit/Views/Bootstrap/Bootstrap.aspx?mode=js\" id=\"tridion.siteedit\"></script>";

        /// <summary>
        /// Gets the Page Regions.
        /// </summary>
        public new RegionModelSet Regions
        {
            get
            {
                return _regions;
            }
        }

        /// <summary>
        /// Initializes a new instance of PageModel.
        /// </summary>
        /// <param name="id">The identifier of the Page.</param>
        public PageModel(string id)
            : base(id)
        {
        }

        #region Overrides

        /// <summary>
        /// Gets the rendered XPM markup
        /// </summary>
        /// <param name="localization">The context Localization.</param>
        /// <returns>The XPM markup.</returns>
        public override string GetXpmMarkup(Localization localization)
        {
            if (XpmMetadata == null)
            {
                return string.Empty;
            }

            string cmsUrl;
            if (!XpmMetadata.TryGetValue("CmsUrl", out cmsUrl))
            {
                cmsUrl = localization.GetConfigValue("core.cmsurl");
            }
            if (cmsUrl.EndsWith("/"))
            {
                // remove trailing slash from cmsUrl if present
                cmsUrl = cmsUrl.Remove(cmsUrl.Length - 1);
            }

            return string.Format(
                _xpmPageSettingsMarkup,
                XpmMetadata["PageID"],
                XpmMetadata["PageModified"],
                XpmMetadata["PageTemplateID"],
                XpmMetadata["PageTemplateModified"]
                ) + 
                string.Format(_xpmPageScript, cmsUrl);
        }

        #endregion  
    }

}
