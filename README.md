# IP2Azure

This command-line tool allows you to quickly check if a given IPv4 address is used by an Azure service.

It does so by loading the public list of Azure IPs (which is published at https://www.microsoft.com/en-us/download/details.aspx?id=56519)
and getting all the services whose published subnets contain the given IP.

A copy of this file is stored in this repository, under the `data` directory.

For example, from the `src` directory:

```
❯  dotnet run --azureJson ../data/ServiceTags_Public_20220627.json --address 20.51.10.137 -q
20.51.10.137 AzureResourceManager, AzureResourceManager.WestUS2, AzureCloud.westus2, AzureCloud
```

If the tool finds no matching services, it will only echo bach the given IP. Example:

```
❯  dotnet run --azureJson ../data/ServiceTags_Public_20220627.json --address 127.0.0.1 -q
127.0.0.1
```

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft 
trademarks or logos is subject to and must follow 
[Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship.
Any use of third-party trademarks or logos are subject to those third-party's policies.
