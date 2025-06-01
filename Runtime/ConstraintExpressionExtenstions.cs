using JsonAssert.Constraints;
using NUnit.Framework.Constraints;

namespace JsonAssert.Unity
{
    public static class ConstraintExpressionExtensions
    {
        public static Constraint JsonObject(this ConstraintExpression expression)
        {
            return expression.Append(new JsonObjectConstraint());
        }

        public static Constraint JsonArray(this ConstraintExpression expression)
        {
            return expression.Append(new JsonArrayConstraint());
        }

        public static ConstraintExpression JsonProperty(this ConstraintExpression expression, string name)
        {
            return expression.Append(new JsonPropertyOperator(name));
        }

        public static ConstraintExpression JsonPath(this ConstraintExpression expression, string path)
        {
            return expression.Append(new JsonPathOperator(path));
        }

        public static ConstraintExpression IndexAt(this ConstraintExpression expression, int index)
        {
            return expression.Append(new IndexAtOperator(index));
        }
    }
}
