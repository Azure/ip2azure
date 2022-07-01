using System.Net;

namespace IP2Azure
{
    // Represents a range of IP addresses (such as "192.168.1.0/24"). The address and
    // the subnet mask are stored as IPAddress classes to facilitate the matching.
    class IPRange
    {
        public IPRange(string range)
        {
            var tokens = range.Split("/");

            Subnet = IPAddress.Parse(tokens[0]);
            Mask = GetSubnetMask(Int32.Parse(tokens[1]));
            Range = range;
        }

        public IPAddress Subnet { get; }
        public IPAddress Mask { get; }
        public string Range { get; }

        public bool Contains(IPAddress addr)
        {
#pragma warning disable 0618
            // .Address is deprecated, but for the purpose of this analyzer it's the best
            // way to do subnet checking.
            return Subnet.Address == (addr.Address & Mask.Address);
#pragma warning restore 0618
        }

        // TODO: memoize if needed.
        private IPAddress GetSubnetMask(int bits)
        {
            var stringMask = new String('1', bits) + new string('0', 32 - bits);
            var intMask = Convert.ToUInt32(stringMask, 2);
            var bytes = System.BitConverter.GetBytes(intMask);

            if (System.BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return new IPAddress(bytes);
        }

        public override string ToString()
        {
            return Range;
        }

        public static bool IsIpV4Range(string range)
        {
            try
            {
                var subnet = IPAddress.Parse(range.Split("/")[0]);
                return subnet.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
