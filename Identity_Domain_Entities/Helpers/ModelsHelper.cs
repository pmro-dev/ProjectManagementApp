namespace Identity_Domain_Entities.Helpers
{
	public static class ModelsHelper
	{
		public static bool IsUserDataFromProviderDifferentToUserDataInDb(ref UserModel userProviderData, ref UserModel userDbData) 
		{
			return !userProviderData.Equals(userDbData);
		}
	}
}
