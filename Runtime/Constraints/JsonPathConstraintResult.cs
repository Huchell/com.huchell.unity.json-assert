using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public sealed class JsonPathConstraintResult : ConstraintResult
    {
        private readonly ConstraintResult baseResult;

        public JsonPathConstraintResult(IConstraint constraint, object actualValue, bool isSuccess)
            : base(constraint, actualValue, isSuccess)
        {
        }

        public JsonPathConstraintResult(IConstraint constraint, object actualValue, ConstraintStatus status)
            : base(constraint, actualValue, status)
        {
        }

        public JsonPathConstraintResult(IConstraint constraint, ConstraintResult baseResult)
            : base(constraint, baseResult.ActualValue, baseResult.Status)
        {
            this.baseResult = baseResult;
        }

        public override void WriteMessageTo(MessageWriter writer)
        {
            this.baseResult?.WriteMessageTo(writer);
        }
    }
}
