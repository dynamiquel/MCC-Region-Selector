using System;
using System.Collections.Generic;
using System.Text;

namespace MCCRegionSelector
{
    public static class Regions
    {
        private static readonly Dictionary<Region, List<string>> regions;

        static Regions()
        {
            regions = new Dictionary<Region, List<string>>
            {
                [Region.E_US] = new List<string>()
                {
                    "0.0.0.0 pfmsqosprod.eastus.cloudapp.azure.com",
                    "0.0.0.0 pfmsqosprod.eastus2.cloudapp.azure.com",
                    "0.0.0.0 xblcxplatqos-eus-9-18-2-0.cloudapp.net"
                },
                [Region.NC_US] = new List<string>()
                {
                    "0.0.0.0 pfmsqosprod.northcentralus.cloudapp.azure.com",
                    "0.0.0.0 xblcxplatqos-ncus-9-18-2-0.cloudapp.net"
                },
                [Region.C_US] = new List<string>()
                {
                    "0.0.0.0 pfmsqosprod.centralus.cloudapp.azure.com",
                    "0.0.0.0 xblcxplatqos-cus-9-18-2-0.cloudapp.net"
                },
                [Region.SC_US] = new List<string>()
                {
                    "0.0.0.0 pfmsqosprod.southcentralus.cloudapp.azure.com",
                    "0.0.0.0 xblcxplatqos-scus-9-18-2-0.cloudapp.net"
                },
                [Region.W_US] = new List<string>()
                {
                    "0.0.0.0 pfmsqosprod.westus.cloudapp.azure.com",
                    "0.0.0.0 xblcxplatqos-wus-9-18-2-0.cloudapp.net"
                },
                [Region.N_EU] = new List<string>()
                {
                    "0.0.0.0 pfmsqosprod.northeurope.cloudapp.azure.com",
                    "0.0.0.0 xblcxplatqos-neu-9-18-2-0.cloudapp.net"
                },
                [Region.W_EU] = new List<string>()
                {
                    "0.0.0.0 pfmsqosprod.westeurope.cloudapp.azure.com",
                    "0.0.0.0 xblcxplatqos-weu-9-18-2-0.cloudapp.net"
                },
                [Region.SE_AS] = new List<string>()
                {
                    "0.0.0.0 pfmsqosprod.southeastasia.cloudapp.azure.com",
                    "0.0.0.0 xblcxplatqos-seas-9-18-2-0.cloudapp.net"
                },
                [Region.E_AS] = new List<string>()
                {
                    "0.0.0.0 pfmsqosprod.eastasia.cloudapp.azure.com",
                    "0.0.0.0 xblcxplatqos-eas-9-18-2-0.cloudapp.net"
                },
                [Region.E_AU] = new List<string>()
                {
                    "0.0.0.0 pfmsqosprod.australiaeast.cloudapp.azure.com",
                    "0.0.0.0 xblcxplatqos-aue-9-18-2-0.cloudapp.net"
                },
                [Region.SE_AU] = new List<string>()
                {
                    "0.0.0.0 pfmsqosprod.australiasoutheast.cloudapp.azure.com",
                    "0.0.0.0 pfmsqosprod2.australiasoutheast.cloudapp.azure.com",
                    "0.0.0.0 xblcxplatqos-ause-9-18-2-0.cloudapp.net"
                },
                [Region.W_JA] = new List<string>()
                {
                    "0.0.0.0 pfmsqosprod.japanwest.cloudapp.azure.com",
                    "0.0.0.0 xblcxplatqos-jaw-9-18-2-0.cloudapp.net"
                },
                [Region.E_JA] = new List<string>()
                {
                    "0.0.0.0 pfmsqosprod.japaneast.cloudapp.azure.com",
                    "0.0.0.0 xblcxplatqos-jae-9-18-2-0.cloudapp.net"
                },
            };
        }

        public static bool ContainsKey(Region region)
        {
            return regions.ContainsKey(region);
        }

        public static List<string> GetHostNames(Region region)
        {
            if (ContainsKey(region))
                return regions[region];

            return null;
        }
    }

    public enum Region
    {
        E_US,
        NC_US,
        C_US,
        SC_US,
        W_US,
        N_EU,
        W_EU,
        E_AS,
        SE_AS,
        W_JA,
        E_JA,
        E_AU,
        SE_AU
    }
}
