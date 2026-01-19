namespace PrintingHouse.Domain.Models.Shared
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
        public T Data { get; set; }
    }
}
