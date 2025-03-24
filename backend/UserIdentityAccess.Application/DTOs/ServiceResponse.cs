namespace UserIdentityAccess.Application.DTOs;

public class ServiceResponse<T>(bool success, T? data, List<string> errors)
{
    public T? Data { get; set; } = data;
    public bool Success { get; set; } = success;
    public List<string> Errors { get; set; } = errors;
}