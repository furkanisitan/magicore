namespace Core.Utilities.Results;

public static class ResultMessages
{
    public const string Ok = " The request has been processed successfully.";
    public const string Created = "Resource(s) added successfully.";
    public const string ErrNotFound = "The resource not found.";
    public const string ErrValidation = "A validation error has occurred.";
    public const string ErrUniqueConstraint = "A unique constraint error has occurred.";
    public const string ErrForeignKeyConstraint = "A foreign key constraint error has occurred.";
}