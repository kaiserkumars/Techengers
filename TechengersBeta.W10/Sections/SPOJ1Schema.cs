using System;
using AppStudio.DataProviders;

namespace TechengersBeta.Sections
{
    /// <summary>
    /// Implementation of the SPOJ1Schema class.
    /// </summary>
    public class SPOJ1Schema : SchemaBase
    {

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
