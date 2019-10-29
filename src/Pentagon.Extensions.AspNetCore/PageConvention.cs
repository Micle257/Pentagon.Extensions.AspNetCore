// -----------------------------------------------------------------------
//  <copyright file="HostedService.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.AspNetCore
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Primitives;

    public class PageConvention
    {
        public string PageName { get; set; }

        public StringValues Routes { get; set; }

        public bool PreCache { get; set; }
    }
}