namespace MilkTeaRepository.IRepo
{
    public interface IPaymentRepo
    {
        Task<Dictionary<string, object>> CreateOrder(int id);
    }
}
