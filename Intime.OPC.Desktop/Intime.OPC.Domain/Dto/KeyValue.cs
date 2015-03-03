namespace Intime.OPC.Domain.Dto
{
    public class KeyValue<TKey>
    {
        public KeyValue()
        {
        }

        public KeyValue(TKey key, string value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key { get; set; }

        public string Value { get; set; }
    }


    public class KeyValue : KeyValue<int>
    {
        public KeyValue()
        {
        }

        public KeyValue(int key, string value) : base(key, value)
        {
        }
    }
}