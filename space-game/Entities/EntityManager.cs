using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Entities.Tests")]
namespace Entities
{
    /// <summary>
    /// Tracks all entities in the game
    /// </summary>
    public class EntityManager : IEntityManager
    {
        private readonly Dictionary<Guid, BaseGameEntity> _entityMap = new Dictionary<Guid, BaseGameEntity>();
        public void RegisterEntity(BaseGameEntity newEntity)
        {
            if (newEntity != null)
            {
                _entityMap.Add(newEntity.Id, newEntity);
            }
        }

        public BaseGameEntity GetEntityFromId(Guid id)
        {
            return _entityMap.ContainsKey(id) ? _entityMap[id] : null;
        }

        public void RemoveEntity(BaseGameEntity pEntity)
        {
            if (pEntity != null && _entityMap.ContainsKey(pEntity.Id))
            {
                _entityMap.Remove(pEntity.Id);
            }
        }

        public void UpdateAll()
        {
            if (_entityMap.Count > 0)
            {
                foreach (var e in _entityMap)
                {
                    e.Value.Update();
                }
            }
        }

        public int Count => _entityMap.Count;

        public List<BaseGameEntity> GetAllEntities()
        {
            return _entityMap.Count > 0 ? _entityMap.Values.ToList() : new List<BaseGameEntity>();
        }
    }
}
