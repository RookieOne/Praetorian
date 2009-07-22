using System;
using System.Collections.Generic;
using System.Windows.Input;
using ExpenseShared.ReadDtos;
using ExpenseShared.Repositories;
using Microsoft.Practices.Unity;
using PraetorianWpf.Commands;

namespace PraetorianWpf.ViewModels
{
    public class ExpensesViewModel : ViewModel
    {
        private readonly IUnityContainer _container;
        private IEnumerable<ExpenseDto> _expenses;

        public IEnumerable<ExpenseDto> Expenses
        {
            get { return _expenses; }
            set
            {
                _expenses = value;
                OnPropertyChanged("Expenses");
            }
        }

        public ICommand CreateExpenseCommand { get; set; }

        public ExpensesViewModel(IUnityContainer container)
        {
            _container = container;
            CreateExpenseCommand = new ActionCommand(OnCreateExpense);

            GetExpenses();
        }

        private void OnCreateExpense()
        {
            
        }


        private void GetExpenses()
        {
            Expenses = _container.Resolve<ExpenseReadRepository>().GetAllExpenses();
        }
    }
}