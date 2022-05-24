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
    public partial class AppsComponent : ComponentBase
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
        protected RadzenDataGrid<Intranet.Models.Kkk.App> grid0;
        protected RadzenDataList<Intranet.Models.Kkk.App> dlist0;

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

        IEnumerable<Intranet.Models.Kkk.App> _getAppsResult;
        protected IEnumerable<Intranet.Models.Kkk.App> getAppsResult
        {
            get
            {
                return _getAppsResult;
            }
            set
            {
                if (!object.Equals(_getAppsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getAppsResult", NewValue = value, OldValue = _getAppsResult };
                    _getAppsResult = value;
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

            var kkkGetAppsResult = await Kkk.GetApps(new Query() { Filter = $@"i => i.Name.Contains(@0) || i.Description.Contains(@1) || i.Ulr.Contains(@2) || i.ClassLogo.Contains(@3) || i.Category.Contains(@4)", FilterParameters = new object[] { search, search, search, search, search } });
            getAppsResult = kkkGetAppsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddApp>("Add App", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Kkk.ExportAppsToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "AppId,Name,Description,Ulr,ClassLogo,Category,LastUpdate" }, $"Apps");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Kkk.ExportAppsToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "AppId,Name,Description,Ulr,ClassLogo,Category,LastUpdate" }, $"Apps");

            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<Intranet.Models.Kkk.App> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditApp>("Edit App", new Dictionary<string, object>() { {"AppId", args.Data.AppId} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var kkkDeleteAppResult = await Kkk.DeleteApp(data.AppId);
                    if (kkkDeleteAppResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception kkkDeleteAppException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete App" });
            }
        }
    }
}
