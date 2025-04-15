using CDR_API.Endpoints.CallRecords.BaseClasses;
using FastEndpoints;
using FluentValidation;

namespace CDR_API.Endpoints.CallRecords.Validations;

public class DateValidator<T> : Validator<T> where T : BaseDateRequest 
{
    public DateValidator()
    {
        RuleFor(x => x.DateTo)
            .NotEmpty()
            .WithMessage("Date from is required");

        RuleFor(x => x.DateTo)
            .NotEmpty()
            .WithMessage("Date to is required");
    }
}