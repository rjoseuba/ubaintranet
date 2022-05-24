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
    public partial class EditStaffComponent : ComponentBase
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

        [Parameter]
        public dynamic StaffId { get; set; }

        bool _hasChanges;
        protected bool hasChanges
        {
            get
            {
                return _hasChanges;
            }
            set
            {
                if (!object.Equals(_hasChanges, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "hasChanges", NewValue = value, OldValue = _hasChanges };
                    _hasChanges = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        bool _canEdit;
        protected bool canEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if (!object.Equals(_canEdit, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "canEdit", NewValue = value, OldValue = _canEdit };
                    _canEdit = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        Intranet.Models.Kkk.Staff _staff;
        protected Intranet.Models.Kkk.Staff staff
        {
            get
            {
                return _staff;
            }
            set
            {
                if (!object.Equals(_staff, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "staff", NewValue = value, OldValue = _staff };
                    _staff = value;
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
            hasChanges = false;

            canEdit = true;

            var kkkGetStaffByStaffIdResult = await Kkk.GetStaffByStaffId(StaffId);
            staff = kkkGetStaffByStaffIdResult;
        }

        protected async System.Threading.Tasks.Task CloseButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            Kkk.Reset();

            await this.Load();
        }

        protected async System.Threading.Tasks.Task Form0Submit(Intranet.Models.Kkk.Staff args)
        {
            try
            {
                var kkkUpdateStaffResult = await Kkk.UpdateStaff(StaffId, staff);
                DialogService.Close(staff);
            }
            catch (System.Exception kkkUpdateStaffException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Staff" });

            hasChanges = kkkUpdateStaffException is DbUpdateConcurrencyException;

            if (!(kkkUpdateStaffException is DbUpdateConcurrencyException)) {
                canEdit = false;
            }
            }
        }

        protected async System.Threading.Tasks.Task Button4Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
