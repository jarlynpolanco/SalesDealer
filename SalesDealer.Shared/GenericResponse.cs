namespace SalesDealer.Shared
{
    public class GenericResponse<T>
    {
        public GenericResponse() { }
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
