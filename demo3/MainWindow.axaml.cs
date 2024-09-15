using Avalonia.Controls;
using demo3.Context;
using demo3.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using Type = demo3.Models.Type;

namespace demo3
{
    public partial class MainWindow : Window
    {
        private List<Agent> agents = Helper.DataBase.Agents.Include(x => x.Salehistories).ToList();
        public MainWindow()
        {
            InitializeComponent();

            SetFilterBox();

            InitList();
        }
        
        private void InitList()
        {
            if (FilterComboBox == null || SortComboBox == null || SearchTextBox == null) return;

            var list = agents;


            if (!string.IsNullOrEmpty(SearchTextBox.Text))
            {
                string[] terms = SearchTextBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                list = list.Where(agent =>
                {
                    string[] fields =
                    [
                        agent.Name.ToLower(),
                        agent.Email.ToLower(),
                        agent.Phone.ToLower()
                    ];

                    return terms.Any(term => fields.Any(fild => fild.Contains(term)));

                }).ToList();
            }

            list = FilterComboBox.SelectedIndex switch
            {
                0 => list,
                _ => list.Where(x => x.Type == FilterComboBox.SelectedItem).ToList()
            };

            list = SortComboBox.SelectedIndex switch
            {
                1 => list.OrderBy(x => x.Name).ToList(),    
                2 => list.OrderByDescending(x => x.Name).ToList(),    
                3 => list.OrderBy(x => x.Discount).ToList(),    
                4 => list.OrderByDescending(x => x.Discount).ToList(),    
                5 => list.OrderBy(x => x.Priority).ToList(),    
                6 => list.OrderByDescending(x => x.Priority).ToList(),    
                _ => list
            };

            AgentListBox.ItemsSource = null;
            AgentListBox.ItemsSource = list;
        }

        private void SetFilterBox()
        {
            var list = new List<Type>() { new Type() { Name = "Все"} };

            list.AddRange(Helper.DataBase.Types.ToList());

            FilterComboBox.ItemsSource = list;
            FilterComboBox.SelectedIndex = 0;
        }

        private void TextBox_TextChanged(object? sender, Avalonia.Controls.TextChangedEventArgs e) => InitList();
        
        private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e) => InitList();

        private void Button_Click_Add(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AgentWindow agentWindow = new AgentWindow();    
            agentWindow.Show();
            Close();
        }

        private void Border_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            var agent = AgentListBox.SelectedItem as Agent;

            AgentWindow agentWindow = new AgentWindow(agent!);
            agentWindow.Show();
            Close();
        }
    }
}