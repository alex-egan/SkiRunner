namespace SkiRunnerWebService.Models;

public class ServiceResponse<T> {
    public T? Data { get; set; }
    public bool Success = false;
    public string Message { get; set; } = "";

    public ServiceResponse() {}

    public ServiceResponse(string message) {
        Message = message;
    }

    public ServiceResponse(bool success, string message) : this(message) {
        Success = success;
    }

    public ServiceResponse(T? data, bool success, string message) : this(success, message) {
        Data = data;
    }
}