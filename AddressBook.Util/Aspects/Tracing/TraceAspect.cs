using System;
using System.Reflection;
using System.Threading;
using Castle.Core;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;
using AddressBook.Util.Logging;

namespace AddressBook.Util.Aspects.Tracing
{
    public class TraceAspect : IInterceptor, IOnBehalfAware
    {
        private string name;

        #region IInterceptor Members

        /// <summary>
        /// Adds Tracing via Log.Verbose.Write to any Intercepted class
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            MethodInfo method = invocation.Method;
            ParameterInfo[] parameters = method.GetParameters();

            var parameterdictionary = new ParameterDictionary<string, object>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                parameterdictionary[parameters[i].Name] = invocation.Arguments[i];
            }

            var cp = new CallProperties
            {
                HasReturnValue = method.ReturnType != typeof(void),
                MethodName = method.Name,
                ServiceName = name,
                Parameters = parameterdictionary,
                ReturnValue = method.ReturnType != typeof(void) ? invocation.ReturnValue : null
            };

            Logger.Verbose.Write("T{0}\t{1} Enter, Call Properties: {2}", threadId, method.ToString(), cp);

            try
            {
                invocation.Proceed();
                cp.ReturnValue = method.ReturnType != typeof(void) ? invocation.ReturnValue : null;
                if (cp.ReturnValue != null)
                    Logger.Verbose.Write("T{0}\t{1} Returning {2}", threadId, method.Name, cp.ReturnValue.ToString());
                else
                    Logger.Verbose.Write("T{0}\t{1} Exit", threadId, method.Name);
            }
            catch (Exception e)
            {
                Logger.Error.Write(e);
                throw;
            }

        }

        #endregion

        #region IOnBehalfAware Members

        public void SetInterceptedComponentModel(ComponentModel target)
        {
            name = target.Service.Name;
        }

        #endregion


    }
}
