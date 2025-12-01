using Core.Application.Dtos.Requests;
using Core.Application.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace backend.Tests.Validators;

public class RegisterUserRequestValidatorTests
{
    private readonly RegisterUserRequestValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var request = new RegisterUserRequest("", "password123", "username");
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var request = new RegisterUserRequest("invalid-email", "password123", "username");
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        var request = new RegisterUserRequest("test@example.com", "", "username");
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Too_Short()
    {
        var request = new RegisterUserRequest("test@example.com", "short", "username");
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Should_Have_Error_When_Username_Is_Empty()
    {
        var request = new RegisterUserRequest("test@example.com", "password123", "");
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }

    [Fact]
    public void Should_Have_Error_When_Username_Is_Too_Short()
    {
        var request = new RegisterUserRequest("test@example.com", "password123", "ab");
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }

    [Fact]
    public void Should_Have_Error_When_Username_Is_Too_Long()
    {
        var longUsername = new string('a', 31);
        var request = new RegisterUserRequest("test@example.com", "password123", longUsername);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Request_Is_Valid()
    {
        var request = new RegisterUserRequest("test@example.com", "password123", "username");
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
