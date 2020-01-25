using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace class_project.Models.ViewModel
{
    public class PersonViewModel
    {
        public PersonViewModel(Person person)
        {
            PersonID = person.ID;
            PersonName = person.FirstName + " " + person.LastName;
            PersonAge = person.Athletes.Select(a => a.Age).Sum();
        }

        public int PersonID { get; private set; }
        [DisplayName("Name:")]
        public string PersonName { get; private set; }
        [DisplayName("Age:")]
        public int PersonAge { get; private set; }
    }
}