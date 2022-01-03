using System.Text;

namespace Core.Utilities.Results
{
    public static class ApiResult
    {
        public const string SuccessOk = " The request has been processed successfully.";
        public const string SuccessCreated = "Resource(s) added successfully.";
        public const string ErrNotFound = "The resource not found.";
        public const string ErrValidation = "A validation error has occurred.";
        public const string ErrUniqueConstraint = "A unique constraint error has occurred.";
        public const string ErrForeignKeyConstraint = "A foreign key constraint error has occurred.";

        public static IResult Ok() =>
            Result.Builder().Success().Message(SuccessOk).Build();

        public static IResult<T> Created<T>(T payload) =>
            Result.Builder(payload).Success().Message(SuccessCreated).Build();

        public static IResult NotFound(string? name, params KeyValuePair<string, object>[] parameters) =>
            Result.Builder().Message(ErrNotFound).AddError(BuildNotFoundMessage(name, parameters)).Build();

        private static string BuildNotFoundMessage(string? name, params KeyValuePair<string, object>[] parameters)
        {
            if (name == null) return string.Empty;

            var builder = new StringBuilder(name + " not found");

            if (parameters.Length > 0)
                builder.Append(" for parameters");

            foreach (var (key, value) in parameters)
                builder.Append($" {{{key}='{value}'}}");

            return builder.Append('.').ToString();
        }
    }
}
