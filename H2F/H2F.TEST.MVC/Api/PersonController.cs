using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//
using H2F.TEST.Interface;
using H2F.Standard.Common.Ioc;
using H2F.TEST.MVC.Common;

namespace H2F.TEST.MVC.Api
{
    public class PersonController : ApiController
    {
        public IPerson person;

        public PersonController(IPerson p)
        {
            person = p;
        }
        // GET api/<controller>
        public string Get()
        {
            return person.GetNames();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return IocContainerManager.ResolveByEnumKey<PersonType,IPerson>(PersonType.Worker).GetNames();
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}