using System;
//
using H2F.TEST.Interface;
namespace H2F.TEST.Implement
{
    public class Student : IPerson
    {
        public string GetNames()
        {
            return "Student";
        }
    }
}
