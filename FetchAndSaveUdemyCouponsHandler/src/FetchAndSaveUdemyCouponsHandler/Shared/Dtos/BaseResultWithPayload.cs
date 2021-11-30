namespace FetchAndSaveUdemyCouponsHandler.Shared.Dtos
{
    public class BaseResultWithPayload<T> : BaseResult
    {
        public T Data { get; set; } 
    }
}