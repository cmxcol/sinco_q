using System;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Management.Instrumentation;
using Microsoft.Practices.Unity;
using Persistence.EntityDataModelObjectContext.ObjectContextSCAdm;

namespace Persistence.DAO.Generic
{
    public class GenericDAO<E, PK> : IGenericDAO<E, PK> where E : IEntityWithKey
    {
        private Type eClass;
        private ObjectContext ctx;
        private String eContainerName;

        public GenericDAO()
        {
            this.eClass = typeof(E);
            //ctx = ObjCtxSCAdmIns.Instance.SCAdmEntity();            
        }
        //[Dependency]
        public ObjectContext Context
        {
            set
            {
                ctx = value;
                eContainerName = ctx.DefaultContainerName;
                //eContainerName = (ctx.MetadataWorkspace.GetItems<EntityContainer>(DataSpace.CSpace))[0].Name;
                ctx.MetadataWorkspace.LoadFromAssembly(eClass.Assembly);
            }
            get { return ctx; }
        }
        public EntityKey CreateEntityKey(PK id)
        {
            EntityType eType = (EntityType)ctx.MetadataWorkspace.GetType(eClass.Name, eClass.Namespace, DataSpace.CSpace);
            String primaryKeyFieldName = ((EntityType)eType).KeyMembers.First().ToString();
            EntityKey eKey = new EntityKey(eContainerName + "." + eClass.Name, new EntityKeyMember[] { new EntityKeyMember(primaryKeyFieldName, id) });
            return eKey;
        }
        public void Save(E entity)
        {
            String entitySetName = eContainerName + "." + eClass.Name;
            ctx.AddObject(entitySetName, entity);
            ctx.SaveChanges();
            ctx.Refresh(RefreshMode.StoreWins, entity);
            ctx.AcceptAllChanges();
        }
        /// <exception cref="InstanceNotFoundException"/>
        public E Load(PK id)
        {
            EntityKey entityKey = this.CreateEntityKey(id);
            try
            {
                Context.Refresh(RefreshMode.StoreWins, ctx.GetObjectByKey(entityKey));
                E result = (E)ctx.GetObjectByKey(entityKey);                
                return result;
            }
            catch (ObjectNotFoundException e)
            {
                //throw new InstanceNotFoundException(id, eClass.FullName);
                throw new InstanceNotFoundException(e.ToString(), e.InnerException);
            }
        }
        public Boolean Exists(PK id)
        {
            Boolean objectFound = true;
            EntityKey entityKey = this.CreateEntityKey(id);
            try
            {
                ctx.GetObjectByKey(entityKey);
            }
            catch (ObjectNotFoundException)
            {
                objectFound = false;
            }
            return objectFound;
        }
        public E Update(E entity)
        {
            // Last Updates are sent to database
            ctx.Refresh(RefreshMode.ClientWins, entity);
            ctx.SaveChanges();
            ctx.AcceptAllChanges();
            return (E)ctx.GetObjectByKey(entity.EntityKey);
        }
        /// <exception cref="InstanceNotFoundException"/>
        public void Remove(PK id)
        {
            E objectToRemove = default(E);
            try
            {
                // First we need to find the object
                objectToRemove = Load(id);
                ctx.DeleteObject(objectToRemove);
                ctx.SaveChanges();
                ctx.AcceptAllChanges();
            }
            catch (InstanceNotFoundException)
            {
                throw;
            }
            catch (OptimisticConcurrencyException)
            {
                ctx.Refresh(RefreshMode.ClientWins, objectToRemove);
                ctx.DeleteObject(objectToRemove);
                ctx.SaveChanges();
                ctx.AcceptAllChanges();
            }
            catch (InvalidOperationException e)
            {
                //throw new InstanceNotFoundException(id, entityClass.FullName);
                throw new InstanceNotFoundException(e.ToString(), e.InnerException);
            }
        }
    }
}
