namespace Common.Errors;

public interface IResultWithError<out T> : IResultWithError
{
	T Result { get; }
}

public interface IResultWithError
{
	int ErrorCode { get; }
	string Message { get; }
	bool IsError { get; }
}