namespace MiniORM
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class DbSet<TEntity> : ICollection<TEntity>
        where TEntity : class, new()
    {
        public DbSet(IList<TEntity> entities)
        {
            this.Entities = entities.ToList();

            this.ChageTracker = new ChangeTracker<TEntity>(entities);
        }

        internal ChangeTracker<TEntity> ChageTracker { get; set; }

        internal IList<TEntity> Entities { get; set; }

        public void Add(TEntity item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null!");
            }

            this.Entities.Add(item);

            this.ChageTracker.Add(item);
        }

        public void Clear()
        {
            while (this.Entities.Any())
            {
                var entity = this.Entities.First();
                this.Remove(entity);
            }
        }

        public bool Contains(TEntity item)
            => this.Entities.Contains(item);

        public void CopyTo(TEntity[] array, int arrayIndex)
            => this.Entities.CopyTo(array, arrayIndex);

        public int Count
            => this.Entities.Count();

        public bool IsReadOnly
            => this.Entities.IsReadOnly;

        public bool Remove(TEntity item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "item cannot be null!");
            }

            var removedSuccessfully = this.Entities.Remove(item);

            if (removedSuccessfully)
            {
                this.ChageTracker.Remove(item);
            }

            return removedSuccessfully;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities.ToArray())
            {
                this.Remove(entity);
            }
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}