using System;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Users
{
    public class GetUsersReportDto : BaseDto
    {
        public GetUsersReportData Data { set; get; }
    }

    public class GetUsersReportData
    {
        public TopMenuData TopMenu { set; get; }
        public ChartData Charts { set; get; }
        public UserActiveData UserActive { set; get; }
        public MaterialAccessData MaterialAccess { set; get; }
        public PKTData PKTData { set; get; }
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

    public class UserActiveData
    {
        public IList<UserActiveDataItem> Items { set; get; }
    }

    public class UserActiveDataItem
    {
        public int Id { set; get; }
        public string SalesCode { set; get; }
        public string SalesName { set; get; }
        public string DealerName { set; get; }
        public string DealerCode { set; get; }
        public string DealerCity { set; get; }
        public int TotalAccess { set; get; }
    }

    public class PKTData
    {
        public IList<PKTDataItem> Items { set; get; }
    }

    public class PKTDataItem
    {
        public int Id { set; get; }
        public string SalesCode { set; get; }
        public string SalesName { set; get; }
        public string DealerName { set; get; }
        public int TotalReport { set; get; }
    }

    public class MaterialAccessData
    {
        public IList<MaterialAccessDataItem> Items { set; get; }
    }

    public class MaterialAccessDataItem
    {
        public string Category { set; get; }
        public int TotalViews { set; get; }
    }
}
