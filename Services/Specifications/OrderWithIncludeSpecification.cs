using Domain.Contracts;
using Domain.Entities.OrderEntities;

namespace Services.Specifications
{
    public class OrderWithIncludeSpecification : Specification<Order>
    {
        public OrderWithIncludeSpecification(Guid id)
            : base(order => order.Id == id)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
        }

        public OrderWithIncludeSpecification(string buyerEmail)
            : base(order => order.BuyerEmail == buyerEmail)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
            SetOrderBy(order => order.OrderDate);
        }
    }
}
