namespace RadCars.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using ViewModels.Feature;
using Services.Data.Contracts;

using static Common.NotificationTypeConstants;
using static Common.ExceptionsAndNotificationsMessages;

public class FeatureController : BaseAdminController
{
    private readonly IFeatureCategoryService featureCategoryService;

    public FeatureController(IFeatureCategoryService featureCategoryService)
    {
        this.featureCategoryService = featureCategoryService;
    }

    [HttpGet]
    public async Task<IActionResult> AllByCategory(int categoryId)
    {
        try
        {
            var features = await this.featureCategoryService.GetAllFeaturesByCategoryIdAsync(categoryId);

            return View(features);
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("All", "FeatureCategory");
        }
    }

    [HttpGet]
    public IActionResult Create(int categoryId)
    {
        var formModel = new FeatureFormModel
        {
            CategoryId = categoryId
        };

        return View(formModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(FeatureFormModel form)
    {
        try
        {
            if (this.ModelState.IsValid == false)
            {
                return View(form);
            }

            var categoryId = await this.featureCategoryService.CreateFeatureAsync(form);

            return RedirectToAction("AllByCategory", new { categoryId });
        }
        catch (InvalidOperationException)
        {
            this.TempData[ErrorMessage] = EntityAlreadyExists;

            return RedirectToAction("All", "FeatureCategory");
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("All", "FeatureCategory");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int featureId)
    {
        try
        {
            var formModel = await this.featureCategoryService.GetFeatureEditByIdAsync(featureId);

            return View(formModel);
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("All", "FeatureCategory");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(FeatureFormModel form)
    {
        try
        {
            if (this.ModelState.IsValid == false)
            {
                return View(form);
            }

            var categoryId = await this.featureCategoryService.EditFeatureAsync(form);

            return RedirectToAction("AllByCategory", new { categoryId });
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("All", "FeatureCategory");
        }
    }

    [HttpPost]
    public async Task<IActionResult> SoftDelete(int featureId)
    {
        try
        {
            var categoryId = await this.featureCategoryService.SoftDeleteFeatureByIdAsync(featureId);

            return RedirectToAction("AllByCategory", new { categoryId });
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("All", "FeatureCategory");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Undelete(int featureId)
    {
        try
        {
            var categoryId = await this.featureCategoryService.UndeleteFeatureByIdAsync(featureId);

            return RedirectToAction("AllByCategory", new { categoryId });
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("All", "FeatureCategory");
        }
    }
}