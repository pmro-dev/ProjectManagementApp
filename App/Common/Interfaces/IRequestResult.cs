namespace App.Common.Interfaces
{
    public interface IRequestResult<T> where T : class, new()
    {
        T? Data { get; }
        string? ErrorMessage { get; }
        int StatusCode { get; }
    }

	public interface IRequestResult
	{
		string? ErrorMessage { get; }
		int StatusCode { get; }
	}
}