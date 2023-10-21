using Api.EndPoints.Question.ViewModels;
using Api.Infrastructure.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Api.EndPoints.Question;

public static class QuestionEndpoints
{
    public static RouteGroupBuilder MapQuestionApi(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet("/", GetAllQuestions)
            .RequireAuthorization()
            .WithName(nameof(GetAllQuestions));

        return groupBuilder;
    }

    private static Ok<ApiResponse<IReadOnlyCollection<QuestionVm>>> GetAllQuestions()
    {
        return TypedResults.Ok(new ApiResponse<IReadOnlyCollection<QuestionVm>>
        {
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Data = new List<QuestionVm>
            {
                new()
                {
                    Id = 1,
                    Question = "Question 1"
                },
                new()
                {
                    Id = 2,
                    Question = "Question 2"
                },
                new()
                {
                    Id = 3,
                    Question = "Question 3"
                }
            }
        });
    }
}