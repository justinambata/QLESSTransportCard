/*
 * https://stackoverflow.com/questions/2019402/when-why-to-use-delegates
 * By defining a delegate, you are saying to the user of your class,
 * "Please feel free to assign any method that matches this signature to the delegate and it will be called each time my delegate is called".
 */

using System;
using System.Collections.Generic;

namespace QLESS_Transport_Card
{
    /// <summary>
    /// A class to define a person
    /// </summary>
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class DelegateApp
    {
        //Our delegate
        public delegate bool FilterDelegate(Person p);

        public void Run()
        {
            Console.WriteLine("DelegateApp start...");

            //Create 4 Person objects
            var p1 = new Person { Name = "John", Age = 41 };
            var p2 = new Person { Name = "Jane", Age = 69 };
            var p3 = new Person { Name = "Jake", Age = 12 };
            var p4 = new Person { Name = "Jessie", Age = 25 };
            var p5 = new Person { Name = "Gevie", Age = 32 };

            //Create a list of Person objects and fill it
            var people = new List<Person>() { p1, p2, p3, p4, p5 };

            //Invoke DisplayPeople using appropriate delegate
            DisplayPeople("Children:", people, IsChild);
            DisplayPeople("Adults:", people, IsAdult);
            DisplayPeople("Seniors:", people, IsSenior);
            DisplayPeople("Starts with \'J\':", people, StartsWithJ);
            DisplayPeople("Doesn\'t start with \'J\':", people, DoesntStartWithJ);
        }

        /// <summary>
        /// A method to filter out the people you need
        /// </summary>
        /// <param name="people">A list of people</param>
        /// <param name="filter">A filter</param>
        /// <returns>A filtered list</returns>
        static void DisplayPeople(string title, List<Person> people, FilterDelegate filter)
        {
            Console.WriteLine(title);

            foreach (var p in people)
            {
                if (filter(p))
                {
                    Console.WriteLine("{0}, {1} years old", p.Name, p.Age);
                }
            }

            Console.Write("\n\n");
        }

        //==========FILTERS===================
        static bool IsChild(Person p)
        {
            return p.Age < 18;
        }

        static bool IsAdult(Person p)
        {
            return p.Age >= 18;
        }

        static bool IsSenior(Person p)
        {
            return p.Age >= 65;
        }

        static bool StartsWithJ(Person p)
        {
            return p.Name.StartsWith("J", StringComparison.InvariantCultureIgnoreCase);
        }

        static bool DoesntStartWithJ(Person p)
        {
            //return !p.Name.StartsWith("J", StringComparison.InvariantCultureIgnoreCase);
            return !StartsWithJ(p);
        }
    }

    public class EventDelegateApp
    {
        public void Run()
        {
            Console.WriteLine("EventDelegateApp start...");
            var patient = new Patient();
            patient.Death();


        }


        public class Patient
        {
            public delegate void DeathInfo(); //Declaring a Delegate//
            //public event DeathInfo DeathDate; //Declaring the event//
            public event Action DeathDate;
            public void Death()
            {
                Console.WriteLine("Patient.Death() start");
                DeathDate();
                //DeathDate?.Invoke();
                Console.WriteLine("Patient.Death() end");
            }
        }
        public class Insurance
        {
            private readonly Patient _patient = new Patient();
            void GetDeathDetails()
            {
                //-------Do Something with the deathDate event------------//
                Console.WriteLine("Insurance.GetDeathDetails()");
            }
            void Main()
            {
                //--------Subscribe the function GetDeathDetails----------//
                _patient.DeathDate += GetDeathDetails;
            }
        }
        public class Bank
        {
            private readonly Patient _patient = new Patient();
            void GetPatientInfo()
            {
                //-------Do Something with the deathDate event------------//
                Console.WriteLine("Bank.GetPatientInfo()");
            }
            void Main()
            {
                //--------Subscribe the function GetPatInfo ----------//
                _patient.DeathDate += GetPatientInfo;
            }
        }
    }
}