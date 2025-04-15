using FastEndpoints;
using FluentValidation;

namespace CDR_API.Endpoints.CallRecords.Validations;

public class GetByIdValidator : Validator<GetByIdRequest>
{
    public GetByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required")
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0");
    }
}