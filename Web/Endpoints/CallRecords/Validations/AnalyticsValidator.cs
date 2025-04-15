using CDR_API.Endpoints.CallRecords.BaseClasses;
using FluentValidation;

namespace CDR_API.Endpoints.CallRecords.Validations;

public abstract class AnalyticsRequestValidator<T> : DateValidator<T> where T : BaseAnalyticsRequest
{
    protected AnalyticsRequestValidator()
    {
        RuleFor(x => x.Sort)
            .Must(x => string.IsNullOrEmpty(x) || x.Equals("asc", StringComparison.CurrentCultureIgnoreCase) ||
                       x.Equals("desc", StringComparison.CurrentCultureIgnoreCase))
            .WithMessage("Sort value can only be 'asc' or 'desc'");
    }
}