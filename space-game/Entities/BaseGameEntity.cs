using Messaging;
using System;

namespace Entities
{
    /// <summary>
    /// Base class for all game objects
    /// </summary>
    public abstract class BaseGameEntity
    {
        // every entity has a unique identifier
        private readonly Guid _id;

        protected readonly IEntityManager _entityManager;
        public Guid Id => _id;

        protected BaseGameEntity(IEntityManager entityManager)
        {
            if (entityManager == null)
            {
                throw new ArgumentNullException(nameof(entityManager));
            }

            _id = Guid.NewGuid();
            _entityManager = entityManager;
            _entityManager.RegisterEntity(this);
        }

        /// <summary>
        /// All entities must implement an update function
        /// </summary>
        /// <remarks></remarks>
        public abstract void Update();

        /// <summary>
        /// All subclasses can communicate using messages
        /// </summary>
        public virtual bool HandleMessage(Telegram msg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Type of the entity
        /// </summary>
        public string Type => this.GetType().ToString();

        /// <summary>
        /// Whether the entity can be used as a camera target
        /// </summary>
        public virtual bool CameraTarget { get; set; } = true;
    }
}
