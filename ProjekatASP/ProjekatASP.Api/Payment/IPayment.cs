namespace ProjekatASP.Api.Payment
{
    public interface IPayment
    {
        bool Pay(decimal amount);
    }
}
