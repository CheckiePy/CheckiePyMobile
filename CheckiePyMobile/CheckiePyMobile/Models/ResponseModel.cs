namespace CheckiePyMobile.Models
{
    public class ResponseModel<T>
    {
        public T Result { get; set; }
        public string Detail { get; set; }
    }
}
