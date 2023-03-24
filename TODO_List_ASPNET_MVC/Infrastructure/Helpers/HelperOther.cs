using Microsoft.AspNetCore.Authentication.Cookies;

namespace Project_Main.Infrastructure.Helpers
{
    public static class HelperOther
    {
        public const int idBoundryBottom = 0;
        public const int ZeroValueToIndicatesEmptyArray = 0;

		/// <summary>
		/// Create action or operation name base on given parameters.
		/// </summary>
		/// <param name="actionFullName"></param>
		/// <param name="prefix"></param>
		/// <returns></returns>
		public static string GetActionNameForLoggingAndExceptions(string actionFullName, string prefix)
        {
            if (actionFullName.Contains("Async"))
            {
                return prefix + actionFullName.Replace("Async", string.Empty);
            }

            if (prefix.Contains("Controller"))
            {
                return prefix.Replace("Controller", string.Empty) + actionFullName;
            }

            return actionFullName;
        }
    }
}
