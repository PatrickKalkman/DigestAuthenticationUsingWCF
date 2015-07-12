using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rhino.Mocks;

namespace DigestAuthenticationOnWCFTest
{
   /// <summary>
   /// This class is responsible for creating an instance of an object and
   /// pass default values to its constructor.
   /// </summary>
   /// <typeparam name="TObject">The object type to create.</typeparam>
   public class MockCreator<TObject> where TObject : class
   {
      public static TObject Create()
      {
         List<object> argumentsForContructor = GetArgumentsForContructor();
         return MockRepository.GenerateMock<TObject>(argumentsForContructor.ToArray());
      }

      public static TObject CreateDynamicMock(MockRepository mockRepository)
      {
         List<object> argumentsForContructor = GetArgumentsForContructor();
         return mockRepository.DynamicMock<TObject>(argumentsForContructor.ToArray());
      }

      public static TObject Create(MockRepository mockRepository)
      {
         List<object> argumentsForContructor = GetArgumentsForContructor();
         return mockRepository.StrictMock<TObject>(argumentsForContructor.ToArray());
      }

      public static TObject Stub(MockRepository mockRepository)
      {
         List<object> argumentsForContructor = GetArgumentsForContructor();
         return mockRepository.Stub<TObject>(argumentsForContructor.ToArray());
      }

      private static List<object> GetArgumentsForContructor()
      {
         Type objectType = typeof(TObject);
         ConstructorInfo constructorInfo = objectType.GetConstructors(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault();
         if (constructorInfo == null)
            constructorInfo = objectType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault();

         if (constructorInfo == null)
         {
            throw new InvalidOperationException("Unable to create mock object because no constructor can be found.");
         }
         var argumentsForContructor = new List<object>();
         List<ParameterInfo> parameterInfos = constructorInfo.GetParameters().ToList();
         parameterInfos.ForEach(parameterInfo => argumentsForContructor.Add(GetParameterDefaultValue(parameterInfo.ParameterType)));
         return argumentsForContructor;
      }

      private static object GetParameterDefaultValue(Type parameterType)
      {
         if (!parameterType.IsValueType)
         {
            if (parameterType.IsGenericType &&
                parameterType.Name.Equals("IEnumerable`1", StringComparison.InvariantCultureIgnoreCase))
            {
               return CreateGenericList(parameterType);
            }

            return null;
         }
         return Activator.CreateInstance(parameterType);
      }

      private static object CreateGenericList(Type parameterType)
      {
         Type genericListType = typeof(List<>);
         Type[] genericArguments = parameterType.GetGenericArguments();
         Type genericEnumerableType = genericListType.MakeGenericType(genericArguments);
         return Activator.CreateInstance(genericEnumerableType);
      }
   }
}
