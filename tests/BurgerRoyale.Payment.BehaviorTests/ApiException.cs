namespace BurgerRoyale.Payment.BehaviorTests;

public partial class ApiException<TResult>
{
    public override string ToString()
    {
        if (Result is ProblemDetails problem)
        {
            return problem.Detail;
        }

        if (Result is ValidationProblemDetails problemDetails)
        {
            var messages = problemDetails.Errors.Select(error =>
             $"{error.Key} => {string.Join(":", error.Value)}");

            return string.Join(";", messages);
        }

        return string.Format("HTTP Response: \n\n{0}", Response);
    }

    public override string Message => ToString();
}