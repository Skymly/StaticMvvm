using System;
using System.Collections.Generic;
using System.Text;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using StaticMvvm.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StaticMvvm.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        

        public MainWindowViewModel()
        {
            Title = "StaticMvvm  如何绑定静态成员";
        }

        static MainWindowViewModel()
        {
            Message = "Hello World";
            Number = 666;
            Items = new ObservableCollection<Item>();
        }

        #region instance fileds and properties
        public string Title { get; set; }
        #endregion

        #region Static fileds and properties

        public static ObservableCollection<Item> Items { get => items; set => SetStaticProperty(ref items, value); }

        private static string message;
        public static string Message
        {
            get { return message; }
            set { SetStaticProperty(ref message, value); }
        }

        private static double number;
        public static double Number
        {
            get { return number; }
            set { SetStaticProperty(ref number, value); }
        }

        private static DelegateCommand updateSourceCommand;
        private static ObservableCollection<Item> items;

        public static DelegateCommand UpdateSourceCommand => updateSourceCommand ??= new DelegateCommand(UpdateSource);

        static void UpdateSource()
        {
            Number += 1;
            Message = $"This is a Message,{DateTime.Now:HH:mm:ss.fff}";
            if (Items.Count>10)
            {
                Items.Clear();
            }

            Items.Add(new Item
            {
                Money = DateTime.Now.Millisecond,
                Name = Guid.NewGuid().ToString()[..6].ToUpper()
            });

        }

        public const string Hello = "HelloWorld";

        public static Guid Id { get; } = Guid.NewGuid();

        #endregion

        #region StaticMvvm
        //public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        public static event PropertyChangedEventHandler StaticPropertyChanged;
        public static bool SetStaticProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            RaiseStaticPropertyChanged(propertyName);
            return true;
        }

        public static bool SetStaticProperty<T>(ref T storage, T value, Action onChanged, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            onChanged?.Invoke();
            RaiseStaticPropertyChanged(propertyName);
            return true;
        }
        public static void RaiseStaticPropertyChanged([CallerMemberName] string propertyName = null) => OnStaticPropertyChanged(new PropertyChangedEventArgs(propertyName));

        public static void OnStaticPropertyChanged(PropertyChangedEventArgs args) => StaticPropertyChanged?.Invoke(null, args);
        #endregion

    }
}
