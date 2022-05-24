using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Intranet.Models.Kkk;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Pages
{
    public partial class DepartmentsComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected KkkService Kkk { get; set; }
        protected RadzenDataGrid<Intranet.Models.Kkk.Department> grid0;

        string _search;
        protected string search
        {
            get
            {
                return _search;
            }
            set
            {
                if (!object.Equals(_search, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "search", NewValue = value, OldValue = _search };
                    _search = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Intranet.Models.Kkk.Department> _getDepartmentsResult;
        protected IEnumerable<Intranet.Models.Kkk.Department> getDepartmentsResult
        {
            get
            {
                return _getDepartmentsResult;
            }
            set
            {
                if (!object.Equals(_getDepartmentsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getDepartmentsResult", NewValue = value, OldValue = _getDepartmentsResult };
                    _getDepartmentsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            if (string.IsNullOrEmpty(search)) {
                search = "";
            }

            var kkkGetDepartmentsResult = await Kkk.GetDepartments(new Query() { Filter = $@"i => i.Description.Contains(@0) || i.Supervisor.Contains(@1) || i.Extension1.Contains(@2) || i.Extension2.Contains(@3) || i.Extension3.Contains(@4) || i.Extension4.Contains(@5) || i.QtdStaff.Contains(@6)", FilterParameters = new object[] { search, search, search, search, search, search, search } });
            getDepartmentsResult = kkkGetDepartmentsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddDepartment>("Add Department", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Kkk.ExportDepartmentsToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Department1.Description as Department1Description,Description,Supervisor,Extension1,Extension2,Extension3,Extension4,QtdStaff,StaffId,LastUpdate" }, $"Departments");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Kkk.ExportDepartmentsToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Department1.Description as Department1Description,Description,Supervisor,Extension1,Extension2,Extension3,Extension4,QtdStaff,StaffId,LastUpdate" }, $"Departments");

            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<Intranet.Models.Kkk.Department> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditDepartment>("Edit Department", new Dictionary<string, object>() { {"DeptoId", args.Data.DeptoId} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var kkkDeleteDepartmentResult = await Kkk.DeleteDepartment(data.DeptoId);
                    if (kkkDeleteDepartmentResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception kkkDeleteDepartmentException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Department" });
            }
        }
    }
}
