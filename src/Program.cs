using CommandLine;
using Newtonsoft.Json;
using System.Net;
using IP2Azure;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed<Options>(opts =>
        {
            IPAddress? ip = null;
            try {
                ip = IPAddress.Parse(opts.Address);
            }
            catch (FormatException)
            {
                Console.WriteLine($"{opts.Address} is not a valid IPV4 address.");
                Environment.Exit(1);
            }
            string json = System.IO.File.ReadAllText(opts.AzureJson);

            // Map of IP Range (as string) to a list of services.
            var servicesMap = JsonConvert.DeserializeObject<ServiceRanges>(json)?.GetServicesByRange();

            if (servicesMap is null) {
                Console.WriteLine($"Could not parse the JSON file {opts.AzureJson}.");
                Environment.Exit(1);
            }

            // Emit a warning if non-IPv4 ranges are found in the JSON file.
            var nonIpv4Count = servicesMap.Keys.Where(s => !IPRange.IsIpV4Range(s)).Count();
            if (nonIpv4Count > 0 && !opts.Quiet)
            {
                Console.WriteLine($"WARNING: the JSON file contains {nonIpv4Count} non-IPv4 ranges, but this tool only supports IPv4.");
            }

            var ranges = servicesMap.Keys.Where(s => IPRange.IsIpV4Range(s)).Select(s => new IPRange(s));
            var services = new List<String>();
            foreach (var range in ranges)
            {
                if (range.Contains(ip))
                {
                    services.AddRange(servicesMap[range.Range]);
                }
            }
            var svcList = String.Join(", ", services);
            Console.WriteLine($"{ip} {svcList}");
        }
    );

class Options
{
    [Option('a', "address", Required = true, HelpText = "IPv4 Address to check.")]
    public string Address{ get; set; } = default!;

    [Option('j', "azureJson", Required = true, HelpText = "JSON file with Azure IP ranges.")]
    public string AzureJson { get; set; } = default!;

    [Option ('q', "quiet", Default = false, HelpText = "Suppress warnings and other miscellaneous messages.")]
    public bool Quiet { get; set; } = default!;
}
