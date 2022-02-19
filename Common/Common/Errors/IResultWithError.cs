namespace Common.Errors;

public interface IResultWithError<out T>
{
	int ErrorCode { get; }
	string Message { get; }
	T Result { get; }
	bool IsError { get; }
}