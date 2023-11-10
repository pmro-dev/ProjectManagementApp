namespace Domain.Helpers
{
    public static class AttributesHelper
    {
        public const int DescriptionMaxLength = 300;
        public const int DescriptionMinLength = 7;
        public const int TitleMaxLength = 70;
        public const int TitleMinLength = 3;
        public const int NameMaxLength = 70;
        public const int NameMinLength = 3;
		public const string DataFormat = "yyyy-MM-ddTHH:mm";

		#region FOR IDENTITY

		public const int UsernameMaxLength = 100;
		public const int UsernameMinLength = 6;
		public const int FirstNameMaxLength = 70;
		public const int FirstNameMinLength = 3;
		public const int LastNameMaxLength = 70;
		public const int LastNameMinLength = 3;
		public const int RoleNameMaxLength = 70;
		public const int RoleNameMinLength = 5;

		#endregion
	}
}