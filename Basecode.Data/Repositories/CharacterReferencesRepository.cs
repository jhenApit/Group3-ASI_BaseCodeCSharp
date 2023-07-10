using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;

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
            _context.CharacterReferences.Add(characterReferences);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = _context.CharacterReferences.Find(id);
            if (data != null)
            {
                _context.CharacterReferences.Remove(data);
                _context.SaveChanges();
            }
        }

        public CharacterReferences GetByName(string name)
        {
            return _context.CharacterReferences.FirstOrDefault(e => e.Name == name);
        }

        public CharacterReferences GetById(int id)
        {
            return _context.CharacterReferences.FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<CharacterReferences> RetrieveAll()
        {
            return this.GetDbSet<CharacterReferences>();
        }

        public void Update(CharacterReferences characterReferences)
        {
            _context.Entry(characterReferences).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
