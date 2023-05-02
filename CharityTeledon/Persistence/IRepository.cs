using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharityTeledon.model;

namespace CharityTeledon.repository
{
    public interface IRepository<ID, E> where E : Entity<ID>
    {
        E FindOne(ID id);
        IEnumerable<E> GetAll();
        E Add(E entity);
        E Delete(ID id);
        E Update(E entity);
    }
}
