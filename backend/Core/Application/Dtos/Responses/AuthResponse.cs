namespace Core.Application.Dtos.Responses;

public record AuthResponse(
    UserResponse User,
    string Token,
    string RefreshToken
);