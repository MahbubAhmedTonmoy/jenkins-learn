using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var parent = new Parent();
            parent.FunParent();
            var chield = new Child();
            chield.FunChild();

            Parent replace = new Child();
            replace.FunParent();

            Child chield1 = new Parent();
            //var result = await isFireToday();
            //if (!result)
            //{
            //    Console.WriteLine("not run");
            //}
            //else
            //{
            //    Console.WriteLine(" run");
            //}
        }
        private static async Task<bool> isFireToday()
        {
            var url = @"http://datetimeextensions.azurewebsites.net/Api/HolidayObservances/de-DE/2021?language=de-DE";
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                using (HttpContent httpContent = response.Content)
                {
                    var data = await httpContent.ReadAsStringAsync();
                    var rootObject = JsonConvert.DeserializeObject<List<Data>>(data);
                    var a = rootObject;
                    //a.Add(new Data { Name = "today", ObservanceDate = DateTime.Now.Date });
                    DateTime today = DateTime.Today;
                    if (a.Any(x => x.ObservanceDate == today) || today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
                    {
                        return false;
                    }
                }
            }
            else
            {
                Console.WriteLine($" StatusCode: {response.StatusCode}");
            }
            return true;
        }
    }

    public class Parent
    {
        //public int a { get; set; }
        //public Parent(int a)
        //{
        //    this.a = a;
        //}
        public void FunParent()
        {
            Console.WriteLine("Parent");
        }
    }
    public class Child : Parent
    {
        //public Child(int b): base(b)
        //{
        //}
        public void FunChild()
        {
            Console.WriteLine("Child");
        }
    }
    public class Data
    {
        public string Name { get; set; }
        public DateTime ObservanceDate { get; set; }
    }
}
