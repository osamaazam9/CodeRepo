using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;


namespace SO.Utility.Base
{
    public  class BaseDbContext : DbContext
    {

        public BaseDbContext(string connectionString)
            : base(connectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            this.ChangeTracker
                .Entries()
                .Where(e => e.State ==  EntityState.Modified || e.State ==  EntityState.Added)
                .ToList().ForEach(e =>
                {
                    setChangeDateTimes(e, e.State);
                });

            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                string errors = "";
                foreach (var entityErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in entityErrors.ValidationErrors)
                    {
                        errors += String.Format("- Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception("Entity Validation Errors: " + errors);
            }
        }

        public void setChangeDateTimes(DbEntityEntry obj, EntityState state)
        {
    
            Type curType = obj.Entity.GetType();

            var created = curType.GetProperty("created");
            object createdValue = null;
            if(created!=null)
                 createdValue = created.GetValue(obj.Entity, null);

            if (created != null && (state == EntityState.Added || createdValue == null || (DateTime)createdValue==DateTime.MinValue))
            {
                    created.SetValue(obj.Entity, DateTime.Now, null);
            }
            var modified = curType.GetProperty("modified");
            if (modified != null)
                modified.SetValue(obj.Entity, DateTime.Now, null);
           
        }
    }
}
