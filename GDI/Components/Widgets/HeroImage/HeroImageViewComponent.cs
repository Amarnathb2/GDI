using CMS.Core;
using GDI.Components.Widgets.HeroImage;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using XperienceAdapter.Models;
using XperienceAdapter.Repositories;

[assembly: RegisterWidget(HeroImageViewComponent.IDENTIFIER, typeof(HeroImageViewComponent), "Hero Image Widget",
    typeof(HeroImageProperties), Description = "Displays an Text, Image",
    IconClass = "icon-badge")]

namespace GDI.Components.Widgets.HeroImage
{
    public class HeroImageViewComponent : ViewComponent
    {
        /// <summary>
        /// Identifier for the HerobannerImage Widget
        /// </summary>
        public const string IDENTIFIER = "GDI.Widget.HeroImage";

        private readonly IMediaFileRepository _mediaFileRepository;
        private readonly IEventLogService _eventLogService;

        public HeroImageViewComponent(IMediaFileRepository mediaFileRepository, IEventLogService eventLogService)
        {
            _mediaFileRepository = mediaFileRepository ?? throw new ArgumentNullException(nameof(mediaFileRepository));
            _eventLogService = eventLogService ?? throw new ArgumentNullException(nameof(eventLogService));
        }

        public async Task<IViewComponentResult> InvokeAsync(HeroImageProperties properties, CancellationToken? cancellationToken = null)
        {
            try
            {
                if (properties != null)
                {
                    HeroImageViewModel BannerModel = new();
                    BannerModel.Visible = properties.Visible;
                    BannerModel.HeroDescription = properties.HeroDescription;
                    BannerModel.HeroTitle = properties.HeroTitle;
                    BannerModel.ButtonTextOne = properties.ButtonTextOne;
                    BannerModel.ButtonUrlOne = properties.ButtonUrlOne;
                    BannerModel.ButtonTargetOne = properties.ButtonTargetOne;
                    BannerModel.Transformation = properties.Transformation;
                    BannerModel.CrossImageBottomAltText = properties.crossImageBottomAltText;
                    BannerModel.crossImageTopAltText = properties.CrossImageTopAltText;
                    BannerModel.ButtonClass = properties.ButtonClass;
                    BannerModel.BackgroundColor = properties.BackgroundColor;
                    BannerModel.SelectPage = properties.SelectPage;

                    if (properties?.ImageSmall?.Count() > 0)
                    {
                        MediaLibraryFile? mediaFile = default;
                        mediaFile = await _mediaFileRepository.GetMediaFileAsync(properties.ImageSmall.FirstOrDefault().FileGuid);
                        BannerModel.ImageSmall = mediaFile?.MediaFileUrl?.DirectPath;
                    }
                    else
                    {
                        BannerModel.ImageSmall = null;
                    }
                    if (properties?.ImageMedium?.Count() > 0)
                    {
                        MediaLibraryFile? mediaFile = default;
                        mediaFile = await _mediaFileRepository.GetMediaFileAsync(properties.ImageMedium.FirstOrDefault().FileGuid);
                        BannerModel.ImageMedium = mediaFile?.MediaFileUrl?.DirectPath;
                    }
                    else
                    {
                        BannerModel.ImageMedium = null;
                    }
                    if (properties?.ImageLarge?.Count() > 0)
                    {
                        MediaLibraryFile? mediaFile = default;
                        mediaFile = await _mediaFileRepository.GetMediaFileAsync(properties.ImageLarge.FirstOrDefault().FileGuid);
                        BannerModel.ImageLarge = mediaFile?.MediaFileUrl?.DirectPath;
                    }
                    else
                    {
                        BannerModel.ImageLarge = null;
                    }
                    if (properties?.ImageXLarge?.Count() > 0)
                    {
                        MediaLibraryFile? mediaFile = default;
                        mediaFile = await _mediaFileRepository.GetMediaFileAsync(properties.ImageXLarge.FirstOrDefault().FileGuid);
                        BannerModel.ImageXLarge = mediaFile?.MediaFileUrl?.DirectPath;
                    }
                    else
                    {
                        BannerModel.ImageXLarge = null;
                    }
                    if (properties?.CrossImageTop?.Count() > 0)
                    {
                        MediaLibraryFile? mediaFile = default;
                        mediaFile = await _mediaFileRepository.GetMediaFileAsync(properties.CrossImageTop.FirstOrDefault().FileGuid);
                        BannerModel.CrossImageTop = mediaFile?.MediaFileUrl?.DirectPath;
                    }
                    else
                    {
                        BannerModel.CrossImageTop = null;
                    }
                    if (properties?.CrossImageBottom?.Count() > 0)
                    {
                        MediaLibraryFile? mediaFile = default;
                        mediaFile = await _mediaFileRepository.GetMediaFileAsync(properties.CrossImageBottom.FirstOrDefault().FileGuid);
                        BannerModel.CrossImageBottom = mediaFile?.MediaFileUrl?.DirectPath;
                    }
                    else
                    {
                        BannerModel.CrossImageBottom = null;
                    }

                    if (properties?.BackgroundImage?.Count() > 0)
                    {
                        MediaLibraryFile? mediaFile = default;
                        mediaFile = await _mediaFileRepository.GetMediaFileAsync(properties.BackgroundImage.FirstOrDefault().FileGuid);
                        BannerModel.BackgroundImage = mediaFile?.MediaFileUrl?.DirectPath;
                    }
                    else
                    {
                        BannerModel.BackgroundImage = null;
                    }
                    if (properties?.DefaultImage?.Count() > 0)
                    {
                        MediaLibraryFile? mediaFile = default;
                        mediaFile = await _mediaFileRepository.GetMediaFileAsync(properties.DefaultImage.FirstOrDefault().FileGuid);
                        BannerModel.DefaultImage = mediaFile?.MediaFileUrl?.DirectPath;
                    }
                    else
                    {
                        BannerModel.DefaultImage = null;
                    }
                    return await Task.FromResult((IViewComponentResult)View("~/Components/Widgets/HeroImage/_HeroImage.cshtml", BannerModel));
                }
            }
            catch (Exception ex)
            {
                _eventLogService.LogException(nameof(HeroImageViewComponent), nameof(InvokeAsync), ex);
            }
            return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
        }
    }
}