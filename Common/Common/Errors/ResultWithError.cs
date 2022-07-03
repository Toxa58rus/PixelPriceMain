namespace Common.Errors;

public class ResultWithError<T> : ResultWithError, IResultWithError<T>
{

	public ResultWithError(int errorCode, string message, T result)
		: base(errorCode, message)
	{
		Result = result;
	}

	public T Result { get; }

}

public class ResultWithError: IResultWithError
{
	public ResultWithError(int errorCode, string message)
	{
		ErrorCode = errorCode;
		Message = message;
	}

	public int ErrorCode { get; }
	public string Message { get; }
	public bool IsError => ErrorCode != 200;


}

