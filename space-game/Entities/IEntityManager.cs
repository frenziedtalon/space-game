using System;
using System.Collections.Generic;

namespace Entities
{
    public interface IEntityManager
    {
        void RegisterEntity(BaseGameEntity newEntity);
        void RemoveEntity(BaseGameEntity pEntity);
        void UpdateAll();
        BaseGameEntity GetEntityFromId(Guid id);
        List<BaseGameEntity> GetAllEntities();
    }
}
