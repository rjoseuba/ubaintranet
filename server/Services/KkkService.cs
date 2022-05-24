using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using Intranet.Data;

namespace Intranet
{
    public partial class KkkService
    {
        KkkContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly KkkContext context;
        private readonly NavigationManager navigationManager;

        public KkkService(KkkContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportAppsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/kkk/apps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/kkk/apps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAppsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/kkk/apps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/kkk/apps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAppsRead(ref IQueryable<Models.Kkk.App> items);

        public async Task<IQueryable<Models.Kkk.App>> GetApps(Query query = null)
        {
            var items = Context.Apps.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnAppsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAppCreated(Models.Kkk.App item);
        partial void OnAfterAppCreated(Models.Kkk.App item);

        public async Task<Models.Kkk.App> CreateApp(Models.Kkk.App app)
        {
            OnAppCreated(app);

            var existingItem = Context.Apps
                              .Where(i => i.AppId == app.AppId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Apps.Add(app);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(app).State = EntityState.Detached;
                throw;
            }

            OnAfterAppCreated(app);

            return app;
        }
        public async Task ExportDepartmentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/kkk/departments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/kkk/departments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDepartmentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/kkk/departments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/kkk/departments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDepartmentsRead(ref IQueryable<Models.Kkk.Department> items);

        public async Task<IQueryable<Models.Kkk.Department>> GetDepartments(Query query = null)
        {
            var items = Context.Departments.AsQueryable();

            items = items.Include(i => i.Department1);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnDepartmentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDepartmentCreated(Models.Kkk.Department item);
        partial void OnAfterDepartmentCreated(Models.Kkk.Department item);

        public async Task<Models.Kkk.Department> CreateDepartment(Models.Kkk.Department department)
        {
            OnDepartmentCreated(department);

            var existingItem = Context.Departments
                              .Where(i => i.DeptoId == department.DeptoId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Departments.Add(department);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(department).State = EntityState.Detached;
                department.Department1 = null;
                throw;
            }

            OnAfterDepartmentCreated(department);

            return department;
        }
        public async Task ExportStaffsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/kkk/staffs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/kkk/staffs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportStaffsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/kkk/staffs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/kkk/staffs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnStaffsRead(ref IQueryable<Models.Kkk.Staff> items);

        public async Task<IQueryable<Models.Kkk.Staff>> GetStaffs(Query query = null)
        {
            var items = Context.Staffs.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnStaffsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnStaffCreated(Models.Kkk.Staff item);
        partial void OnAfterStaffCreated(Models.Kkk.Staff item);

        public async Task<Models.Kkk.Staff> CreateStaff(Models.Kkk.Staff staff)
        {
            OnStaffCreated(staff);

            var existingItem = Context.Staffs
                              .Where(i => i.StaffId == staff.StaffId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Staffs.Add(staff);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(staff).State = EntityState.Detached;
                throw;
            }

            OnAfterStaffCreated(staff);

            return staff;
        }

        partial void OnAppDeleted(Models.Kkk.App item);
        partial void OnAfterAppDeleted(Models.Kkk.App item);

        public async Task<Models.Kkk.App> DeleteApp(int? appId)
        {
            var itemToDelete = Context.Apps
                              .Where(i => i.AppId == appId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAppDeleted(itemToDelete);

            Context.Apps.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAppDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnAppGet(Models.Kkk.App item);

        public async Task<Models.Kkk.App> GetAppByAppId(int? appId)
        {
            var items = Context.Apps
                              .AsNoTracking()
                              .Where(i => i.AppId == appId);

            var itemToReturn = items.FirstOrDefault();

            OnAppGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Kkk.App> CancelAppChanges(Models.Kkk.App item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAppUpdated(Models.Kkk.App item);
        partial void OnAfterAppUpdated(Models.Kkk.App item);

        public async Task<Models.Kkk.App> UpdateApp(int? appId, Models.Kkk.App app)
        {
            OnAppUpdated(app);

            var itemToUpdate = Context.Apps
                              .Where(i => i.AppId == appId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(app);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterAppUpdated(app);

            return app;
        }

        partial void OnDepartmentDeleted(Models.Kkk.Department item);
        partial void OnAfterDepartmentDeleted(Models.Kkk.Department item);

        public async Task<Models.Kkk.Department> DeleteDepartment(int? deptoId)
        {
            var itemToDelete = Context.Departments
                              .Where(i => i.DeptoId == deptoId)
                              .Include(i => i.Departments1)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDepartmentDeleted(itemToDelete);

            Context.Departments.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDepartmentDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnDepartmentGet(Models.Kkk.Department item);

        public async Task<Models.Kkk.Department> GetDepartmentByDeptoId(int? deptoId)
        {
            var items = Context.Departments
                              .AsNoTracking()
                              .Where(i => i.DeptoId == deptoId);

            items = items.Include(i => i.Department1);

            var itemToReturn = items.FirstOrDefault();

            OnDepartmentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Kkk.Department> CancelDepartmentChanges(Models.Kkk.Department item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDepartmentUpdated(Models.Kkk.Department item);
        partial void OnAfterDepartmentUpdated(Models.Kkk.Department item);

        public async Task<Models.Kkk.Department> UpdateDepartment(int? deptoId, Models.Kkk.Department department)
        {
            OnDepartmentUpdated(department);

            var itemToUpdate = Context.Departments
                              .Where(i => i.DeptoId == deptoId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(department);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterDepartmentUpdated(department);

            return department;
        }

        partial void OnStaffDeleted(Models.Kkk.Staff item);
        partial void OnAfterStaffDeleted(Models.Kkk.Staff item);

        public async Task<Models.Kkk.Staff> DeleteStaff(int? staffId)
        {
            var itemToDelete = Context.Staffs
                              .Where(i => i.StaffId == staffId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnStaffDeleted(itemToDelete);

            Context.Staffs.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterStaffDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnStaffGet(Models.Kkk.Staff item);

        public async Task<Models.Kkk.Staff> GetStaffByStaffId(int? staffId)
        {
            var items = Context.Staffs
                              .AsNoTracking()
                              .Where(i => i.StaffId == staffId);

            var itemToReturn = items.FirstOrDefault();

            OnStaffGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Kkk.Staff> CancelStaffChanges(Models.Kkk.Staff item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnStaffUpdated(Models.Kkk.Staff item);
        partial void OnAfterStaffUpdated(Models.Kkk.Staff item);

        public async Task<Models.Kkk.Staff> UpdateStaff(int? staffId, Models.Kkk.Staff staff)
        {
            OnStaffUpdated(staff);

            var itemToUpdate = Context.Staffs
                              .Where(i => i.StaffId == staffId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(staff);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterStaffUpdated(staff);

            return staff;
        }
    }
}
