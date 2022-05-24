using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Intranet.Data;

namespace Intranet
{
    public partial class ExportKkkController : ExportController
    {
        private readonly KkkContext context;
        private readonly KkkService service;
        public ExportKkkController(KkkContext context, KkkService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/Kkk/apps/csv")]
        [HttpGet("/export/Kkk/apps/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAppsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetApps(), Request.Query), fileName);
        }

        [HttpGet("/export/Kkk/apps/excel")]
        [HttpGet("/export/Kkk/apps/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportAppsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetApps(), Request.Query), fileName);
        }
        [HttpGet("/export/Kkk/departments/csv")]
        [HttpGet("/export/Kkk/departments/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportDepartmentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDepartments(), Request.Query), fileName);
        }

        [HttpGet("/export/Kkk/departments/excel")]
        [HttpGet("/export/Kkk/departments/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportDepartmentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDepartments(), Request.Query), fileName);
        }
        [HttpGet("/export/Kkk/staffs/csv")]
        [HttpGet("/export/Kkk/staffs/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportStaffsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetStaffs(), Request.Query), fileName);
        }

        [HttpGet("/export/Kkk/staffs/excel")]
        [HttpGet("/export/Kkk/staffs/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportStaffsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetStaffs(), Request.Query), fileName);
        }
    }
}
