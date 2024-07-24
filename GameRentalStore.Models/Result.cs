namespace GameRentalStore.Models
{
    public class Result<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool Status { get; set; }
        public string? Action { get; set; }
        public string? Controller { get; set; }
    }
}
