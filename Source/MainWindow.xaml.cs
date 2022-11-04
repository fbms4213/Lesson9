using Microsoft.Maps.MapControl.WPF;
using System;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace Source;

public partial class MainWindow : Window, INotifyPropertyChanged
{

    private BakuBus? _bakuBus;
    public BakuBus? BakuBus
    {
        get { return _bakuBus; }
        set
        {
            _bakuBus = value;
            NotifyPropertyChanged(nameof(BakuBus));
        }
    }








    public MainWindow()
    {
        var bingMapKey = "ldaR7JaahPkEzvf19WmD~k907NUcuJ92udF7Jp8GGjQ~Arv3YEfHiisoQLzEanbLupoXqIZOOO4RmK9Bdnn4Q4l_o_ryp7iEB2T1GBCqz_Go";

        InitializeComponent();
        DataContext = this;

        // m.CredentialsProvider = new ApplicationIdCredentialsProvider(bingMapKey);


        //var timer = new Timer();
        //timer.Interval = 1000;
        //timer.Elapsed += Timer_Elapsed;
        //timer.Start();




        DispatcherTimer timer = new();

        timer.Interval = new TimeSpan(1000);
        timer.Tick += Timer_Tick; ;
        timer.Start();




        //// timer.Stop();

    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        txt.Text = DateTime.Now.ToLongTimeString();
    }



    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        // txt.Text = DateTime.Now.ToLongTimeString();
        Dispatcher.Invoke(() =>
        {
            txt.Text = DateTime.Now.ToLongTimeString();
        });
    }







    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        //// with API
        // var client = new HttpClient();
        // var jsonString = await client.GetStringAsync("https://www.bakubus.az/az/ajax/apiNew1");




        //// with Json file
        var fileName = "bakubusApi.json";
        var dir = new DirectoryInfo($"../../../");

        var filePath = Path.Combine(dir.FullName, fileName);
        var jsonString = await File.ReadAllTextAsync(filePath);






        BakuBus = JsonSerializer.Deserialize<BakuBus>(jsonString);
    }






    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


}