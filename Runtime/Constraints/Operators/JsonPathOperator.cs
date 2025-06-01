using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public sealed class JsonPathOperator : SelfResolvingOperator
    {
        private readonly string path;

        public JsonPathOperator(string path)
        {
            this.path = path;
            this.left_precedence = this.right_precedence = 1;
        }

        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            if (this.RightContext is null || this.RightContext is BinaryOperator)
            {
                stack.Push(new JsonPathExistsConstraint(this.path));
            }
            else
            {
                stack.Push(new JsonPathConstraint(this.path, stack.Pop()));
            }
        }
    }
}
