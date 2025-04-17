using System.Linq.Expressions;

namespace Domain.Contracts
{
    public class Specification<T> where T : class
    {
        public Specification(Expression<Func<T,bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T,object>>> Includes { get; }

        protected void AddInclude(Expression<Func<T,object>> expression)
            => Includes.Add(expression);
    }
}
