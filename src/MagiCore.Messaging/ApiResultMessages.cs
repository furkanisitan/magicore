namespace MagiCore.Messaging;

public static class ApiResultMessages
{
    public const string Ok = "The request has been processed successfully.";
    public const string Created = "Resource(s) added successfully.";

    public const string ErrBadRequest = "Invalid bad request.";
    public const string ErrNotFound = "The resource not found.";
    public const string ErrValidation = "A validation error has occurred.";
    public const string ErrUniqueConstraint = "A unique constraint error has occurred.";
    public const string ErrForeignKeyConstraint = "A foreign key constraint error has occurred.";
    public const string ErrInternalServer = "An internal server error has occurred.";
}