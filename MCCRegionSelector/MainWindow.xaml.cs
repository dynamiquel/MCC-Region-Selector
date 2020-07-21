using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCCRegionSelector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, Region> stringToRegion;

        public MainWindow()
        {
            InitializeComponent();

            stringToRegion = new Dictionary<string, Region>
            {
                ["Central US"] = Region.C_US,
                ["North Central US"] = Region.NC_US,
                ["East US"] = Region.E_US,
                ["South Central US"] = Region.SC_US,
                ["West US"] = Region.W_US,
                ["North Europe"] = Region.N_EU,
                ["West Europe"] = Region.W_EU,
                ["East Asia"] = Region.E_AS,
                ["South East Asia"] = Region.SE_AS,
                ["East Japan"] = Region.E_JA,
                ["West Japan"] = Region.W_JA,
                ["East Australia"] = Region.E_AU,
                ["South East Australia"] = Region.SE_AU,
                ["Brazil"] = Region.BZ
            };

            try
            {
                GetHostsFile();
            }
            catch
            {
                MessageBox.Show("An error occured. Ensure this app is being run in Admin mode.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var selectedRegions = GetSelectedRegionsFromHostsFile();
            foreach (ListBoxItem item in regionsListBox.Items)
            {
                Region region = stringToRegion[item.Content as string];
                if (selectedRegions.Contains(region))
                {
                    item.IsSelected = true;
                    item.Focus();
                }
            }
        }

        private static string GetHostsFile()
        {
            return File.ReadAllText(@"C:\Windows\System32\drivers\etc\hosts");
        }

        private static void SaveHostsFile(string newHosts)
        {
            File.WriteAllText(@"C:\Windows\System32\drivers\etc\hosts", newHosts);
        }

        private void regionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            applyButton.IsEnabled = true;

            var listBox = sender as ListBox;
        }

        private string ConfigureHostsFile(string hostsFile)
        {
            var hostsToAddSB = new StringBuilder("\n");

            foreach (ListBoxItem item in regionsListBox.Items)
            {
                Region region = stringToRegion[item.Content as string];
                var hosts = Regions.GetHostNames(region);

                // Remove existing.
                // Selected = to not include.
                if (item.IsSelected)
                {
                    foreach (var host in hosts)
                    {
                        if (hostsFile.Contains(host))
                            hostsFile = hostsFile.Replace($"\n{host}", string.Empty);
                    }
                }
                // Add new.
                else
                {
                    foreach (var host in hosts)
                    {
                        if (!hostsFile.Contains(host))
                            hostsToAddSB.AppendLine(host);
                    }
                }
            }

            hostsFile += hostsToAddSB.ToString();

            // Removes the extra empty lines.
            hostsFile = hostsFile
                .Replace("\r\n\r\n", string.Empty)
                .Replace("\r\r", string.Empty)
                .Replace("\n\n", string.Empty);

            return hostsFile;
        }

        private static IEnumerable<Region> GetSelectedRegionsFromHostsFile()
        {
            var hostsFile = GetHostsFile();
            var regions = Enum.GetValues(typeof(Region)).Cast<Region>();
            var regionHostNames = regions.ToDictionary(r => r, r => Regions.GetHostNames(r));
            var selectedRegions = regionHostNames.Where(r => !r.Value.All(h => hostsFile.Contains(h))).Select(r => r.Key);
            return selectedRegions;
        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string hostsFile = GetHostsFile();
                hostsFile = ConfigureHostsFile(hostsFile);
                SaveHostsFile(hostsFile);

                MessageBox.Show("Server regions adjusted.", "Success", MessageBoxButton.OK, MessageBoxImage.None);

            }
            catch (Exception)
            {
                MessageBox.Show("Could not adjust the server regions. Ensure this app is being run in Admin mode.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void selectAllButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (ListBoxItem item in regionsListBox.Items)
            {
                item.IsSelected = true;
            }
        }

        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            Debug.WriteLine(cb.Parent);
        }
    }
}
