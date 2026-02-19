namespace RecipeHub.Common;

public static class ServiceResponseExtension
{
    public static IResult ToHttpResult<T>(this ServiceResponse<T> result)
    {
        var body = new
        {
            message = result.Message,
            data = result.Data,
            errors = result.Errors
        };     return result.StatusCode switch
        {
            200 => Results.Ok(body),
            201 => Results.Created(string.Empty, body),
            400 => Results.BadRequest(body),
            401 => Results.Unauthorized(),
            403 => Results.Forbid(),
            404 => Results.NotFound(body),
            422 => Results.UnprocessableEntity(body),
            _   => Results.Problem(
                detail: result.Message,
                statusCode: result.StatusCode)
        };
    }
}