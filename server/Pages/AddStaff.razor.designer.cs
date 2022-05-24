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
    public partial class AddStaffComponent : ComponentBase
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
            staff = new Intranet.Models.Kkk.Staff(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Intranet.Models.Kkk.Staff args)
        {
            try
            {
                var kkkCreateStaffResult = await Kkk.CreateStaff(staff);
                DialogService.Close(staff);
            }
            catch (System.Exception kkkCreateStaffException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Staff!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
