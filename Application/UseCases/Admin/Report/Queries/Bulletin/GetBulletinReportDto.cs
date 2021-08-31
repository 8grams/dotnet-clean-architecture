using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Bulletin
{
    public class GetBulletinReportDto : BaseDto
    {
        public BulletinReportData Data { set; get; }
    }

    public class BulletinReportData
    {
        public TopMenuData TopMenu { set; get; }
        public ChartData Charts { set; get; }
        public TopVisit TopVisit { set; get; }
    }

    public class TopMenuData
    {
        public IList<TopMenuDataItem> Items { set; get; }
    }

    public class TopMenuDataItem
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Key { set; get; }
        public string Value { set; get; }
        public string Icon { set; get; }
        public string Color { set; get; }
    }

    public class ChartData
    {
        public IList<ChartDataItem> Items { set; get; }
    }

    public class ChartDataItem
    {
        public string Period { set; get; }
        public int Total { set; get; }
    }

    public class TopVisit
    {
        public IList<TopVisitItem> Items { set; get; }
    }

    public class TopVisitItem
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public int TotalUser { set; get; }
        public int TotalVisit { set; get; }
    }
}
