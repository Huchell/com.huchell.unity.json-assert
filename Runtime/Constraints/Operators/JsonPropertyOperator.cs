using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public class JsonPropertyOperator : SelfResolvingOperator
    {
        private readonly string name;

        public JsonPropertyOperator(string name)
        {
            this.name = name;
            left_precedence = right_precedence = 1;
        }

        public override void Reduce(ConstraintBuilder.ConstraintStack stack)
        {
            if (this.RightContext is null || this.RightContext is BinaryOperator)
            {
                stack.Push(new JsonPropertyExistsConstraint(this.name));
            }
            else
            {
                stack.Push(new JsonPropertyConstraint(this.name, stack.Pop()));
            }
        }
    }
}
