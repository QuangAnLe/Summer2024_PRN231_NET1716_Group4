namespace MilkTeaServices.IServices
{
    public interface IPaymentServices
    {
        Task<Dictionary<string, object>> CreateOrder();
    }
}
