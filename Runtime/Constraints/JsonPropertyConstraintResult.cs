using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public sealed class JsonPropertyConstraintResult : ConstraintResult
    {
        private readonly ConstraintResult baseResult;

        public JsonPropertyConstraintResult(IConstraint constraint, object actualValue, bool isSuccess)
            : base(constraint, actualValue, isSuccess)
        {
        }

        public JsonPropertyConstraintResult(IConstraint constraint, object actualValue, ConstraintStatus status)
            : base(constraint, actualValue, status)
        {
        }

        public JsonPropertyConstraintResult(IConstraint constraint, ConstraintResult baseResult)
            : base(constraint, baseResult.ActualValue, baseResult.Status)
        {
            this.baseResult = baseResult;
        }

        public override void WriteMessageTo(MessageWriter writer)
        {
            base.WriteMessageTo(writer);
            this.baseResult?.WriteMessageTo(writer);
        }
    }
}
