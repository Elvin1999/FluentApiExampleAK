using FluentApiExample.Commands;
using FluentApiExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FluentApiExample.Domain.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
		public void ClearForm()
		{
			Customer = new Customer();
		}

		private Customer customer;

		public Customer Customer
        {
			get { return customer; }
			set { customer = value; OnPropertyChanged(); }
		}

		private Order selectedOrder;

		public Order SelectedOrder
		{
			get { return selectedOrder; }
			set { selectedOrder = value; OnPropertyChanged(); }
		}


		private ObservableCollection<Customer> allCustomers;

		public ObservableCollection<Customer> AllCustomers
		{
			get { return allCustomers; }
			set { allCustomers = value; OnPropertyChanged(); }
		}

		private ObservableCollection<Order> allOrders;

		public ObservableCollection<Order> AllOrders
		{
			get { return allOrders; }
			set { allOrders = value; OnPropertyChanged(); }
		}

		public RelayCommand AddCommand { get; private set; }
		public RelayCommand DeleteCustomerCommand { get; private set; }
		public RelayCommand UpdateCommand { get; private set; }
		public RelayCommand OrderNowCommand { get; private set; }
		public RelayCommand ResetCommand { get; set; }

		public MainViewModel()
		{
			Customer = new Customer();

			AllCustomers = new ObservableCollection<Customer>(App.DB.CustomerRepository.GetAll());

			AllOrders = new ObservableCollection<Order>(App.DB.OrderRepository.GetAll());

			ResetCommand = new RelayCommand((o) =>
			{
				ClearForm();
			});

			AddCommand = new RelayCommand((o) =>
			{
				if (Customer.Id >= 1)
				{
					var item = App.DB.CustomerRepository
					.GetData(Customer.Id);
					if (item != null)
					{
						MessageBox.Show("Customer is already exists");
					}
					else
					{
						App.DB.CustomerRepository.AddData(Customer);
						AllCustomers = new ObservableCollection<Customer>(App.DB.CustomerRepository.GetAll());
						MessageBox.Show("Customer was added successfully");
						ClearForm();
					}
				}
				else
				{
                    App.DB.CustomerRepository.AddData(Customer);
                    AllCustomers = new ObservableCollection<Customer>(App.DB.CustomerRepository.GetAll());
                    MessageBox.Show("Customer was added successfully");
                    ClearForm();
                }
			});

			DeleteCustomerCommand = new RelayCommand((o) =>
			{
				App.DB.CustomerRepository.DeleteData(Customer);
                AllCustomers = new ObservableCollection<Customer>(App.DB.CustomerRepository.GetAll());
                AllOrders = new ObservableCollection<Order>(App.DB.OrderRepository.GetAll());
            });


			UpdateCommand = new RelayCommand((o) =>
			{
				App.DB.CustomerRepository.UpdateData(Customer);
				MessageBox.Show("Updated Succesfully");
				ClearForm();
			});

			OrderNowCommand = new RelayCommand((o) =>
			{
				var order = new Order
				{
					OrderDate=DateTime.Now,
					CustomerId=Customer.Id
				};

				App.DB.OrderRepository.AddData(order);
				AllOrders = new ObservableCollection<Order>(App.DB.OrderRepository.GetAll());
				ClearForm();
				MessageBox.Show("Order Completed Succesfully");
			},
			(p) =>
			{
				if(Customer!=null && Customer.Id != 0)
				{
					return true;
				}
				return false;
			});
		}


	}
}
