namespace Project_IdentityDomainEntities.Helpers
{
	public static class ModelsHelper
	{
		public static bool IsUserDataFromProviderDifferentToUserDataInDb(ref UserModel userProviderData, ref UserModel userDbData) 
		{
			return !userProviderData.Equals(userDbData);
		}
	}
}
