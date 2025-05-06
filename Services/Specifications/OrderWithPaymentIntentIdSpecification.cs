using Domain.Contracts;
using Domain.Entities.OrderEntities;

namespace Services.Specifications
{
    public class OrderWithPaymentIntentIdSpecification(string paymentIntentId)
        : Specification<Order>(order => order.PaymentIntentId == paymentIntentId)
    {
    }
}
