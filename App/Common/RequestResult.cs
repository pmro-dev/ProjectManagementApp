using App.Common.Interfaces;

namespace App.Common;

public class RequestResult : IRequestResult
{
	public int StatusCode { get; }
	public string? ErrorMessage { get; }

	public RequestResult(int statusCode, string? errorMessage)
	{
		StatusCode = statusCode;
		ErrorMessage = errorMessage;
	}

	public static RequestResult Success(int statusCode = 200)
	{
		return new RequestResult(statusCode, null);
	}

	public static RequestResult Failure(int statusCode, string errorMessage)
	{
		return new RequestResult(statusCode, errorMessage);
	}
}

public class RequestResult<T> : IRequestResult<T> where T : class, new()
{
	public T? Data { get; }
	public int StatusCode { get; }
	public string? ErrorMessage { get; }

	public RequestResult(T? data, int statusCode, string? errorMessage)
	{
		Data = data;
		StatusCode = statusCode;
		ErrorMessage = errorMessage;
	}

	public static RequestResult<T> Success(T data, int statusCode = 200)
	{
		return new RequestResult<T>(data, statusCode, null);
	}

	public static RequestResult<T> Failure(int statusCode, string errorMessage)
	{
		return new RequestResult<T>(null, statusCode, errorMessage);
	}
}