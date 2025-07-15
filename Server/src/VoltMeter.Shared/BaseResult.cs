namespace VoltMeter.Shared;

public sealed class BaseResult<T>
{
    public T? Data { get; set; } = default!;
    public List<string>? ErrorMessages { get; set; }
    public bool IsSuccessful { get; set; } = true;
    public int StatusCode { get; set; } = 200;
    public BaseResult()
    {
    }

    public BaseResult(T data)
    {
        Data = data;
    }

    public BaseResult(int statusCode, List<string> errorMessages)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessages = errorMessages;
    }

    public BaseResult(int statusCode, string errorMessage)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessages = new List<string> { errorMessage };
    }
}