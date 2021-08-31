using SFIDWebAPI.Application.Models.Query;
using CsvHelper.Configuration;
using System.Globalization;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.ExportBulletin
{
    public class ExportBulletinDto : BaseDto
    {
        public ExportBulletinData Data { set; get; }
    }

    public class ExportBulletinData
    {
        public string Link { set; get; }
    }

    public class ExportData
    {
    #nullable enable
        public int? No { set; get; }
        public int? Id { set; get; }
        public string? FileCode { set; get; }
        public string? Content { set; get; }
        public string? Title { set; get; }
        public string? MaterialType { set; get; }
        public int? TotalView { set; get; }
        public int? TotalUserVisited { set; get; }
    }

    public sealed class ExportMap : ClassMap<ExportData>
    {
        public ExportMap()
        {
            AutoMap(CultureInfo.CurrentCulture);
            Map(m => m.Id).Ignore();
        }
    }
}