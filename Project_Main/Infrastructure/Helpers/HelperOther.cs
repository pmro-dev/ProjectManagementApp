namespace Project_Main.Infrastructure.Helpers
{
    public static class HelperOther
    {
		/// <summary>
		/// Create action's or operation's name based on provided parameters.
		/// </summary>
		/// <param name="actionFullName"></param>
		/// <param name="prefix"></param>
		/// <returns></returns>
		public static string CreateActionNameForLoggingAndExceptions(string actionFullName, string prefix)
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
