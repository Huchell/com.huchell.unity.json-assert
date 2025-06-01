using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public sealed class IndexAtConstraint : PrefixConstraint
    {
        private readonly int index;

        public IndexAtConstraint(int index, IConstraint baseConstraint)
            : base(baseConstraint)
        {
            this.index = index;
            this.DescriptionPrefix = $"at index {this.index}";
        }

        public override ConstraintResult ApplyTo(object actual)
        {
            actual = GetTestObject(actual);
            if (actual is not IList list)
            {
                return new IndexAtConstraintResult(this, actual, isSuccess: false);
            }

            if (list.Count <= index)
            {
                return new IndexAtConstraintResult(this, actual, isSuccess: false);
            }

            var obj = this.GetValue(list);
            var result = this.BaseConstraint.ApplyTo(obj);
            return new IndexAtConstraintResult(this, result);
        }

        private object GetTestObject(object actual)
        {
            if (actual is not string str)
            {
                return actual;
            }

            try
            {
                var arr = JArray.Parse(str);
                return arr;
            }
            catch (JsonReaderException)
            {
                return actual;
            }
        }

        private object GetValue(IList list)
        {
            var value = list[this.index];
            return value switch
            {
                JValue jValue => GetValueFromJValue(jValue),
                _ => value,
            };
        }

        private object GetValueFromJValue(JValue value)
        {
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
