using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Dashboard
{
    public class GetDashboardDto : BaseDto
    {
        public DashboardData Data { set; get; }
    }

    public class DashboardData
    {
        public TopMenuData TopMenu { set; get; }
        public ChartData Charts { set; get; }
        public UserActiveData UserActive { set; get; }
        public CategoryMaterialData CategoryMaterials { set; get; }
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
        public int Value { set; get; }
        public string DealerCode { set; get; }
        public string DealerName { set; get; }
        public string DealerCity { set; get; }
    }

    public class UserActiveData
    {
        public IList<UserActiveDataItem> Items { set; get; }
    }

    public class UserActiveDataItem
    {
        public int Id { set; get; }
        public string DealerCode { set; get; }
        public string DealerName { set; get; }
        public string DealerCity { set; get; }
        public string DealerGroup { set; get; }
        public int TotalAccess { set; get; }
    }

    public class CategoryMaterialData
    {
        public IList<CategoryMaterialItem> Items { set; get; }
    }

    public class CategoryMaterialItem
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Category { set; get; }
        public int TotalViews { set; get; }
        public int TotalUserAccessed { set; get; }
    }
}
