using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using ODataService.Models;

namespace ODataService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<HrEmployee>("HrEmployees");
            config.MapODataServiceRoute(routeName: "ODataRoute", routePrefix: "api", model: builder.GetEdmModel());
            // Enable odata values
            config.Select().Expand().Filter().OrderBy().MaxTop(null).Count();
        }
    }
}
