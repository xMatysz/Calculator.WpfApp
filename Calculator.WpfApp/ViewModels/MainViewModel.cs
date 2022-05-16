using Calculator.WpfApp.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Calculator.WpfApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _screenVal;
        private List<string> _avaibleOperations = new List<string> { "+", "-", "*", "/"};
        private DataTable _dataTable = new DataTable();
        private bool _isLastSignAnOperation = false;
        public MainViewModel()
        {
            ScreenVal = "0";
            AddNumberCommand = new RelayCommand(AddNumber);
            AddOperationCommand = new RelayCommand(AddOperation, CanUseOperation);
            ClearScreenCommand = new RelayCommand(ClearScreen);
            GetResultCommand = new RelayCommand(GetResult, CanUseOperation);

        }

        private bool CanUseOperation(object obj) => !_isLastSignAnOperation;
        

        private void AddNumber(object obj)
        {
            var number = obj as string;

            if (ScreenVal == "0" && number != ",")
                ScreenVal = String.Empty;
            else if (number == "," && _avaibleOperations.Contains(ScreenVal.Substring(ScreenVal.Length - 1)))
                number = "0,";

            ScreenVal += number;

            _isLastSignAnOperation = false;
        }
        private void AddOperation(object obj)
        {
            var operation = obj as string;

            ScreenVal += operation;

            _isLastSignAnOperation = true;
        }
        private void ClearScreen(object obj)
        {
            ScreenVal = "0";

            _isLastSignAnOperation = false;
        }
        private void GetResult(object obj)
        {
            var result = Math.Round(Convert.ToDouble(_dataTable.Compute(ScreenVal.Replace(",", "."), "")), 2);

            ScreenVal = result.ToString();
        }

        public string ScreenVal
        {
            get 
            { 
                return _screenVal; 
            }
            set 
            { 
                _screenVal = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddNumberCommand { get; set; }
        public ICommand AddOperationCommand { get; set; }
        public ICommand ClearScreenCommand { get; set; }
        public ICommand GetResultCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
