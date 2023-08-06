namespace RadCars.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Data.Contracts;
using ViewModels.FeatureCategory;

using static Common.NotificationTypeConstants;
using static Common.ExceptionsAndNotificationsMessages;

public class FeatureCategoryController : BaseAdminController
{
    private readonly IFeatureCategoryService featureCategoryService;

    public FeatureCategoryController(IFeatureCategoryService featureCategoryService)
    {
        this.featureCategoryService = featureCategoryService;
    }

    public async Task<IActionResult> All()
    {
        try
        {
            var categories = await this.featureCategoryService.GetCategoriesAsync();

            return View(categories);
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("Index", "Home");
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(FeatureCategoryFormModel form)
    {
        try
        {
            if (this.ModelState.IsValid == false)
            {
                this.TempData[ErrorMessage] = InvalidDataProvidedError;

                return View(form);
            }

            var categoryId = await this.featureCategoryService.CreateCategoryAsync(form);

            return RedirectToAction("AllByCategory", "Feature", new { categoryId });
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("Index", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int categoryId)
    {
        try
        {
            var viewModel = await this.featureCategoryService.GetCategoryEditAsync(categoryId);

            return View(viewModel);
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(FeatureCategoryFormModel form)
    {
        try
        {
            if (this.ModelState.IsValid == false)
            {
                this.TempData[ErrorMessage] = InvalidDataProvidedError;

                return View(form);
            }

            await this.featureCategoryService.EditCategoryAsync(form);

            return RedirectToAction("All", "FeatureCategory");
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    public async Task<IActionResult> SoftDelete(int categoryId)
    {
        try
        {
            await this.featureCategoryService.SoftDeleteCategoryAndFeaturesByCategoryIdAsync(categoryId);

            return RedirectToAction("All", "FeatureCategory");
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Undelete(int categoryId)
    {
        try
        {
            await this.featureCategoryService.UndeleteCategoryAndFeaturesByCategoryIdAsync(categoryId);

            return RedirectToAction("All", "FeatureCategory");
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = AnErrorOccurred;

            return RedirectToAction("Index", "Home");
        }
    }
}