using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public sealed class IndexAtConstraintResult : ConstraintResult
    {
        private readonly ConstraintResult baseResult;

        public IndexAtConstraintResult(IConstraint constraint, object actualValue, bool isSuccess)
            : base(constraint, actualValue, isSuccess)
        {
        }

        public IndexAtConstraintResult(IConstraint constraint, object actualValue, ConstraintStatus status)
            : base(constraint, actualValue, status)
        {
        }

        public IndexAtConstraintResult(IConstraint constraint, ConstraintResult baseResult)
            : base(constraint, baseResult.ActualValue, baseResult.Status)
        {
        }

        public override void WriteMessageTo(MessageWriter writer)
        {
            base.WriteMessageTo(writer);
            this.baseResult?.WriteMessageTo(writer);
        }
    }
}
