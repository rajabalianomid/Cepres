namespace Cepres.Service.API.Models
{
    public class CommonResponse<T> : CommonResponse where T : class
    {
        public T Result { get; set; }
    }

    public class CommonResponse
    {
        public string StatusText { get; set; }
        public bool Status => StatusText == null;
        public string ActionName { get; set; }
    }
}
