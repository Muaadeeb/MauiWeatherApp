using WeatherApp.Common;
using WeatherApp.Services;

namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> WeatherList { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    //TODO: Get by location works great.  Get by city returns different model/data.  Will need custom model maybe.

	public WeatherPage()
	{
		InitializeComponent();
        WeatherList = new List<Models.List>();
	}

    protected override async void OnAppearing()
    {
		base.OnAppearing();
        await GetLocation();
    }

    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        Latitude = location!.Latitude;
        Longitude = location!.Longitude;
    }

    private async void TapLocation_Tapped(object sender, TappedEventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(Latitude, Longitude);
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var result = await ApiService.GetWeather(Latitude, Longitude);
        UpdateUI(result);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var city = await DisplayPromptAsync(title: "", message: "", placeholder: "Search weather by city", accept: "Search",
            cancel: "Cancel");

        if (city is not null)
        {
            await GetWeatherDataByCity(city);
        }
    }


    public async Task GetWeatherDataByCity(string city)
    {
        var result = await ApiService.GetWeatherByCity(city);
        UpdateUI(result);
    }


    public void UpdateUI(dynamic result)
    {
        foreach (var item in result.List)
        {
            WeatherList.Add(item);
        }

        CvWeather.ItemsSource = WeatherList;

        lblCity.Text = result.City.Name;
        lblWeatherDescription.Text = result.List[0].Weather[0].Description;
        lblTemperature.Text = $"{result.List[0].Main.Temperature}{GlobalConstants.TemperatureFahrenheit}";
        lblHumidity.Text = $"{result.List[0].Main.Humidity}%";
        lblWind.Text = $"{result.List[0].Wind.Speed}mph";
        //ImgWeatherIcon.Source = result.List[0].Weather[0].Icon;
        ImgWeatherIcon.Source = result.List[0].Weather[0].CustomIcon;
    }
}