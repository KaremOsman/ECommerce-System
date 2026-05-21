using Domain.Entities.OrderModule;

namespace Service.Specifications
{
    internal class OrderSpecifications : BaseSpecification<Order, Guid>
    {
        // Get All Orders By Email
        public OrderSpecifications(string email) : base(O => O.UserEmail == email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }
        // Get Order By Id and Email
        public OrderSpecifications(Guid id) : base(O => O.Id == id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
        }
    }
}
 