using FluentValidation;

namespace CDR_API.Endpoints.CallRecords.Validations;

public class GetAllValidator : DateValidator<GetAllRequest>
{
    public GetAllValidator()
    {
        RuleFor(x => x.Skip)
            .GreaterThan(0)
            .WithMessage("Skip must be greater than 0");
        
        RuleFor(x => x.Take)
            .GreaterThan(0)
            .WithMessage("Take must be greater than 0");
    }
    
}