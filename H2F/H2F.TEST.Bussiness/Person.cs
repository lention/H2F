using System;
//
using H2F.TEST.Interface;
namespace H2F.TEST.Bussiness
{
    public class Person
    {
        private IPerson person;
        public Person(IPerson p)
        {
            person = p;
        }

        public string GetCurrentNames()
        {
            return person.GetNames();
        }
    }
}
