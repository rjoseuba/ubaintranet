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
    public partial class StaffsComponent : ComponentBase
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
        protected RadzenDataGrid<Intranet.Models.Kkk.Staff> grid0;

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

        IEnumerable<Intranet.Models.Kkk.Staff> _getStaffsResult;
        protected IEnumerable<Intranet.Models.Kkk.Staff> getStaffsResult
        {
            get
            {
                return _getStaffsResult;
            }
            set
            {
                if (!object.Equals(_getStaffsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getStaffsResult", NewValue = value, OldValue = _getStaffsResult };
                    _getStaffsResult = value;
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

            var kkkGetStaffsResult = await Kkk.GetStaffs(new Query() { Filter = $@"i => i.UnitCode.Contains(@0) || i.Country.Contains(@1) || i.Surname.Contains(@2) || i.FirstName.Contains(@3) || i.MiddleName.Contains(@4) || i.Email.Contains(@5) || i.JobGrade.Contains(@6) || i.GradeClassification.Contains(@7) || i.JobRole.Contains(@8) || i.SalesNonSales.Contains(@9) || i.Department.Contains(@10) || i.LocationBranch.Contains(@11) || i.CountryWork.Contains(@12) || i.CountryOrigin.Contains(@13) || i.EmploymentDate.Contains(@14) || i.LengthStay.Contains(@15) || i.SolId.Contains(@16) || i.Mobile1.Contains(@17) || i.Mobile2.Contains(@18) || i.Extension.Contains(@19)", FilterParameters = new object[] { search, search, search, search, search, search, search, search, search, search, search, search, search, search, search, search, search, search, search, search } });
            getStaffsResult = kkkGetStaffsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddStaff>("Add Staff", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Kkk.ExportStaffsToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "StaffId,UnitCode,Country,Surname,FirstName,MiddleName,Email,DateBirth,CurrentYear,Age,JobGrade,GradeClassification,JobRole,SalesNonSales,Department,LocationBranch,CountryWork,CountryOrigin,EmploymentDate,DateLastPromotion,LengthStay,SolId,Mobile1,Mobile2,Extension,StartDate" }, $"Staffs");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Kkk.ExportStaffsToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "StaffId,UnitCode,Country,Surname,FirstName,MiddleName,Email,DateBirth,CurrentYear,Age,JobGrade,GradeClassification,JobRole,SalesNonSales,Department,LocationBranch,CountryWork,CountryOrigin,EmploymentDate,DateLastPromotion,LengthStay,SolId,Mobile1,Mobile2,Extension,StartDate" }, $"Staffs");

            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<Intranet.Models.Kkk.Staff> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditStaff>("Edit Staff", new Dictionary<string, object>() { {"StaffId", args.Data.StaffId} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var kkkDeleteStaffResult = await Kkk.DeleteStaff(data.StaffId);
                    if (kkkDeleteStaffResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception kkkDeleteStaffException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Staff" });
            }
        }
    }
}
