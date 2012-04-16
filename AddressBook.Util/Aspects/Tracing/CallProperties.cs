

namespace AddressBook.Util.Aspects.Tracing
{
    internal class CallProperties
    {
        public bool HasReturnValue { get; set; }
        public string ServiceName { get; set; }
        public string MethodName { get; set; }
        public ParameterDictionary<string, object> Parameters { get; set; }
        public object ReturnValue { get; set; }

        public override string ToString()
        {
            return Parameters.Count > 0
                       ?
                           string.Format("{{ ServiceName = {0}, Parameters = {1} }}", ServiceName, Parameters)
                       : string.Format("{{ ServiceName = {0} }}", ServiceName);
        }
    }
}
