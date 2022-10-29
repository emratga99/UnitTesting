
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Logging; 
using Microsoft.AspNetCore.Mvc;
using mvc1.Controllers;
using mvc1.Models;
using mvc1.Services;
using System.Linq;

namespace RookiesControllerTest;

public class Tests
{
    private RookiesController _rookies;
    private Mock<IPersonService> _personServiceMock;
    private Mock<ILogger<RookiesController>> _loggerMock;
    private static List<PersonModel> _data = new List<PersonModel>{
        new PersonModel
        {
            FirstName = "Test",
            LastName = "Last Test",
            BirthPlace = "HN"
        },
        new PersonModel
        {
            FirstName = "Test2",
            LastName = "Last Test2",
            BirthPlace = "HN"
        }
    };

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<RookiesController>>();
        _personServiceMock = new Mock<IPersonService>();
        _rookies = new RookiesController(_loggerMock.Object, _personServiceMock.Object);
        _personServiceMock.Setup(q => q.GetAll()).Returns(_data);
    }

    [Test]
    public void Test1_GetAll_Success()
    {
        var result = _rookies.Index();

        Assert.IsInstanceOf<ViewResult>(result);

        var view = (ViewResult)result;

        Assert.IsInstanceOf<List<PersonModel>>(view.ViewData.Model);
        Assert.IsAssignableFrom<List<PersonModel>>(view.ViewData.Model);

        var list = (List<PersonModel>)view.ViewData.Model;

        Assert.AreEqual(2, list.Count());
    }

    [Test]
    public void Test2_Create_RedirectToAction_Success()
    {
        _rookies.ModelState.AddModelError("FirstName", "Required");

        var result = _rookies.Create(model: null);

        Assert.IsInstanceOf<RedirectToActionResult>(result);
    }

    [Test]
    public void Test3_Create_ReturnView()
    {
        var newCreatePerson = new PersonCreateModel()
        {
            FirstName = "sdasdh",
        };
        var result = _rookies.Create(newCreatePerson);

        Assert.IsInstanceOf<RedirectToActionResult>(result);

        var actual = (RedirectToActionResult)result;

        Assert.AreEqual("Index", actual.ActionName);
    }

    [Test]
    public void Test4_Update_ViewResult_IsValid()
    {
        _rookies.ModelState.AddModelError("FistName", "FieldRequired");

        var member = new PersonModel();
        var index = 1;
        var updateMember = new PersonUpdateModel();
        var result = _rookies.Update(index, updateMember);

        Assert.IsInstanceOf<BadRequestObjectResult>(result);

        var viewResult = (BadRequestObjectResult)result;
        if (viewResult.Value != null)
        {
            var errorsList = (List<string>)viewResult.Value; 
            Assert.AreEqual("FistName", errorsList[0] as string);
        }
    }

    [Test]
    public void Test5_Update_RedirectToAction_StateIsValid()
    {
        var member = new PersonModel();
        var index = 2;
        var updateMember = new PersonUpdateModel()
        {
            FirstName = "MOGH"
        };

        var result = _rookies.Update(index, updateMember);

        Assert.IsInstanceOf<RedirectToActionResult>(result);

        var redirectToActionResult = (RedirectToActionResult)result;

        Assert.Null(redirectToActionResult.ControllerName);
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
    }

    [Test]
    public void Test6_Detail_ReturnViewResult_Success()
    {
        int index = 1;
        var result = _rookies.Details(index);

        Assert.IsInstanceOf<ViewResult>(result);
    }

    [Test]
    public void Test7_Detail_ReturnView()
    {
        var testId = 1;

        _personServiceMock.Setup(x => x.GetOne(testId)).Returns((PersonModel)null);

        var result = _rookies.Details(testId);

        Assert.IsInstanceOf<ViewResult>(result); 
    }

    [Test]
    public void Test8_Delete_Success()
    {
        int index = 1;
        _personServiceMock.Setup(p => p.Delete(index)).Callback(() =>
        {
            _data.Remove(_data[1]);
        }).Returns(_data[1]);

        var controller = new RookiesController(_loggerMock.Object, _personServiceMock.Object);
        var expected = _data.Count() - 1;

        var result = controller.Delete(1);
        var actual = _data.Count();

        Assert.IsInstanceOf<ViewResult>(result);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, actual);
    }
}