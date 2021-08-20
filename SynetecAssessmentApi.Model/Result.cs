namespace SynetecAssessmentApi.Model
{
    public class Result<T> 
    {
        public T Value { get; }
        public bool IsSucceed { get; }
        public string ErrorMessage { get; }

        protected internal Result(T value, bool isSucceed, string errorMessage)
        {
            Value = value;
            IsSucceed = isSucceed;
            ErrorMessage = errorMessage;
        }
        public static Result<T> Ok(T value)
        {
            return new Result<T>(value, true, null);
        }
        public static Result<T> Fail(string message)
        {
            return new Result<T>(default, false, message);
        }
    }
}
