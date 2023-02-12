using FluentApiExample.DataAccess.EFrameworkServer;
using FluentApiExample.Domain.Abstractions;
using FluentApiExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FluentApiExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IUnitOfWork DB;
        public App()
        {
            DB = new EFUnitOfWork();

            using (var context=new MyContext())
            {
                try
                {
                    context.Database.CreateIfNotExists();
                }
                catch (Exception)
                {
                }

                if (DB.CustomerRepository.GetAll().Count == 0)
                {
                    var c1 = new Customer
                    {
                        City = "Baku",
                        CompanyName = "Step IT MMC",
                        ContactName = "12345",
                        Country = "Azerbaijan"
                    };

                    var c2 = new Customer
                    {
                        City = "Silicon Valley",
                        CompanyName = "Elvin MMC",
                        ContactName = "76545112",
                        Country = "USA"
                    };

                    DB.CustomerRepository.AddData(c1);
                    DB.CustomerRepository.AddData(c2);
                }


                if (DB.OrderRepository.GetAll().Count == 0)
                {
                    var o1 = new Order
                    {
                        CustomerId = 1,
                        OrderDate = DateTime.Now.AddDays(-3),
                        ImagePath = "https://cdn.vox-cdn.com/thumbor/xVihezO-_BS4Nw_3HegZv9GH2j0=/0x0:2040x1360/1400x1400/filters:focal(1020x680:1021x681)/cdn.vox-cdn.com/uploads/chorus_asset/file/19206400/akrales_190914_3666_0245.jpg"
                    };

                    var o2 = new Order
                    {
                        CustomerId = 1,
                        OrderDate = DateTime.Now.AddDays(-5),
                        ImagePath = "https://images.squarespace-cdn.com/content/v1/5446f93de4b0a3452dfaf5b0/1632243919767-JKQZ2NMF2ZT2L5UC6PZJ/iPhone+13+Pro+%28Above+Avalon%29"
                    };

                    var o3 = new Order
                    {
                        CustomerId = 2,
                        OrderDate = DateTime.Now.AddDays(-10),
                        ImagePath = "https://www.digitaltrends.com/wp-content/uploads/2022/05/galaxy-s22-angled-shot.jpg?p=1"
                    };

                    var o4 = new Order
                    {
                        CustomerId = 2,
                        OrderDate = DateTime.Now.AddDays(-12),
                        ImagePath = "https://m.media-amazon.com/images/I/71HUnJvHsbL._SL1500_.jpg"
                    };

                    DB.OrderRepository.AddData(o1);
                    DB.OrderRepository.AddData(o2);
                    DB.OrderRepository.AddData(o3);
                    DB.OrderRepository.AddData(o4);
                }
            }
        }
    }
}
