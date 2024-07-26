
using System.Text.RegularExpressions;

namespace NetCoreProyectExample.CustomConstraints
{
    public class MonthsCustomConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(routeKey))
            {
                return false; //not a match
            }

            Regex regex = new Regex("^(apr|jul|oct)$");
            string? value = Convert.ToString(values[routeKey]);
            if (regex.IsMatch(value))
            {
                return true;
            }
            return false;
        }
    }
}
