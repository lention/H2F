using System;
using System.Collections.Generic;
using System.Text;
//
using H2F.TEST.Interface;
namespace H2F.TEST.Implement
{
    public class Worker : IPerson
    {
        public string GetNames()
        {
            return "worker";
        }
    }
}
