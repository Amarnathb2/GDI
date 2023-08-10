using CMS.Core;
using GDI.Components.Widgets.Card;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using XperienceAdapter.Models;
using XperienceAdapter.Repositories;
[assembly: RegisterWidget(CardViewComponent.IDENTIFIER, typeof(CardViewComponent), "{$GDI.Widget.Card.Name$}", typeof(CardProperties), Description = "{$GDI.Widget.Card.Name}", IconClass = "icon-l-lightbox")]
namespace GDI.Components.Widgets.Card
{
    public class CardViewComponent : ViewComponent
    {

        public const string IDENTIFIER = "Card";

        private readonly IMediaFileRepository _mediaFileRepository;

        private readonly IEventLogService _eventLogService;

        public CardViewComponent(IMediaFileRepository mediaFileRepository, IEventLogService eventLogService)
        {
            _mediaFileRepository = mediaFileRepository ?? throw new ArgumentNullException(nameof(mediaFileRepository));
            _eventLogService = eventLogService ?? throw new ArgumentNullException(nameof(eventLogService));
        }

        public async Task<IViewComponentResult> InvokeAsync(CardProperties properties, CancellationToken? cancellationToken = null)
        {

            try
            {
                if (properties != null)
                {

                    CardViewModel cardModel = new();
                    cardModel.IsVisible = properties.IsVisible;
                    cardModel.Title = properties.Title;
                    cardModel.TargetURL = properties.TargetURL;
                    cardModel.Description = properties.Description;
                    cardModel.ImageAltText = properties.ImageAltText;
                    cardModel.ImagePosition = properties.ImagePosition;
                    cardModel.Background = properties.Background;
                    cardModel.IsButton = properties.IsButton;
                    cardModel.ButtonType = properties.ButtonType;
                    cardModel.ButtonTextOne = properties.ButtonTextOne;
                    cardModel.ButtonTargetOne = properties.ButtonTargetOne;
                    cardModel.ButtonURL = properties.ButtonURL;
                    cardModel.ButtonBackground = properties.ButtonBackground;

                    if (properties?.Image?.Count() > 0)
                    {
                        MediaLibraryFile? mediaFile = default;
                        mediaFile = await _mediaFileRepository.GetMediaFileAsync(properties.Image.First().FileGuid);
                        cardModel.Image = mediaFile?.MediaFileUrl?.DirectPath;

                    }
                    else
                    {
                        cardModel.Image = null;
                    }

                    if (properties?.BackgroundImage?.Count() > 0)
                    {
                        MediaLibraryFile? mediaFile = default;
                        mediaFile = await _mediaFileRepository.GetMediaFileAsync(properties.BackgroundImage.First().FileGuid);
                        cardModel.BackgroundImage = mediaFile?.MediaFileUrl?.DirectPath;

                    }
                    else
                    {
                        cardModel.BackgroundImage = null;
                    }
                    if (properties?.BackgroundSmallImage?.Count() > 0)
                    {
                        MediaLibraryFile? mediaFile = default;
                        mediaFile = await _mediaFileRepository.GetMediaFileAsync(properties.BackgroundSmallImage.First().FileGuid);
                        cardModel.BackgroundSmallImage = mediaFile?.MediaFileUrl?.DirectPath;

                    }
                    else
                    {
                        cardModel.BackgroundSmallImage = null;
                    }


                    return await Task.FromResult((IViewComponentResult)View("~/Components/Widgets/Card/_Card.cshtml", cardModel));
                }

                else
                {
                    return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
                }
            }
            catch (Exception ex)
            {

                _eventLogService.LogException(nameof(CardViewComponent), nameof(InvokeAsync), ex);


                return await Task.FromResult<IViewComponentResult>(Content(string.Empty));

            }

        }
    }

}
