using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2024._02._20.wpfappdogagyak;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        WriteRandomCakeOnTheGUI();
        HowManyCakesWereAwarded();
        TheCheapestAndTheMostExpensiveCake();
        ListCakesInThePastryShop();
    }
    public class Cake
    { 
        public string name { get;}
        public string type { get; }
        public bool isAwarded { get; }
        public int price { get; }
        public string mesurement { get; }

        public Cake(string name,string type,bool isAwarded,int price,string mesurement)
        {
            this.name = name;
            this.type = type;
            this.isAwarded = isAwarded;
            this.price = price;
            this.mesurement = mesurement;

        }
        
    }

    List<Cake> ReadCakeTxt()
    {
        StreamReader f = new StreamReader("cuki.txt");
        List<Cake> cakes = new List<Cake>(); 
        while (!f.EndOfStream)
        {
            string[] line = f.ReadLine().Split(";");
            
            cakes.Add(new Cake(line[0],line[1],Convert.ToBoolean(line[2]),Convert.ToInt32(line[3]),line[0]));
        }

        return cakes;
    }
    void WriteRandomCakeOnTheGUI()
    {
        Random r = new Random();
        randomCakeGoesHere.Text = $"Mai ajánlatunk: {ReadCakeTxt()[r.Next(1,ReadCakeTxt().Count)].name}";
    }
    void HowManyCakesWereAwarded()
    {
        int counter = 0;

        foreach (var expr in ReadCakeTxt())
        {
            if (expr.isAwarded) counter++;
        }

        HowManyCakesAreAwarded.Text = $"{counter} féle díjnyertes édességből választhat";
    }

    void TheCheapestAndTheMostExpensiveCake()
    {
        Cake cheap = ReadCakeTxt().MinBy((cake => cake.price ));
        Cake expensive = ReadCakeTxt().MaxBy((cake => cake.price ));

        theMostExpensiveCakeGoesHere.Text = expensive.name;
        theCheapestCakeGoesHere.Text = cheap.name;
        theMostExpensiveCakesPriceGoesHere.Text = $"{expensive.price} Ft / {expensive.mesurement} ";
        theCheapestCakesPriceGoesHere.Text = $"{cheap.price} Ft / {cheap.mesurement}";
    }

    void ListCakesInThePastryShop()
    {
        StreamWriter r = new StreamWriter("lista.txt");
        List<Cake> cakesWithOutRepetition = new List<Cake>();

        foreach (var item in ReadCakeTxt())
        {
            if (!cakesWithOutRepetition.Contains(item))
            {
                cakesWithOutRepetition.Add(item);
                r.WriteLine($"{item.name};{item.type}");
            };
        }
    }
}