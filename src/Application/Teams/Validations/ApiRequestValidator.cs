using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

using FluentValidation;
using FluentValidation.Results;

using Domain.Entities;

namespace Application.Requests.Validations
{
    public class ApiRequestValidator : AbstractValidator<ApiRequest>
    {
        private static readonly string[] Methods = new string[] {
            "POST", "PUT", "DELETE"
        };

        public ApiRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor<string>(x => x.Entity).NotEmpty();
            RuleFor(x => x.OrganizationUnits).NotEmpty();
            RuleFor(x => x.Verb)
                .Must(
                    verb =>
                        Methods.ToList().Contains(verb)
                )
                .WithMessage(
                    $"'verb' can only either be [{string.Join(", ", Methods)}]"
                );
            RuleFor(x => x.Endpoint).NotEmpty();
            RuleFor(x => x.ClientEmail)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Headers)
                .Must(json => {
                    try
                    {
                        if (!String.IsNullOrEmpty(json))
                        {
                            JsonSerializer
                                .Deserialize<IDictionary<string, string>>(
                                    json
                                );
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                })
                .WithMessage(
                    "'headers' must be an object with "
                    + "string key and string value"
                );
        }

        public static ValidationResult ValidateRequest(ApiRequest request)
        {
            ApiRequestValidator validator = new ApiRequestValidator();
            return validator.Validate(request);
        }
    }
}