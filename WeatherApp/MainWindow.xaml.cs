using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeatherApp
{
    public partial class MainWindow : Window
    {
        private readonly WeatherService _weatherService;
        private List<City> _cities;
        public MainWindow()
        {
            InitializeComponent();
            _weatherService = new WeatherService("7d1700393bf490100030b6aa685a6481");
            InitializeCities();
        }
        private void InitializeCities()
        {
            _cities = new List<City>
        {
            new City { Name = "İstanbul", Latitude = 41.0082, Longitude = 28.9784 },
            new City { Name = "Ankara", Latitude = 39.9334, Longitude = 32.8597 },
            new City { Name = "İzmir", Latitude = 38.4237, Longitude = 27.1428 },
            new City { Name = "Bursa", Latitude = 40.1885, Longitude = 29.0610 },
            new City { Name = "Antalya", Latitude = 36.8969, Longitude = 30.7133 },
            new City { Name = "Manisa", Latitude = 38.630554, Longitude = 27.422222 },
            new City { Name = "Diyarbakir", Latitude = 37.910000, Longitude = 40.240002 },
            new City { Name = "Izmit", Latitude = 40.766666, Longitude = 29.916668 },
            new City { Name = "Gaziantep", Latitude = 37.066666, Longitude = 37.383331 },
            new City { Name = "Sanliurfa", Latitude = 37.158333, Longitude = 38.791668 },
            new City { Name = "Tekirdag", Latitude = 40.977779, Longitude = 27.515278 },
            new City { Name = "Adana", Latitude = 37.000000, Longitude = 35.321335 },
            new City { Name = "Antalya", Latitude = 36.884804, Longitude = 30.704044 },
            new City { Name = "Giresun", Latitude = 40.917534, Longitude = 38.392654 },
            new City { Name = "Yozgat", Latitude = 39.822060, Longitude = 34.808132 },
            new City { Name = "Bolu", Latitude = 40.731647, Longitude = 31.589813 },
            new City { Name = "Zonguldak", Latitude = 41.451733, Longitude = 31.791344 },
            new City { Name = "Van", Latitude = 38.499817, Longitude = 43.378143 },
            new City { Name = "Denizli", Latitude = 37.783333, Longitude = 29.094715 },
            new City { Name = "Eskisehir", Latitude = 39.766193, Longitude = 30.526714 },
            new City { Name = "Artvin", Latitude = 41.183224, Longitude = 41.830982 },
            new City { Name = "Isparta", Latitude = 37.768002, Longitude = 30.561905 },
            new City { Name = "Konya", Latitude = 37.874641, Longitude = 32.493156 },
            new City { Name = "Nevsehir", Latitude = 38.626995, Longitude = 34.719975 },
            new City { Name = "Mersin", Latitude = 36.812103, Longitude = 34.641479 },
            new City { Name = "Rize", Latitude = 41.025513, Longitude = 40.517666 },
            new City { Name = "Kahramanmaras", Latitude = 37.575275, Longitude = 36.922821 },
            new City { Name = "Bursa", Latitude = 40.193298, Longitude = 29.074202 },
            new City { Name = "Kayseri", Latitude = 38.734802, Longitude = 35.467987 },
            new City { Name = "Elazig", Latitude = 38.680969, Longitude = 39.226398 },
            new City { Name = "Sakarya", Latitude = 40.783333, Longitude = 30.400000 },
            new City { Name = "Hatay", Latitude = 36.200001, Longitude = 36.166668 },
            new City { Name = "Edirne", Latitude = 41.674965, Longitude = 26.583481 },
            new City { Name = "Malatya", Latitude = 38.356869, Longitude = 38.309669 },
            new City { Name = "Sivas", Latitude = 39.750359, Longitude = 37.015598 },
            new City { Name = "Trabzon", Latitude = 41.002697, Longitude = 39.716763 },
            new City { Name = "Amasya", Latitude = 40.652382, Longitude = 35.828819 },
            new City { Name = "Afyonkarahisar", Latitude = 38.756886, Longitude = 30.538704 },
            new City { Name = "London", Latitude = 51.509865, Longitude = -0.118092 },
            new City { Name = "Moscow", Latitude = 55.751244, Longitude = 37.618423 },
            new City { Name = "Florida", Latitude = 27.994402, Longitude = -81.760254 },
            new City { Name = "Texas", Latitude = 31.000000, Longitude = -100.000000 },
            new City { Name = "Washington", Latitude = 47.751076, Longitude = -120.740135 },
            new City { Name = "California", Latitude = 36.778259, Longitude = -119.417931 },
            new City { Name = "Munich", Latitude = 48.137154, Longitude = 48.137154 }
        };

            cmbCities.ItemsSource = _cities;
            cmbCities.DisplayMemberPath = "Name";
        }
        private async void btnGetWeather_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCity = cmbCities.SelectedItem as City;
                if (selectedCity == null)
                {
                    MessageBox.Show("Lütfen bir şehir seçin!");
                    return;
                }

                btnGetWeather.IsEnabled = false;
                progressBar.Visibility = Visibility.Visible;

                var weather = await _weatherService.GetWeatherAsync(
                    selectedCity.Latitude,
                    selectedCity.Longitude
                );

                double celsius = weather.Main.Temp - 273.15;

                txtTemperature.Text = $"{celsius:F1}°C";
                txtFeelsLike.Text = $"Hissedilen: {(weather.Main.Feels_like - 273.15):F1}°C";
                txtDescription.Text = char.ToUpper(weather.Weather[0].Description[0]) + weather.Weather[0].Description.Substring(1);
                txtHumidity.Text = $"Nem: {weather.Main.Humidity}%";
                txtPressure.Text = $"Basınç: {weather.Main.Pressure} hPa";
                txtWind.Text = $"Rüzgar: {weather.Wind.Speed} m/s";
                txtVisibility.Text = $"Görüş: {weather.Visibility / 1000} km";

                string iconUrl = $"http://openweathermap.org/img/w/{weather.Weather[0].Icon}.png";
                weatherIcon.Source = new BitmapImage(new Uri(iconUrl));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            finally
            {
                btnGetWeather.IsEnabled = true;
                progressBar.Visibility = Visibility.Collapsed;
            }
        }
    }
}