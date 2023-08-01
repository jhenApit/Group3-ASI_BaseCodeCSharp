using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface IAddressRepository
    {
        IQueryable<Addresses> RetrieveAll();
        Addresses GetByCity(string city);
        void Add(Addresses address);
        Addresses GetById(int id);
        Addresses GetByApplicantId(int applicantId);
    }
}
