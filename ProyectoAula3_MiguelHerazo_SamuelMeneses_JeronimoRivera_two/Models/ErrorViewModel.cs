public class ErrorViewModel
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }

    public ErrorViewModel(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
}