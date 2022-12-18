namespace Ecommerce.Web.Areas.AdminPanel.Mappings
{
	using AutoMapper;
	using Ecommerce.Data.Models;
	using Ecommerce.Web.Areas.AdminPanel.Models.Admin;

	public class AdminPanelProfile : Profile
	{
		public AdminPanelProfile()
		{
			this.CreateMap<Product, ProductStatisticsViewModel>();
			this.CreateMap<Category, CategoryStatisticsViewModel>();
		}
	}
}
