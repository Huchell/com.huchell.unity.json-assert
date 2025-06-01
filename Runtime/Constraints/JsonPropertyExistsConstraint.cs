using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public sealed class JsonPropertyExistsConstraint : Constraint
    {
        private readonly string propertyName;

        public JsonPropertyExistsConstraint(string propertyName)
            : base()
        {
            this.propertyName = propertyName;
        }

        public override ConstraintResult ApplyTo(object actual)
        {
            JObject jObject = actual switch
            {
                string str => this.SafeParse(str),
                JObject => (JObject)actual,
                _ => null,
            };
            if (jObject is null)
            {
                return new JsonPropertyConstraintResult(this, actual, ConstraintStatus.Error);
            }

            JProperty property = jObject.Property(this.propertyName);
            return new JsonPropertyConstraintResult(this, actual, property is not null);
        }

        private JObject SafeParse(string str)
        {
            try
            {
                return JObject.Parse(str);
            }
            catch (JsonReaderException)
            {
                return null;
            }
        }
    }
}
