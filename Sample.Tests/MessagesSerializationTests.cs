using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using System.Reflection;
using Sample.Messages.Events.People;

namespace Sample.Tests
{
    [TestFixture]
    public class MessagesSerializationTests
    {
        [StaticTestFactory]
        public static IEnumerable<Test> CreateTests()
        {
            TestSuite commandsSuite = new TestSuite("messages");
            foreach (Type message in GetMessages())
            {
                commandsSuite.Children.Add( BuildTestCase(message));
            }
            yield return commandsSuite;
        }

        private static IEnumerable<Type> GetMessages()
        {
            return typeof(Sample.Messages.IEvent).Assembly.GetTypes().Where(t => !t.IsAbstract && !t.IsInterface);
        }

        private static Test BuildTestCase(Type messge)
        {
            string name = string.Format("{0}.{1}", messge.Namespace.Replace(messge.Assembly.GetName().Name + ".", string.Empty), messge.Name);
            return new TestCase(name, () => TestType(messge));

        }           
    
        private static void TestType(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            FieldInfo[] fields = type.GetFields();

            Assert.Count(1, constructors, "The message type has {0} constructors and must have exactly 1", constructors.Count());
            ConstructorInfo constructor = constructors.Single();

            ParameterInfo[] parameters = constructor.GetParameters();
            Assert.Count(fields.Count(), parameters,
                "The constructor parameter {0} count must be the same as the field count {1} .", parameters.Count(), fields.Count());

            Assert.IsTrue(fields.All(f => f.IsPublic && f.IsInitOnly),
                "All fields must be marked public readonly. Not conforming:  {0}",
                fields.Where(f => !(f.IsPublic && f.IsInitOnly)).Select(f => f.Name).ToArray());

            IEnumerable<string> fieldNames = fields.Select(f => f.Name.ToLowerInvariant());
            IEnumerable<string> paramNames = parameters.Select(p => p.Name.ToLowerInvariant());

            Assert.AreElementsEqualIgnoringOrder(fieldNames, paramNames);
        }
    }
}
