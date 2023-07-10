using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;

namespace Basecode.Data.Repositories
{
    public class CharacterReferencesRepository : BaseRepository, ICharacterReferencesRepository
    {
        private readonly BasecodeContext _context;
        public CharacterReferencesRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }
        public void Add(CharacterReferences characterReferences)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public CharacterReferences GetById(int id)
        {
            throw new NotImplementedException();
        }

        public CharacterReferences GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CharacterReferences> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public void Update(CharacterReferences characterReferences)
        {
            throw new NotImplementedException();
        }
    }
}
