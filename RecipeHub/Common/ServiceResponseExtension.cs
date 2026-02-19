namespace RecipeHub.Common;

public static class ServiceResponseExtension
{
    public static IResult ToHttpResult<T>(this ServiceResponse<T> result)
    {
        return result.StatusCode switch
        {
            200 => Results.Ok(result.Data),
            201 => Results.Created(string.Empty, result.Data),
            400 => Results.BadRequest(new { result.Message, result.Errors }),
            401 => Results.Unauthorized(),
            403 => Results.Forbid(),
            404 => Results.NotFound(new { result.Message }),
            _ => Results.Problem(result.Message)
        };
    }
}