namespace IP2Azure
{
    // Represents a mapping of IP ranges to multiple services.
    // Used to represent the data in the lists of IP ranges for Azure services published as
    // JSON files.
    public class ServiceRanges
    {
        public class Property
        {
            public int ChangeNumber { get; set; }
            public string? Region { get; set; }
			public string? Platform { get; set; }
            public string? SystemService { get; set; }
            public List<String> AddressPrefixes { get; set; } = new();
        }
        public class Service
        {
            public string Name { get; set; } = default!;
            public string Id { get; set; } = default!;
			public Property Properties = new();
        }

        public int ChangeNumber { get; set; }
        public string Cloud { get; set; } = default!;
        public List<Service> Values { get; set; } = new();

        // Returns a dictionary mapping an IP range (expressed as address/maskBits) to a list
        // of services that use that range.
        public Dictionary<string, List<string>> GetServicesByRange()
        {
            var result = new Dictionary<string, List<string>>();
            foreach (var service in Values)
            {
                foreach (var prefix in service.Properties.AddressPrefixes)
                {
                    if (!result.ContainsKey(prefix))
                    {
                        result[prefix] = new List<string>();
                    }
                    result[prefix].Add(service.Name);
                }
            }
            return result;
        }
    }
}
