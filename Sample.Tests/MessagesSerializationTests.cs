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
        /// <summary>
        /// Build the test suite dynamically to be able to se which type fails the tests.
        /// </summary>
        [StaticTestFactory]
        public static IEnumerable<Test> CreateTests()
        {
            // build suite
            TestSuite commandsSuite = new TestSuite("messages");

            foreach (Type message in GetMessages())
            {
                // build test case for each message type
                commandsSuite.Children.Add( BuildTestCase(message));
            }
            yield return commandsSuite;
        }

        /// <summary>
        /// Verify the expectations on a message type.
        /// </summary>
        /// <param name="type">The message type to assert on.</param>
        private static void TestType(Type type)
        {
            // get the fields of the type
            FieldInfo[] fields = type.GetFields();
            
            // all fields must be public readonly
            Assert.IsTrue(fields.All(f => f.IsPublic && f.IsInitOnly),
                "All fields must be marked public readonly. Not conforming:  {0}",
                fields.Where(f => !(f.IsPublic && f.IsInitOnly)).Select(f => f.Name).ToArray());

            // get the constructors of the type
            ConstructorInfo[] constructors = type.GetConstructors();
            
            // the type must have exactly one constructor
            Assert.Count(1, constructors, "The message type has {0} constructors and must have exactly 1", constructors.Count());
            ConstructorInfo constructor = constructors.Single();

            // get the parameters of the constructor
            ParameterInfo[] parameters = constructor.GetParameters();
            
            // the parameter count must be exactly as the field count
            Assert.Count(fields.Count(), parameters,
                "The constructor parameter {0} count must be the same as the field count {1} .", parameters.Count(), fields.Count());
            
            // get the names of the fields
            IEnumerable<string> fieldNames = fields.Select(f => f.Name.ToLowerInvariant());

            // get the names of the constructor parameters 
            IEnumerable<string> paramNames = parameters.Select(p => p.Name.ToLowerInvariant());

            // assert they are the same
            Assert.AreElementsEqualIgnoringOrder(fieldNames, paramNames);
        }

        /// <summary>
        /// Get all the message types from the messages assembly
        /// </summary>
        private static IEnumerable<Type> GetMessages()
        {
            return typeof(Sample.Messages.IEvent).Assembly.GetTypes().Where(t => !t.IsAbstract && !t.IsInterface);
        }

        private static Test BuildTestCase(Type messge)
        {
            string name = string.Format("{0}.{1}", messge.Namespace.Replace(messge.Assembly.GetName().Name + ".", string.Empty), messge.Name);
            return new TestCase(name, () => TestType(messge));

        }           
    
        
    }
}
