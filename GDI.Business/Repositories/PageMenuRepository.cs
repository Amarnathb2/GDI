using CMS.DocumentEngine;
using GDI.Business.Models;
using XperienceAdapter.Repositories;
using XperienceAdapter.Services;

namespace GDI.Business.Repositories;

public class PageMenuRepository : BasePageRepository<PageMenuModel, TreeNode>
{
    public PageMenuRepository(IRepositoryServices repositoryServices) : base(repositoryServices)
    {

    }
    public override void MapDtoProperties(TreeNode treeNode, PageMenuModel dto)
    {
        dto.PageTitle = (string?)treeNode.GetValue("PageTitle");
        dto.Description = (string?)treeNode.GetValue("Description");
        dto.RelativeUrl = (string?)treeNode.GetValue("RelativeUrl");
        dto.TeaserImage = (string?)treeNode.GetValue("TeaserImage");
        dto.TeaserImageAltText = (string?)treeNode.GetValue("TeaserImageAltText");
        dto.MenuItemGroup = (string?)treeNode.GetValue("MenuItemGroup");
        dto.HasChild = (bool?)treeNode.GetBooleanValue("HasChild", false);
        dto.IsChild = (bool?)treeNode.GetBooleanValue("IsChild", false);
    }
}
