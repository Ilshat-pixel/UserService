using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace UserService.Application.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IHttpContextAccessor httpContextAccessor)
        {
            var device = httpContextAccessor.HttpContext?.Request.Headers["x-Device"].ToString();

            if (device == "mail")
            {
                RuleFor(user => user.FirstName)
                    .NotEmpty()
                    .WithMessage("Имя обязательно для mail устройства");
                RuleFor(user => user.Email)
                    .NotEmpty()
                    .WithMessage("Email обязателен для mail устройства")
                    .EmailAddress()
                    .WithMessage("Неверный формат Email");
            }
            else if (device == "mobile")
            {
                RuleFor(user => user.Phone)
                    .NotEmpty()
                    .WithMessage("Номер телефона обязателен для mobile устройства")
                    .Matches(@"^7\d{10}$")
                    .WithMessage("Неверный формат номера телефона");
            }
            else if (device == "web")
            {
                RuleFor(user => user.FirstName)
                    .NotEmpty()
                    .WithMessage("Имя обязательно для web устройства");
                RuleFor(user => user.LastName)
                    .NotEmpty()
                    .WithMessage("Фамилия обязательна для web устройства");
                RuleFor(user => user.BirthDay)
                    .NotEmpty()
                    .WithMessage("Дата рождения обязательна для web устройства");
                RuleFor(user => user.PassportId)
                    .NotEmpty()
                    .WithMessage("Номер паспорта обязателен для web устройства")
                    .Matches(@"^\d{4} \d{6}$")
                    .WithMessage("Неверный формат номера паспорта");
                RuleFor(user => user.BirthPlace)
                    .NotEmpty()
                    .WithMessage("Место рождения обязательно для web устройства");
                RuleFor(user => user.RegistrationAddress)
                    .NotEmpty()
                    .WithMessage("Адрес регистрации обязателен для web устройства");
            }
            else
            {
                RuleFor(user => user)
                    .Custom(
                        (user, context) =>
                        {
                            context.AddFailure("Неверное значение заголовка x-Device");
                        }
                    );
            }

            // Общие правила валидации
            RuleFor(user => user.Email)
                .EmailAddress()
                .When(user => !string.IsNullOrEmpty(user.Email))
                .WithMessage("Неверный формат Email");
            RuleFor(user => user.Phone)
                .Matches(@"^7\d{10}$")
                .When(user => !string.IsNullOrEmpty(user.Phone))
                .WithMessage("Неверный формат номера телефона");
            RuleFor(user => user.PassportId)
                .Matches(@"^\d{4} \d{6}$")
                .When(user => !string.IsNullOrEmpty(user.PassportId))
                .WithMessage("Неверный формат номера паспорта");
        }
    }
}
