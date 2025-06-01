using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public sealed class JsonPropertyConstraint : PrefixConstraint
    {
        private readonly string propertyName;

        public JsonPropertyConstraint(string propertyName, IResolveConstraint baseConstraint)
            : base(baseConstraint)
        {
            this.propertyName = propertyName;
            this.DescriptionPrefix = "JSON property";
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
            if (property is null)
            {
                return new JsonPropertyConstraintResult(this, actual, ConstraintStatus.Failure);
            }

            var value = this.GetValueFromProperty(property);
            var baseResult = this.BaseConstraint.ApplyTo(value);
            return new JsonPropertyConstraintResult(this, baseResult);
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

        private object GetValueFromProperty(JProperty property)
        {
            var value = property.Value;
            return value.Type switch
            {
                JTokenType.Boolean => value.ToObject<bool>(),
                JTokenType.Integer => value.ToObject<int>(),
                JTokenType.Float => value.ToObject<float>(),
                JTokenType.String => value.ToObject<string>(),
                _ => (object)value,
            };
        }
    }
}
