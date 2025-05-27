namespace TatAisEnergo.Core.DTOs.Results
{
    public class Result<TData> : Result
    {
        public TData Data { get; set; }

        public Result()
        {
        }

        public Result<TData> WithData(TData data)
        {
            Data = (TData)(object)data;
            return this;
        }
    }

    public class Result
    {
        public static readonly Result Ok = new Result(Array.Empty<Error>());

        public bool IsSuccess => Errors.Count == 0;

        public IList<Error> Errors { get; }

        public Result() : this(new List<Error>())
        {
        }

        private Result(IList<Error> errors)
        {
            Errors = errors;
        }

        public static implicit operator bool(Result result)
        {
            return result.IsSuccess;
        }
    }
}