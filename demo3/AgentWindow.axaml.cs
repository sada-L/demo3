using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using demo3.Context;
using demo3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace demo3;

public partial class AgentWindow : Window
{
    private Agent CurrentAgent;
    private List<Salehistory> salehistories;
    public AgentWindow()
    {
        CurrentAgent = new Agent();

        InitializeComponent();

        AgentPanel.DataContext = CurrentAgent;
        
        AgentTypeComboBox.ItemsSource = Helper.DataBase.Types.ToList();

        salehistories = Helper.DataBase.Salehistories.Include(x => x.Products).Where(x => x.Agent == CurrentAgent).ToList();
        SaleListBox.ItemsSource = salehistories;
    }

    public AgentWindow(Agent agent)
    {
        InitializeComponent();

        CurrentAgent = agent;

        AgentPanel.DataContext = CurrentAgent;

        AgentTypeComboBox.ItemsSource = Helper.DataBase.Types.ToList();
        AgentTypeComboBox.SelectedItem = CurrentAgent.Type;
        SaleListBox.ItemsSource = CurrentAgent.Salehistories;
    }


    private void Button_Click_Back(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();   
        mainWindow.Show();
        Close();
    }
}