namespace Common.Errors;

public class ResultWithError<T> : IResultWithError<T>
{

	public ResultWithError(int errorCode, string message,T result)
	{
		ErrorCode = errorCode;
		Message = message;
		Result = result;
	}

	public int ErrorCode { get; }
	public string Message { get; }
	public T Result { get; }
	public bool IsError => ErrorCode != 200;
}