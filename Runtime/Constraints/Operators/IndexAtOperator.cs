using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public sealed class IndexAtOperator : SelfResolvingOperator
    {
        private readonly int index;

        public IndexAtOperator(int index)
        {
            this.index = index;
            this.left_precedence = this.right_precedence = 1;
        }

        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            if (this.RightContext is null || this.RightContext is BinaryOperator)
            {
                stack.Push(new IndexAtExistsConstraint(this.index));
            }
            else
            {
                stack.Push(new IndexAtConstraint(this.index, stack.Pop()));
            }
        }
    }
}
